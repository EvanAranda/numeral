using System;

namespace Numeral.Internals
{
    public class AccumulateOperation<X, R> : IAccumulateOperation<X, R>
    {
        private readonly UnaryOp<X, R> _op;

        internal AccumulateOperation(UnaryOp<X, R> op)
        {
            _op = op;
        }

        public Tensor<R> AllocateResult(Tensor<X> x)
        {
            return x.Factory.ZerosLike<X, R>(x);
        }

        public Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis)
        {
            Span<int> order = stackalloc int[x.Rank];
            order.GetReducedOrder(axis);

            using var xi = x.GetIterator(x.Shape, order, 1);
            using var zi = result.GetIterator(result.Shape, order, 1);

            do
            {
                var resultBuffer = zi.GetData();
                _op(xi.GetData(), resultBuffer);

                if (zi.Flags == Iterators.IteratorFlags.Buffered)
                    zi.PutData(resultBuffer);

                xi.MoveNext();
            }
            while (zi.MoveNext());

            return result;
        }
    }
}
