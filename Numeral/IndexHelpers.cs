using System;
using System.Threading.Tasks;

namespace Numeral
{

    public static class IndexHelpers
    {
        public static int GetIndex(
            in ReadOnlySpan<int> strides,
            in ReadOnlySpan<int> indices,
            int startAtDimension = 0)
        {
            var index = 0;
            for (var i = startAtDimension; i < strides.Length; i++)
                index += strides[i] * indices[i];
            return index;
        }

        public static unsafe int GetIndex(
            int* strides,
            int* indices,
            int length,
            int startAtDimension = 0)
        {
            var index = 0;
            for (var i = startAtDimension; i < length; i++)
                index += strides[i] * indices[i];
            return index;
        }

        public static void GetIndices(in ReadOnlySpan<int> strides, int index, in Span<int> indices)
        {
            for (var i = 0; i < strides.Length; i++)
                indices[i] = Math.DivRem(index, strides[i], out index);
        }
    }
}
