using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;

namespace Numeral
{
    public static class ArrayHelpers
    {
        /// <summary>
        /// Copies values from values to result except the value at index
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="reducedShape"></param>
        /// <param name="axis"></param>
        public static void GetReducedShape(this in ReadOnlySpan<int> shape, in Span<int> reducedShape, int axis)
        {
            for (int i = 0, j = 0; i < shape.Length; i++)
            {
                if (i == axis) continue;
                reducedShape[j++] = shape[i];
            }
        }

        /// <summary>
        /// Copies values to result in order, but with the value at index placed 
        /// at the end.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="order"></param>
        /// <param name="index"></param>
        public static void GetReducedOrder(this in Span<int> order, int axis)
        {
            order[^1] = axis;
            for (int i = 0, j = 0; i < order.Length; i++)
            {
                if (i == axis) continue;
                order[j++] = i;
            }
        }

        public static unsafe int GetProductSum(int* array, int length, int startAtDimension = 0)
        {
            if (length == 0)
                return 0;

            var x = 1;
            for (var i = startAtDimension; i < length; i++)
                x *= array[i];
            return x;
        }

        public static int GetProductSum(this in ReadOnlySpan<int> arr)
        {
            if (arr.Length == 0) return 0;

            var p = arr[0];
            for (var i = 1; i < arr.Length; i++)
                p *= arr[i];
            return p;
        }

        public static bool IsValidShape(this in ReadOnlySpan<int> arr)
        {
            foreach (var x in arr)
                if (x < 1) return false;
            return true;
        }

        public static void GetPermuted<T>(this in ReadOnlySpan<T> input, in Span<T> array, in ReadOnlySpan<int> order)
        {
            if (array.Length != order.Length)
                throw new Exception();

            for (var i = 0; i < array.Length; i++)
                array[i] = input[order[i]];
        }

        public static bool AllIncreasing(this in ReadOnlySpan<int> arr)
        {
            var x = arr[0];
            for (var i = 0; i < arr.Length; i++)
                if (x > arr[i]) return false;
            return true;
        }

        public static bool AllDecreasing(this in ReadOnlySpan<int> arr)
        {
            var x = arr[0];
            for (var i = 0; i < arr.Length; i++)
                if (x < arr[i]) return false;
            return true;
        }
    }

}
