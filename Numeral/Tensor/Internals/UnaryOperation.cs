using Numeral.Iterators;
using System;
using System.Buffers;

namespace Numeral.Internals
{
    public class UnaryOperation<X, R> : IUnaryOperation<X, R>
    {
        private readonly UnaryOp<X, R> _op;


        public UnaryOperation(UnaryOp<X, R> op)
        {
            _op = op;
        }

        public Tensor<R> Call(Tensor<X> x, Tensor<R> result)
        {
            if (x is DenseTensor<X> dx &&
                result is DenseTensor<R> dr &&
                dx._flags.IsContiguous() && dr._flags.IsContiguous() &&
                dx.Shape.SequenceEqual(dr.Shape))
            {
                _op(dx, dr);
                return result;
            }

            using var xi = x.GetIterator();
            using var zi = result.GetIterator();

            do
            {
                var resultBuffer = zi.GetData();
                _op(xi.GetData(), resultBuffer);

                if (zi.Flags == IteratorFlags.Buffered)
                    zi.PutData(resultBuffer);

                xi.MoveNext();
            }
            while (zi.MoveNext());

            return result;
        }
    }

}
