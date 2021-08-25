namespace Numeral
{
    using System;
    using System.Threading.Tasks;
    using Numeral.Internals;

    public static partial class Tensor
    {
        public static UnaryOperation<T, R> AsUnaryOp<T, R>(this Func<T, R> func)
        {
            void op(in ReadOnlySpan<T> xx, in Span<R> result)
            {
                for (var i = 0; i < xx.Length; i++)
                    result[i] = func(xx[i]);
            };

            return new UnaryOperation<T, R>(op);
        }

        public static BinaryOperation<X, Y, R> AsBinaryOp<X, Y, R>(this Func<X, Y, R> func)
        {
            void op(in ReadOnlySpan<X> xx, in ReadOnlySpan<Y> yy, in Span<R> result)
            {
                for (var i = 0; i < xx.Length; i++)
                    result[i] = func(xx[i], yy[i]);
            };

            return new BinaryOperation<X, Y, R>(op);
        }

        public static ReductionOperation<T, R> AsReduceOp<T, R>(this Func<T, R, R> func, R seedValue = default)
        {
            void op(in ReadOnlySpan<T> xx, in Span<R> result)
            {
                R tmp = seedValue;
                for (var i = 0; i < xx.Length; i++)
                    tmp = func(xx[i], tmp);
                result[0] = tmp;
            };

            return new ReductionOperation<T, R>(op);
        }

        public static AccumulateOperation<T, R> AsAccumOp<T, R>(this Func<T, R, R> func, R seedValue = default)
        {
            void op(in ReadOnlySpan<T> xx, in Span<R> result)
            {
                var tmp = seedValue;
                for (var i = 0; i < xx.Length; i++)
                {
                    tmp = func(xx[i], tmp);
                    result[i] = tmp;
                }
            };

            return new AccumulateOperation<T, R>(op);
        }

        public static Tensor<R> Apply<T, R>(this Tensor<T> x, Func<T, R> func)
            => func.AsUnaryOp().Call(x);

        public static Tensor<R> Apply<X, Y, R>(this Tensor<X> x, Tensor<Y> y, Func<X, Y, R> func)
            => func.AsBinaryOp().Call(x, y);

        public static Tensor<R> Reduce<T, R>(this Tensor<T> x, Func<T, R, R> reducer, int axis = -1, R seedValue = default)
            => reducer.AsReduceOp(seedValue).Call(x, axis);

        public static Tensor<R> Accum<T, R>(this Tensor<T> x, Func<T, R, R> accumulator, int axis, R seedValue = default)
            => accumulator.AsAccumOp(seedValue).Call(x, axis);
    }
}