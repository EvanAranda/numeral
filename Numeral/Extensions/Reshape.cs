using System;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        /// <summary>
        /// Reshapes the view over the buffer. Whenever possible, no copies
        /// are created.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="shape"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static NDArray<T> Reshape<T>(this in NDArray<T> arr, params int[] shape)
        {
            // the array is a view that cannot be reshaped
            // so a copy will be created
            // if (arr.Count != arr._buffer.Length)
            if (!arr.IsContiguous())
            {
                if (Options.NoHiddenCopies)
                    throw new NotSupportedException("this operation requires returning a copy");

                return arr.Copy().Reshape(shape);
            }

            var partialCount = 1;
            var placeholderDim = -1;
            for (var i = 0; i < shape.Length; i++)
            {
                if (shape[i] == -1)
                {
                    if (placeholderDim != -1)
                        throw new Exception("only 1 placeholder value (-1) is allowed");

                    placeholderDim = i;
                    continue;
                }

                partialCount *= shape[i];
            }

            if (placeholderDim != -1)
            {
                shape[placeholderDim] = arr.Count / partialCount;
            }
            else
            {
                var count = ArrayHelpers.GetProductSum(shape);
                if (arr.Count != count)
                    throw new Exception();
            }

            var strides = IteratorHelpers.GetStrides(shape);
            return new NDArray<T>(arr._buffer, shape, strides, arr.Count, ArrayFlags.IS_CONTIGUOUS);
        }
    }

}