using System;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class Tensor
    {
        public static Tensor<T> Get<T>(this Tensor<T> tensor, Tensor<int> indices)
        {
            var indexSize = indices.Rank == 1 ? 1 : indices.Shape[^1];
            var indexRank = indices.Rank == 1 ? 0 : 1;
            var outerLoopRank = Math.Max(1, indices.Rank - 1);
            var opRank = tensor.Rank - indexSize;

            var resultRank = outerLoopRank + opRank;
            Span<int> resultShape = stackalloc int[resultRank];

            indices.Shape[..outerLoopRank].CopyTo(resultShape);
            tensor.Shape.Slice(indexSize, opRank).CopyTo(resultShape[..outerLoopRank]);

            var result = Dense.Zeros<T>(resultShape);

            using var ti = tensor.GetIterator(opRank);
            using var ii = indices.GetIterator(indexRank);
            using var ri = result.GetIterator(opRank);

            Span<int> strides = stackalloc int[tensor.Rank];
            IteratorHelpers.GetStrides(tensor.Shape, strides);

            var offset = 0;

            do
            {
                offset = IndexHelpers.GetIndex(strides, ii.GetData());
                ti.MoveTo(offset);
                ti.GetData().CopyTo(ri.GetData());

                ii.MoveNext();
            }
            while (ri.MoveNext());

            return result;
        }

        public static Tensor<T> Set<T>(this Tensor<T> tensor, Tensor<int> indices, Tensor<T> values)
        {
            var indexSize = indices.Rank == 1 ? 1 : indices.Shape[^1];

            if (indexSize > tensor.Rank)
                throw new Exception("indexSize > tensor.Rank");

            var indexRank = indices.Rank == 1 ? 0 : 1;
            var outerLoopRank = Math.Max(1, indices.Rank - 1);

            var indicesOuterShape = indices.Shape[..outerLoopRank];
            var valuesOuterShape = values.Shape[..outerLoopRank];

            if (!indicesOuterShape.SequenceEqual(valuesOuterShape))
                throw new Exception();

            var opRank = tensor.Rank - indexSize;
            var tensorOpShape = tensor.Shape[indexSize..];
            var valuesOpShape = values.Shape[outerLoopRank..];

            if (!tensorOpShape.SequenceEqual(valuesOpShape))
                throw new Exception("");

            using var ti = tensor.GetIterator(opRank);
            using var ii = indices.GetIterator(indexRank);
            using var vi = values.GetIterator(opRank);

            Span<int> strides = stackalloc int[tensor.Rank];
            IteratorHelpers.GetStrides(tensor.Shape, strides);

            var offset = 0;

            do
            {
                offset = IndexHelpers.GetIndex(strides, ii.GetData());
                ti.MoveTo(offset);

                if (ti.Flags == IteratorFlags.Buffered)
                    ti.PutData(vi.GetData());
                else
                    vi.GetData().CopyTo(ti.GetData());
            }
            while (ii.MoveNext() && vi.MoveNext());

            return tensor;
        }

        public static Tensor<T> Set<T>(this Tensor<T> tensor, Tensor<T> values)
            => Copy<T>.Instance.Call(values, tensor);
    }
}
