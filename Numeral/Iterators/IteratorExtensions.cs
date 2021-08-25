using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Numeral.Iterators;

namespace Numeral.Internals
{
    public static partial class IteratorExtensions
    {
        public static unsafe NDIterator<T> CreateIterator<T>(
            this DenseTensor<T> tensor,
            in ReadOnlySpan<int> fullShape,
            in ReadOnlySpan<int> arrayShape,
            in ReadOnlySpan<int> arrayStrides,
            in ReadOnlySpan<int> axisOrder,
            int operandRank)
        {
            var rank = fullShape.Length;
            var iter = new NDIterator<T>(rank, Unsafe.AsPointer(ref tensor._data.Span[0]));

            int axis, arrSize, arrStride, fullSize;

            for (var i = 0; i < rank; i++)
            {
                var i_full = rank - 1 - i;
                var i_arr = arrayShape.Length - 1 - i;
                var i_ord = axisOrder.Length - 1 - i;

                fullSize = fullShape[i_full];
                arrSize = -1;
                arrStride = -1;

                iter.FullShape[i_full] = fullSize;

                if (i_ord >= 0)
                {
                    axis = axisOrder[i_ord];
                    if (axis >= 0)
                    {
                        arrSize = arrayShape[axis];
                        arrStride = arrayStrides[axis];
                    }
                }
                else if (i_arr >= 0)
                {
                    arrSize = arrayShape[i_arr];
                    arrStride = arrayStrides[i_arr];
                }

                if (arrSize == -1)
                {
                    iter.Strides[i_full] = 0;
                }
                else
                {
                    if (!IteratorHelpers.CheckDimension(fullSize, arrSize))
                        throw new Exception();

                    if (arrSize == 1 && fullSize > 1)
                        iter.Strides[i_full] = 0;
                    else
                        iter.Strides[i_full] = arrStride;
                }
            }

            IteratorHelpers.GetBackstrides(
                fullShape, iter.Strides, iter.Backstrides);

            iter.Initialize(tensor._flags.IsContiguous(), operandRank);

            return iter;
        }

        public static unsafe NDIterator<T> CreateIterator<T>(
            this DenseTensor<T> tensor,
            in ReadOnlySpan<int> fullShape,
            in ReadOnlySpan<int> arrayShape,
            in ReadOnlySpan<int> arrayStrides,
            int operandRank)
        {
            var rank = fullShape.Length;
            var iter = new NDIterator<T>(rank, Unsafe.AsPointer(ref tensor._data.Span[0]));

            // populate iter params
            for (var i = 0; i < rank; i++)
            {
                var i_full = rank - 1 - i;
                var i_arr = arrayShape.Length - 1 - i;

                var fullSize = fullShape[i_full];
                iter.FullShape[i_full] = fullSize;

                if (i_arr >= 0)
                {
                    var arraySize = arrayShape[i_arr];

                    if (!IteratorHelpers.CheckDimension(fullSize, arraySize))
                        throw new Exception();

                    if (fullSize > arraySize && arraySize == 1)
                        iter.Strides[i_full] = 0;
                    else
                        iter.Strides[i_full] = arrayStrides[i_arr];

                    continue;
                }

                iter.Strides[i_full] = 0;
            }

            IteratorHelpers.GetBackstrides(
                fullShape, iter.Strides, iter.Backstrides);

            iter.Initialize(tensor._flags.IsContiguous(), operandRank);

            return iter;
        }

        // public static NDIterator<T> GetIterator<T>(
        //     this DenseTensor<T> array,
        //     in ReadOnlySpan<int> fullShape,
        //     in ReadOnlySpan<int> order,
        //     int operandRank)
        // {
        //     var iter = CreateIterator<T>(fullShape, array.Shape, array.Strides, order, operandRank);
        //     iter.Bind(array);
        //     return iter;
        // }

        // public static NDIterator<T> GetIterator<T>(
        //     this DenseTensor<T> array,
        //     in ReadOnlySpan<int> fullShape,
        //     int operandRank)
        // {
        //     var iter = CreateIterator<T>(fullShape, array.Shape, array.Strides, operandRank);
        //     iter.Bind(array);
        //     return iter;
        // }

