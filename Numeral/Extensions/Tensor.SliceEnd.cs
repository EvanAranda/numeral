using System;

namespace Numeral
{
    public static partial class Tensor
    {
        public static Tensor<T> SliceEnd<T>(this Tensor<T> tensor, params Slice[] endSlices)
            => SliceEnd(tensor, endSlices);

        public static Tensor<T> SliceEnd<T>(this Tensor<T> tensor, in ReadOnlySpan<Slice> endSlices)
        {
            Span<Slice> allSlices = stackalloc Slice[tensor.Rank];
            allSlices.Fill(Slice.All);
            endSlices.CopyTo(allSlices[(tensor.Rank - endSlices.Length)..]);
            return tensor.Slice(allSlices);
        }
    }
}