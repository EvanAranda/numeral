using System;
using Numeral.Internals;
using Numeral.Iterators;

namespace Numeral
{

    public static partial class Tensor
    {
        /// <summary>
        /// Creates a rank-1 tensor of linear indices where mask is true
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static Tensor<int> ArgWhere(this Tensor<bool> mask)
        {
            var indices = new int[mask.Count];
            int i = 0, j = 0;

            using var iter = mask.GetIterator();
            Span<bool> buf;

            do
            {
                buf = iter.GetData();
                for (var k = 0; k < buf.Length; k++, j++)
                    if (buf[k]) indices[i++] = j;
            }
            while (iter.MoveNext());

            return indices[..i];
        }

        public static Tensor<int> UnravelIndex(this Tensor<int> linearIndex, in ReadOnlySpan<int> shape)
        {
            Span<int> strides = stackalloc int[shape.Length];
            shape.GetStrides(strides);

            var resultRank = linearIndex.Rank == 1 ? 2 : linearIndex.Rank;
            Span<int> resultShape = stackalloc int[resultRank];

            if (linearIndex.Rank == 1)
            {
                resultShape[0] = linearIndex.Count;
                resultShape[1] = shape.Length;
            }
            else
            {
                linearIndex.Shape[..^1].CopyTo(resultShape);
                resultShape[^1] = shape.Length;
            }

            var result = Dense.Zeros<int>(resultShape);

            using var li = linearIndex.GetIterator(0);
            using var ri = result.GetIterator(1);

            do
            {
                var index = li.GetData()[0];
                IndexHelpers.GetIndices(strides, index, ri.GetData());

                li.MoveNext();
            }
            while (ri.MoveNext());

            return result;
        }

        public static Tensor<int> RavelMultiIndex(this Tensor<int> multiIndex, in ReadOnlySpan<int> shape)
        {
            if (multiIndex.Rank < 2)
                throw new ArgumentException("multiIndex.Rank < 2", nameof(multiIndex));

            Span<int> strides = stackalloc int[shape.Length];
            shape.GetStrides(strides);

            var result = Dense.Zeros<int>(multiIndex.Shape[..^1]);

            using var mi = multiIndex.GetIterator(0);
            using var ri = result.GetIterator(1);

            do
            {
                ri.GetData()[0] = IndexHelpers.GetIndex(strides, mi.GetData());
                mi.MoveNext();
            }
            while (ri.MoveNext());

            return result;

        }
    }
}
