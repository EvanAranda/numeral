using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Numeral.Iterators;

namespace Numeral
{
    public interface INDArray
    {
        ReadOnlySpan<int> Shape { get; }
        ReadOnlySpan<int> Strides { get; }
        int Rank => Shape.Length;
        int Count { get; }
    }

    public interface INDArray<T> : INDArray, IEnumerable<T>
    {
        ref T this[params int[] indices] { get; }
        ref T this[ReadOnlySpan<int> indices] { get; }
        ref T Flat(int index);
    }

    [Flags]
    public enum ArrayFlags
    {
        NONE = 0,
        IS_CONTIGUOUS = 1,
        IS_SLICE = 1 << 2
    }

    public static class ArrayFlagsHelpers
    {
        public static bool IsContiguous<T>(this NDArray<T> array)
            => array._flags.HasFlag(ArrayFlags.IS_CONTIGUOUS);

        public static bool IsSlice<T>(this NDArray<T> array)
            => array._flags.HasFlag(ArrayFlags.IS_SLICE);

        public static bool HasFlags<T>(this NDArray<T> array)
            => array._flags == ArrayFlags.NONE;
    }

    [DebuggerDisplay("({_flags}, {Rank})\n{ToString()}")]
    public readonly partial struct NDArray<T> : INDArray<T>
    {
        internal readonly ArrayFlags _flags;
        internal readonly Memory<T> _buffer;
        internal readonly ReadOnlyMemory<int> _shape;
        internal readonly ReadOnlyMemory<int> _strides;
        private readonly int _count;

        #region Constructors

        /// <summary>
        /// Creates a new 1-d array over the buffer.
        /// </summary>
        /// <param name="buffer"></param>
        internal NDArray(Memory<T> buffer)
            : this(buffer, Constants.SmallArrays.Get(buffer.Length), Constants.OneArray, buffer.Length,
                ArrayFlags.IS_CONTIGUOUS)
        { }

        /// <summary>
        /// /// Create new n-d array of shape over buffer. Strides and count are
        /// calculated and the <see cref="ArrayFlags.IS_CONTIGUOUS"/> is set.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="shape"></param>
        internal NDArray(Memory<T> buffer, ReadOnlyMemory<int> shape)
            : this(buffer, shape, IteratorHelpers.GetStrides(shape.Span), ArrayHelpers.GetProductSum(shape.Span),
                ArrayFlags.IS_CONTIGUOUS)
        { }

        /// <summary>
        /// Creates a new fully specified n-d array.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="shape"></param>
        /// <param name="strides"></param>
        /// <param name="count"></param>
        /// <param name="flags"></param>
        internal NDArray(Memory<T> buffer, ReadOnlyMemory<int> shape, ReadOnlyMemory<int> strides, int count, ArrayFlags flags)
        {
            _flags = flags;
            _buffer = buffer;
            _shape = shape;
            _strides = strides;
            _count = count;
        }


        #endregion

        public ReadOnlySpan<int> Shape => _shape.Span;
        public ReadOnlySpan<int> Strides => _strides.Span;
        public int Rank => Shape.Length;
        public int Count => _count;
        public ref T Flat(int index)
        {
            if (this.IsContiguous())
                return ref _buffer.Span[index];

            Span<int> indices = stackalloc int[Rank];
            IndexHelpers.GetIndices(Strides, index, indices);
            index = IndexHelpers.GetIndex(Strides, indices);
            return ref _buffer.Span[index];
        }

        public ref T this[params int[] indices] => ref GetElement(indices);
        public ref T this[ReadOnlySpan<int> indices] => ref GetElement(indices);

        private ref T GetElement(ReadOnlySpan<int> indices)
        {
            if (Rank == 1)
                return ref _buffer.Span[indices[0]];

            return ref _buffer.Span[IndexHelpers.GetIndex(Strides, indices)];
        }

        public override string ToString()
        {
            return this.Display(limit: 5);
        }

        #region IEnumerable

        struct Enumerator : IEnumerator<T>
        {
            private NDIterator<T> _iterator;

            public Enumerator(NDArray<T> arr)
            {
                _iterator = arr.GetIterator(arr.Shape, 0);
            }

            public unsafe T Current => Unsafe.Read<T>(_iterator.DataPointer);
            object IEnumerator.Current => Current;
            public void Dispose() => _iterator.Dispose();
            public unsafe bool MoveNext() => _iterator.IncrementPointer();
            public void Reset() => throw new NotSupportedException();
        }

        public IEnumerator<T> GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion

        #region Casting Overloads

        public static implicit operator NDArray<T>(T[] arr)
            => new(arr);

        public static implicit operator NDArray<T>(T[,] arr)
            => NDArray.AsNDArray(arr);

        public static implicit operator NDArray<T>(T[,,] arr)
            => NDArray.AsNDArray(arr);

        public static implicit operator NDArray<T>(Memory<T> buffer)
            => new(buffer);

        public static implicit operator Memory<T>(NDArray<T> arr)
            => arr._buffer;

        public static implicit operator ReadOnlyMemory<T>(NDArray<T> arr)
            => arr._buffer;

        public static implicit operator Span<T>(NDArray<T> arr)
            => arr._buffer.Span;

        public static implicit operator ReadOnlySpan<T>(NDArray<T> arr)
            => arr._buffer.Span;

        #endregion
    }


    // public readonly struct NDSparseArray<T>
    // {
    //     private readonly ReadOnlyMemory<int> _shape;
    //     private readonly Dictionary<int, T> _data;

    //     public ref T Flat(int index)
    //     {
    //     }
    // }

}