        public static unsafe NDIterator<T> CreateIterator<T>(
            this Scalar<T> scalar,
            in ReadOnlySpan<int> fullShape,
            int operandRank)
        {
            var iter = new NDIterator<T>(fullShape.Length, Unsafe.AsPointer(ref scalar._value));

            for (var i = 0; i < fullShape.Length; i++)
                iter.FullShape[i] = fullShape[i];

            iter.Initialize(true, operandRank);

            return iter;
        }

        // public static unsafe void Bind<T>(this ref NDIterator<T> iterator, DenseTensor<T> tensor)
        // {
        //     fixed (int* s = iterator.Strides, i = iterator.Indices)
        //     {
        //         var index = IndexHelpers.GetIndex(s, i, iterator.Rank);
        //         iterator.DataPointer = Unsafe.AsPointer(ref tensor._data.Span[index]);
        //     }
        // }

        public static void Truncate<T>(this ref NDIterator<T> iter, int nDims)
        {
            if (nDims < 0 || nDims > iter.FullRank)
                throw new ArgumentOutOfRangeException(nameof(nDims));

            iter.Rank = iter.FullRank - nDims;
        }

        public static unsafe void Initialize<T>(this ref NDIterator<T> iter, bool isMemoryContiguous, int operandRank)
        {
            iter.OperandSize = iter.GetOperandSize(operandRank);

            if (operandRank == 1 && iter.Strides[iter.FullRank - 1] == 0)
            {
                iter.Flags = IteratorFlags.Filled;
                iter.Truncate(operandRank);
            }
            else if (isMemoryContiguous && iter.IsOperandContiguous(operandRank))
            {
                iter.Flags = IteratorFlags.Contiguous;
                iter.Truncate(operandRank);
                return;
            }
            else
            {
                iter.Flags = IteratorFlags.Buffered;
            }

            iter.BorrowedMemory = MemoryPool<T>.Shared.Rent(iter.OperandSize);
        }

        public static unsafe int GetOperandSize<T>(this in NDIterator<T> iterator, int operandRank)
        {
            fixed (int* pshape = iterator.FullShape)
                return ArrayHelpers.GetProductSum(pshape, iterator.FullRank, iterator.FullRank - operandRank);
        }

        /// <summary>
        /// Inspects the iterator to determine if it's operand (inner-most n dims)
        /// is contiguous.
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="operandRank"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static unsafe bool IsOperandContiguous<T>(this in NDIterator<T> iterator, int operandRank)
        {
            if (operandRank == 0)
                return true;

            var iterRank = iterator.FullRank;
            int s, sprev = -1;
            var i = 0;

            // check that the n=operandRank inner-most, non-broadcasted, loops
            // are contiguous if their strides are strictly increasing
            for (; i < iterRank; i++)
            {
                var i_full = iterRank - 1 - i;
                if (iterator.Strides[i_full] != 0)
                {
                    s = iterator.Strides[i_full];
                    if (s < sprev) return false;
                    sprev = s;

                    if (i + 1 == operandRank)
                        break;
                }
            }

            // now check that all strides before the n=operandRank stride
            // are larger

            i = 0;
            for (; i < iterRank - operandRank; i++)
            {
                s = iterator.Strides[i];
                if (s != 0 && s < sprev) return false;
            }

            return true;
        }

        /// <summary>
        /// Produces one out of n=<paramref name="segments"/> iterators that
        /// covers an n-dim slice of the overall iteration structure.  This
        /// is used for dividing up work to split between several threads.
        /// </summary>
        /// <param name="iterator"></param>
        /// <param name="axis"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static unsafe NDIterator<T> Divide<T>(this in NDIterator<T> iterator, int axis, int segments, int index)
        {
            var copied = iterator;

            var count = Math.DivRem(iterator.FullShape[axis], segments, out var remainder);
            var startAt = index * count;
            var extra = index == segments - 1 ? remainder : 0;
            var endAt = startAt + count + extra;

            copied.Indices[axis] = startAt;
            copied.Offsets[axis] = startAt;
            copied.FullShape[axis] = endAt;

            var offset = IndexHelpers.GetIndex(copied.Strides, copied.Indices, copied.Rank);
            copied.DataPointer = Unsafe.Add<T>(iterator.DataPointer, offset);

            // the derived iterator has to rent it's own buffer if the original
            // had one, so there are no possible read/write conflicts
            if (iterator.BorrowedMemory != null)
                copied.BorrowedMemory = MemoryPool<T>.Shared.Rent(copied.OperandSize);

            return copied;
        }

    }
}
