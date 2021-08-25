using System;
using Numeral.Iterators;

namespace Numeral.Internals
{
    public static class Dispatcher
    {
        public static Tensor<R> Call<X, Y, R>(this IBinaryOperation<X, Y, R> op, Tensor<X> x, Tensor<Y> y)
        {
            return op.Call(x, y, op.AllocateResult(x, y));
        }

        public static Tensor<R> Call<X, R>(this IUnaryOperation<X, R> op, Tensor<X> x)
        {
            return op.Call(x, op.AllocateResult(x));
        }

        public static Tensor<R> Call<X, R>(this IReductionOperation<X, R> op, Tensor<X> x, int axis)
        {
            return op.Call(x, op.AllocateResult(x, axis), axis);
        }

        /// <summary>
        /// Reduce a tensor down to a scalar
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="op"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static R Call<X, R>(this IReductionOperation<X, R> op, Tensor<X> x)
        {
            return op.Call(x);
        }

        /// <summary>
        /// Applies an accumulation operation across an axis
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="op"></param>
        /// <param name="x"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Tensor<R> Call<X, R>(this IAccumulateOperation<X, R> op, Tensor<X> x, int axis)
        {
            return op.Call(x, op.AllocateResult(x), axis);
        }

    }
}
