using System;
using System.Runtime.CompilerServices;
using Numeral.Internals;

namespace Numeral.Iterators
{

    public static class IteratorHelpers
    {
        #region Stride & Shape Helpers

        public static void GetStrides(this in ReadOnlySpan<int> shape, in Span<int> strides)
        {
            var stride = 1;
            for (var i = shape.Length - 1; i >= 0; i--)
            {
                strides[i] = stride;
                stride *= shape[i];
            }
        }

        public static int[] GetStrides(in ReadOnlySpan<int> shape)
        {
            var strides = new int[shape.Length];
            GetStrides(shape, strides);
            return strides;
        }

        public static unsafe void GetBackstrides(in ReadOnlySpan<int> shape, in ReadOnlySpan<int> strides, in Span<int> backstrides)
        {
            for (var i = 0; i < shape.Length; i++)
                backstrides[i] = strides[i] * (shape[i] - 1);
        }
        public static unsafe void GetBackstrides(in ReadOnlySpan<int> shape, int* strides, int* backstrides)
        {
            for (var i = 0; i < shape.Length; i++)
                backstrides[i] = strides[i] * (shape[i] - 1);
        }

        /// <summary>
        /// Updates the totalShape to envelope the array.Shape
        /// </summary>
        /// <param name="totalShape"></param>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        public static void UpdateResultShape(in Span<int> totalShape, in ReadOnlySpan<int> arrayShape)
        {
            if (arrayShape.Length > totalShape.Length)
                throw new Exception();

            int totalSize, arraySize;
            for (var i = 0; i < arrayShape.Length; i++)
            {
                var i_full = totalShape.Length - 1 - i;
                var i_arr = arrayShape.Length - 1 - i;

                totalSize = totalShape[i_full];
                arraySize = arrayShape[i_arr];

                if (arraySize > totalSize)
                    totalShape[i_full] = arraySize;
            }
        }

        public static void UpdateResultShape(in Span<int> totalShape, in ReadOnlySpan<int> arrayShape, in ReadOnlySpan<int> arrayAxisOrder)
        {
            // var rank = Math.Max(arrayShape.Length, arrayAxisOrder.Length);
            var rank = totalShape.Length;

            for (var i = 0; i < rank; i++)
            {
                var i_full = totalShape.Length - 1 - i;
                var i_arr = arrayShape.Length - 1 - i;
                var i_ord = arrayAxisOrder.Length - 1 - i;

                var totalSize = totalShape[i_full];
                int arraySize;

                if (i_ord >= 0)
                {
                    var axis = arrayAxisOrder[i_ord];

                    if (axis >= 0)
                    {
                        arraySize = arrayShape[axis];
                        totalShape[i_full] = Math.Max(totalSize, arraySize);
                        continue;
                    }
                }
                else if (i_arr >= 0)
                {
                    arraySize = arrayShape[i_arr];
                    totalShape[i_full] = Math.Max(totalSize, arraySize);
                }
            }
        }

        #endregion

        public static bool CheckDimension(in int totalSize, in int arraySize)
        {
            return totalSize == arraySize ||
                arraySize == 1 && totalSize >= 1;
        }

    }
}
