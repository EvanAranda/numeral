using System;
using Numeral.Iterators;

namespace Numeral.Internals
{
    public static class Dispatcher
    {
        public static Tensor<R> Call<X, Y, R>(this IBinaryOperation<X, Y, R> op, Tensor<X> x, Tensor<Y> y)
        {
            var rank = Math.Max(x.Rank, y.Rank);
            Span<int> shape = stackalloc int[rank];
            IteratorHelpers.UpdateResultShape(shape, x.Shape);
            IteratorHelpers.UpdateResultShape(shape, y.Shape);

            var result = x.Factory.Zeros<R>(shape.ToArray());
            return op.Call(x, y, result);
        }

        public static Tensor<R> Call<X, R>(this IUnaryOperation<X, R> op, Tensor<X> x)
        {
            var result = x.Factory.ZerosLike<X, R>(x);
            return op.Call(x, result);
        }

        public static Tensor<R> Call<X, R>(this IReductionOperation<X, R> op, Tensor<X> x, int axis)
        {
            if (axis < 0)
                axis = x.Rank + axis;

            if (axis < 0)
                throw new Exception();

            Span<int> shape = stackalloc int[x.Rank - 1];
            ArrayHelpers.GetReducedShape(x.Shape, shape, axis);

            var result = x.Factory.Zeros<R>(shape.ToArray());
            return op.Call(x, result, axis);
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
            var result = x.Factory.ZerosLike<X, R>(x);
            return op.Call(x, result, axis);
        }

    }
}
