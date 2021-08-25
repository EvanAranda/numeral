using System;
using Numeral.Iterators;

namespace Numeral.Internals
{
    public delegate void BinaryOp<X, Y, R>(in ReadOnlySpan<X> x, in ReadOnlySpan<Y> y, in Span<R> result);

    public class BinaryOperation<X, Y, R> : ICpuBinaryOperation<X, Y, R>
    {
        private readonly BinaryOp<X, Y, R> _op;

        public BinaryOp<X, Y, R> Op => _op;

        internal BinaryOperation(
            BinaryOp<X, Y, R> op)
        {
            _op = op;
        }

        public Tensor<R> AllocateResult(Tensor<X> x, Tensor<Y> y)
        {
            var rank = Math.Max(x.Rank, y.Rank);
            Span<int> shape = stackalloc int[rank];
            IteratorHelpers.UpdateResultShape(shape, x.Shape);
            IteratorHelpers.UpdateResultShape(shape, y.Shape);
            return x.Factory.Zeros<R>(shape);
        }

        public Tensor<R> Call(Tensor<X> x, Tensor<Y> y, Tensor<R> result)
            => Call(x, y, result, _op);

        public static Tensor<R> Call(
            Tensor<X> x, Tensor<Y> y, Tensor<R> result, BinaryOp<X, Y, R> op)
        {
            using var xi = x.GetIterator(result.Shape);
            using var yi = y.GetIterator(result.Shape);
            using var zi = result.GetIterator();

            do
            {
                var resultBuffer = zi.GetData();
                op(xi.GetData(), yi.GetData(), resultBuffer);

                if (zi.Flags == IteratorFlags.Buffered)
                    zi.PutData(resultBuffer);

                xi.MoveNext();
                yi.MoveNext();
            }
            while (zi.MoveNext());

            return result;
        }

    }

}
