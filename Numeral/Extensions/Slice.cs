using System;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        /// <summary>
        /// Produces a subview array of an array. W
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="slices"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static NDArray<T> Slice<T>(this in NDArray<T> arr, params Slice[] slices)
        {
            // if (slices.Length == 1)
            // {
            //     var (o, c) = slices[0].Range.GetOffsetAndLength(arr.Shape[0]);
            //     var newCount = arr.Count / arr.Shape[0] * c;
            //     return new NDArray<T>(
            //         arr._buffer.Slice(o * arr.Strides[0], newCount),
            //         arr._shape[1..],
            //         arr._strides[1..],
            //         newCount,
            //         arr._flags | ArrayFlags.IS_SLICE
            //     );
            // }

            Span<int> startOffsets = stackalloc int[arr.Rank];
            Span<int> endOffsets = stackalloc int[arr.Rank];
            Span<int> shape = stackalloc int[arr.Rank];
            Span<int> strides = stackalloc int[arr.Rank];
            var count = 1;

            var j = 0;
            for (var i = 0; i < arr.Rank; i++)
            {
                if (i < slices.Length)
                {
                    var (o, c) = slices[i].Range.GetOffsetAndLength(arr.Shape[i]);
                    startOffsets[i] = o;
                    endOffsets[i] = o + c - 1;

                    if (c > 1)
                    {
                        shape[j] = c;
                        strides[j] = arr.Strides[i];
                        count *= c;
                        j++;
                    }
                }
                else
                {
                    var c = arr.Shape[i];
                    startOffsets[i] = 0;
                    endOffsets[i] = c - 1;
                    shape[j] = c;
                    strides[j] = arr.Strides[i];
                    count *= c;
                    j++;
                }
            }

            var startOffset = IndexHelpers.GetIndex(arr.Strides, startOffsets);
            var endOffset = IndexHelpers.GetIndex(arr.Strides, endOffsets);

            // slice did not change number of elements
            // therefore nothing else about the array changes
            if (count == arr.Count)
                return arr;

            var flags = arr._flags;

            // if the buffer is a different size than the count
            // this array can't be contiguous
            if (endOffset - startOffset + 1 != count)
                flags &= ~ArrayFlags.IS_CONTIGUOUS;
            else
                flags |= ArrayFlags.IS_CONTIGUOUS;

            flags |= ArrayFlags.IS_SLICE;

            return new NDArray<T>(
                arr._buffer[startOffset..(endOffset + 1)],
                shape.Slice(0, j).ToArray(),
                strides.Slice(0, j).ToArray(),
                count,
                flags);
        }
    }

}