﻿using System;
using Numeral.Internals;
using Numeral.Iterators;

namespace Numeral
{
    public class DenseTensor<T> : Tensor<T>
    {
        internal readonly ArrayFlags _flags;
        internal readonly Memory<T> _data;

        internal DenseTensor(
            in Memory<T> data,
            in ReadOnlyMemory<int> shape,
            in ReadOnlyMemory<int> strides,
            int count, ArrayFlags flags)
            : base(shape, strides, count)
        {
            _data = data;
            _flags = flags;
        }

        internal DenseTensor(
            in Memory<T> data,
            in ReadOnlyMemory<int> shape)
            : base(shape, IteratorHelpers.GetStrides(shape.Span), ArrayHelpers.GetProductSum(shape.Span))
        {
            _data = data;
            _flags = ArrayFlags.IsContiguous;
        }

        internal DenseTensor(in Memory<T> data)
            : base(Constants.SmallArrays.Get(data.Length),
                   Constants.OneArray,
                   data.Length)
        {
            _data = data;
            _flags = ArrayFlags.IsContiguous;
        }

        public override IFactory Factory => throw new NotImplementedException();
        public override ITensorCore<T> Core => Core<T>.Instance;

        public override ref T this[params int[] indices] => ref GetElement(indices);

        public override ref T this[params Index[] indices] => ref GetElement(indices);

        public override ref T this[in ReadOnlySpan<int> indices] => ref GetElement(indices);

        public override ref T this[in ReadOnlySpan<Index> indices] => ref GetElement(indices);

        public override ref T GetFlat(int index)
        {
            if (_flags.IsContiguous())
                return ref _data.Span[index];

            Span<int> indices = stackalloc int[Rank];
            IndexHelpers.GetIndices(Strides, index, indices);
            index = IndexHelpers.GetIndex(Strides, indices);
            return ref _data.Span[index];
        }

        public override Tensor<T> Squeeze()
        {
            if (Rank == 1)
                return this;

            var newCount = 1;
            var j = 0;
            Span<int> newShape = stackalloc int[Rank];
            for (int i = 0; i < newShape.Length; i++)
            {
                if (Shape[i] == 1) continue;

                newShape[j] = Shape[i];
                newCount *= newShape[j];
                j++;
            }

            return new DenseTensor<T>(
                _data,
                newShape[..j].ToArray(),
                IteratorHelpers.GetStrides(newShape[..j]),
                newCount,
                _flags);
        }

        public override Tensor<T> Reshape(params int[] newShape)
        {
            // the array is a view that cannot be reshaped
            // so a copy will be created
            // if (Count != _buffer.Length)
            if (!_flags.IsContiguous())
            {
                if (Options.NoHiddenCopies)
                    throw new NotSupportedException("this operation requires returning a copy");

                return Copy().Reshape(newShape);
            }

            var partialCount = 1;
            var placeholderDim = -1;
            for (var i = 0; i < newShape.Length; i++)
            {
                if (newShape[i] == -1)
                {
                    if (placeholderDim != -1)
                        throw new Exception("only 1 placeholder value (-1) is allowed");

                    placeholderDim = i;
                    continue;
                }

                partialCount *= newShape[i];
            }

            if (placeholderDim != -1)
            {
                newShape[placeholderDim] = Count / partialCount;
            }
            else
            {
                var count = ArrayHelpers.GetProductSum(newShape);
                if (Count != count)
                    throw new Exception();
            }

            var strides = IteratorHelpers.GetStrides(newShape);
            return new DenseTensor<T>(_data, newShape, strides, Count, ArrayFlags.IsContiguous);
        }

        public override Tensor<T> Transpose()
        {
            if (Rank == 1)
                return this;

            var newShape = Shape.ToArray();
            newShape.AsSpan().Reverse();

            var newStrides = Strides.ToArray();
            newStrides.AsSpan().Reverse();

            var flags = _flags;

            // set the contiguity flag if the strides are ordered
            if (ArrayHelpers.AllDecreasing(newStrides))
                flags |= ArrayFlags.IsContiguous;
            else
                flags &= ~ArrayFlags.IsContiguous;

            return new DenseTensor<T>(_data, newShape, newStrides, Count, flags);
        }

        public override Tensor<T> Transpose(params int[] order)
        {
            var newStrides = new int[Rank];
            ArrayHelpers.GetPermuted(Strides, newStrides, order);

            var newShape = Shape.ToArray();
            ArrayHelpers.GetPermuted(Shape, newShape, order);

            var flags = _flags;

            // set the contiguity flag if the strides are ordered
            if (ArrayHelpers.AllDecreasing(newStrides))
                flags |= ArrayFlags.IsContiguous;
            else
                flags &= ~ArrayFlags.IsContiguous;

            return new DenseTensor<T>(_data, newShape, newStrides, Count, flags);
        }

