using System;
using System.Buffers;
using Numeral.Iterators;

namespace Numeral.Internals
{
    public class UnaryOperation<X, R> : IUnaryOperation<X, R>
    {
        private readonly UnaryOp<X, R> _op;


        public UnaryOperation(UnaryOp<X, R> op)
        {
            _op = op;
        }

        public Tensor<R> Call(Tensor<X> x, Tensor<R> result) => Call(x, result, _op);

        public static Tensor<R> Call(Tensor<X> x, Tensor<R> result, UnaryOp<X, R> op)
        {
            if (x is DenseTensor<X> dx &&
                result is DenseTensor<R> dr &&
                dx._flags.IsContiguous() && dr._flags.IsContiguous() &&
                dx.Shape.SequenceEqual(dr.Shape))
            {
                op(dx, dr);
                return result;
            }

            using var xi = x.GetIterator();
            using var zi = result.GetIterator();

            do
            {
                var resultBuffer = zi.GetData();
                op(xi.GetData(), resultBuffer);

                if (zi.Flags == IteratorFlags.Buffered)
                    zi.PutData(resultBuffer);

                xi.MoveNext();
            }
            while (zi.MoveNext());

            return result;
        }

        public Tensor<R> AllocateResult(Tensor<X> x)
        {
            return x.Factory.ZerosLike<X, R>(x);
        }
    }

}
