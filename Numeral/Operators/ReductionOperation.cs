using System;
using System.Runtime.CompilerServices;

namespace Numeral.Internals
{
    public class ReductionOperation<X, R> : IReductionOperation<X, R>
    {
        private readonly UnaryOp<X, R> _op;

        internal ReductionOperation(UnaryOp<X, R> op)
        {
            _op = op;
        }

        public Tensor<R> AllocateResult(Tensor<X> x, int axis)
        {
            if (axis < 0)
                axis = x.Rank + axis;

            if (axis < 0)
                throw new ArgumentOutOfRangeException(nameof(axis), "axis + x.Rank < 0");

            Span<int> shape = stackalloc int[x.Rank - 1];
            ArrayHelpers.GetReducedShape(x.Shape, shape, axis);

            return x.Factory.Zeros<R>(shape);
        }

        public unsafe R Call(Tensor<X> x)
        {
            if (x is DenseTensor<X> dense)
            {
                R result = default;
                var tmp = new Span<R>(Unsafe.AsPointer(ref result), 1);
                _op(dense, tmp);
                return result;
            }

            throw new NotImplementedException();
        }

        public Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis)
            => Call(x, result, axis, _op);

        public static Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis, UnaryOp<X, R> op)
        {
            Span<int> order = stackalloc int[x.Rank];
            order.GetReducedOrder(axis);

            using var xi = x.GetIterator(x.Shape, order, 1);
            using var zi = result.GetIterator(0);

            do
            {
                op(xi.GetData(), zi.GetData());
                xi.MoveNext();
            }
            while (zi.MoveNext());

            return result;
        }

    }
}
