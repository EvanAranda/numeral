namespace Numeral
{
    using System;
    using Numeral.Evaluators;

    public static partial class NDArrayExtensions
    {
        public static NDArray<R> Apply<T, R>(this in NDArray<T> x, Func<T, R> op)
        {
            void OpWrapper(in ReadOnlySpan<T> xx, in Span<R> result)
            {
                for (var i = 0; i < xx.Length; i++)
                    result[i] = op(xx[i]);
            };

            return Evaluate.ElementWiseUnaryOp<T, R>(x, OpWrapper);
        }

        public static NDArray<R> Reduce<T, R>(this in NDArray<T> x, int axis, Func<T, R, R> reducer)
            where R : unmanaged
        {
            void OpWrapper(in ReadOnlySpan<T> xx, in Span<R> result)
            {
                R tmp = default;
                for (var i = 0; i < xx.Length; i++)
                    tmp = reducer(xx[i], tmp);
                result[0] = tmp;
            };

            return Evaluate.ReduceUnaryOp<T, R>(x, OpWrapper, axis);
        }

        public static NDArray<T> Accum<T>(this in NDArray<T> x, int axis, Func<T, T, T> accumulator)
        {
            void OpWrapper(in ReadOnlySpan<T> xx, in Span<T> result)
            {
                var tmp = xx[0];
                result[0] = tmp;
                for (var i = 1; i < xx.Length; i++)
                {
                    tmp = accumulator(xx[i], tmp);
                    result[i] = tmp;
                }
            };

            return Evaluate.AccumulateUnaryOp<T, T>(x, OpWrapper, axis);
        }
    }
}