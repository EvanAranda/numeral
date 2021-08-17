using System;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        /// <summary>
        /// Returns a new view of the buffer with shape and strides reversed.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static NDArray<T> Transpose<T>(this in NDArray<T> array)
        {
            if (array.Rank == 1)
                return array;

            var newShape = array.Shape.ToArray();
            newShape.AsSpan().Reverse();

            var newStrides = array.Strides.ToArray();
            newStrides.AsSpan().Reverse();

            var flags = array._flags;

            // set the contiguity flag if the strides are ordered
            if (ArrayHelpers.AllDecreasing(newStrides))
                flags |= ArrayFlags.IS_CONTIGUOUS;
            else
                flags &= ~ArrayFlags.IS_CONTIGUOUS;

            return new NDArray<T>(array, newShape, newStrides, array.Count, flags);
        }

        /// <summary>
        /// Returns a new view of the buffer with shape and strides permutated with a new ordering.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="order"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static NDArray<T> Transpose<T>(this in NDArray<T> array, params int[] order)
            => array.Transpose((ReadOnlySpan<int>)order);

        public static NDArray<T> Transpose<T>(this in NDArray<T> array, in ReadOnlySpan<int> order)
        {
            var newStrides = new int[array.Rank];
            ArrayHelpers.GetPermuted(array.Strides, newStrides, order);

            var newShape = array.Shape.ToArray();
            ArrayHelpers.GetPermuted(array.Shape, newShape, order);

            var flags = array._flags;

            // set the contiguity flag if the strides are ordered
            if (ArrayHelpers.AllDecreasing(newStrides))
                flags |= ArrayFlags.IS_CONTIGUOUS;
            else
                flags &= ~ArrayFlags.IS_CONTIGUOUS;

            return new NDArray<T>(array, newShape, newStrides, array.Count, flags);
        }
    }


}