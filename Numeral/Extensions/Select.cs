using System;
using Numeral.Evaluators;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        public static NDArray<T> Select<T>(this NDArray<T> x, in NDArray<int> indices)
        {
            var indexSize = indices.Shape[^1];
            if (!(indexSize == 1 || indexSize == x.Rank))
                throw new Exception("indices.Shape[^1] != x.Rank or 1");

            var resultRank = Math.Max(1, indices.Rank - 1);
            var resultShape = indices.Shape[..resultRank];
            var result = NDArray.Empty<T>(resultShape.ToArray());

            var isFlatIndex = indexSize == 1;
            var ii = indices.GetIterator(indices.Shape, isFlatIndex ? 0 : indexSize);
            var ri = result.GetIterator(resultShape, 0);

            Evaluate.Run(ii, ri, (in ReadOnlySpan<int> indices, in Span<T> result) =>
            {
                if (isFlatIndex)
                    result[0] = x.Flat(indices[0]);
                else
                    result[0] = x[indices];
            });

            return result;
        }

    }
}