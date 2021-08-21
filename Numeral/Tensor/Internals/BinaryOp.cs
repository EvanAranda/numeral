using System;
using Numeral.Iterators;

namespace Numeral.Internals
{
    public delegate void BinaryOp<X, Y, R>(in ReadOnlySpan<X> x, in ReadOnlySpan<Y> y, in Span<R> result);

    public class BinaryOperation<X, Y, R> : IBinaryOperation<X, Y, R>
    {
        private readonly BinaryOp<X, Y, R> _op;

        internal BinaryOperation(
            BinaryOp<X, Y, R> op)
        {
            _op = op;
        }

        public Tensor<R> Call(Tensor<X> x, Tensor<Y> y, Tensor<R> result)
        {
            using var xi = x.GetIterator(result.Shape);
            using var yi = y.GetIterator(result.Shape);
            using var zi = result.GetIterator();

            do
            {
                var resultBuffer = zi.GetData();
                _op(xi.GetData(), yi.GetData(), resultBuffer);

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