        public override Tensor<T>[] Split(int segments, int axis = 0)
        {
            if (axis < 0)
                axis = Rank + axis;

            if (axis < 0)
                throw new Exception("a negative value for axis was too large");

            if (axis > Rank - 1)
                throw new Exception("assert failed: axis > Rank - 1");

            var axisSize = Shape[axis];
            if ((axisSize % segments) != 0)
                throw new Exception("assert failed: size % count != 0");

            // initialize slices to Range.All
            var slices = new Slice[axis + 1];
            for (var i = 0; i < slices.Length; i++)
                slices[i] = Range.All;

            // each sub array is constructed by adjusting
            // the slice of the axis
            var size = axisSize / segments;
            var subArrays = new Tensor<T>[segments];
            for (int i = 0; i < segments; i++)
            {
                slices[axis] = (i * size)..((i + 1) * size);
                subArrays[i] = Slice(slices);
            }

            return subArrays;
        }

        public override Tensor<T> Slice(params Slice[] slices)
        {
            // if (slices.Length == 1)
            // {
            //     var (o, c) = slices[0].Range.GetOffsetAndLength(Shape[0]);
            //     var newCount = Count / Shape[0] * c;
            //     return new NDArray<T>(
            //         _buffer.Slice(o * Strides[0], newCount),
            //         _shape[1..],
            //         _strides[1..],
            //         newCount,
            //         _flags | ArrayFlags.IS_SLICE
            //     );
            // }

            Span<int> startOffsets = stackalloc int[Rank];
            Span<int> endOffsets = stackalloc int[Rank];
            Span<int> shape = stackalloc int[Rank];
            Span<int> strides = stackalloc int[Rank];
            var count = 1;

            var j = 0;
            for (var i = 0; i < Rank; i++)
            {
                if (i < slices.Length)
                {
                    var (o, c) = slices[i].Range.GetOffsetAndLength(Shape[i]);
                    startOffsets[i] = o;
                    endOffsets[i] = o + c - 1;

                    if (c > 1)
                    {
                        shape[j] = c;
                        strides[j] = Strides[i];
                        count *= c;
                        j++;
                    }
                }
                else
                {
                    var c = Shape[i];
                    startOffsets[i] = 0;
                    endOffsets[i] = c - 1;
                    shape[j] = c;
                    strides[j] = Strides[i];
                    count *= c;
                    j++;
                }
            }

            var startOffset = IndexHelpers.GetIndex(Strides, startOffsets);
            var endOffset = IndexHelpers.GetIndex(Strides, endOffsets);

            // slice did not change number of elements
            // therefore nothing else about the array changes
            if (count == Count)
                return this;

            var flags = _flags;

            // if the buffer is a different size than the count
            // this array can't be contiguous
            if (endOffset - startOffset + 1 != count)
                flags &= ~ArrayFlags.IsContiguous;
            else
                flags |= ArrayFlags.IsContiguous;

            flags |= ArrayFlags.IsSlice;

            return new DenseTensor<T>(
                _data[startOffset..(endOffset + 1)],
                shape.Slice(0, j).ToArray(),
                strides.Slice(0, j).ToArray(),
                count,
                flags);

        }

        public override Tensor<T> Copy()
        {
            throw new NotImplementedException();
        }

        public override Tensor<R> Cast<R>()
        {
            if (typeof(R) == typeof(T))
                return this as Tensor<R>;

            throw new NotImplementedException();
        }

        public override Tensor<R> ReinterpretCast<R>()
        {
            if (typeof(R) == typeof(T))
                return this as Tensor<R>;

            throw new NotImplementedException();
        }

        public override INDIterator<T> GetIterator(int operandRank = 1)
        {
            return IteratorExtensions.GetIterator(this, Shape, operandRank);
        }

        public override INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, int operandRank = 1)
        {
            return IteratorExtensions.GetIterator(this, shape, operandRank);
        }

        public override INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, in ReadOnlySpan<int> order, int operandRank = 1)
        {
            return IteratorExtensions.GetIterator(this, shape, order, operandRank);
        }

        private ref T GetElement(ReadOnlySpan<int> indices)
        {
            if (Rank == 1 && _flags.IsContiguous())
                return ref _data.Span[indices[0]];

            return ref _data.Span[IndexHelpers.GetIndex(Strides, indices)];
        }

        private ref T GetElement(ReadOnlySpan<Index> indices)
        {
            if (Rank == 1 && _flags.IsContiguous())
                return ref _data.Span[indices[0]];

            Span<int> _indices = stackalloc int[Rank];
            for (var i = 0; i < Rank; i++)
                _indices[i] = indices[i].GetOffset(Shape[i]);

            return ref _data.Span[IndexHelpers.GetIndex(Strides, _indices)];
        }

        public static implicit operator Span<T>(DenseTensor<T> dense) => dense._data.Span;
        public static implicit operator ReadOnlySpan<T>(DenseTensor<T> dense) => dense._data.Span;
        public static implicit operator Memory<T>(DenseTensor<T> dense) => dense._data;
        public static implicit operator ReadOnlyMemory<T>(DenseTensor<T> dense) => dense._data;

        public static implicit operator DenseTensor<T>(Memory<T> memory) => new DenseTensor<T>(memory);
    }
}
