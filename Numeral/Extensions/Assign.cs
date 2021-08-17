using System;
using Numeral.Evaluators;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        public static NDArray<T> Assign<T>(this in NDArray<T> x, in NDArray<bool> where, in NDArray<T> values)
        {
            if (!x.Shape.SequenceEqual(where.Shape))
                throw new ArgumentException("assert failed: x.Shape == where.Shape", nameof(where));

            return Evaluate.ElementWiseBinaryOp(where, values, x.Copy(), AssignImpl);
        }

        public static NDArray<T> Assign<T>(this in NDArray<T> x, in NDArray<bool> where, T value)
        {
            if (!x.Shape.SequenceEqual(where.Shape))
                throw new ArgumentException("assert failed: x.Shape == where.Shape", nameof(where));

            return Evaluate.ElementWiseBinaryOp(where, value, x.Copy(), AssignImpl);
        }
        public static NDArray<T> AssignInPlace<T>(this in NDArray<T> x, in NDArray<bool> where, in NDArray<T> values)
        {
            if (!x.Shape.SequenceEqual(where.Shape))
                throw new ArgumentException("assert failed: x.Shape == where.Shape", nameof(where));

            return Evaluate.ElementWiseBinaryOp(where, values, x, AssignImpl);
        }

        public static NDArray<T> AssignInPlace<T>(this in NDArray<T> x, in NDArray<bool> where, T value)
        {
            if (!x.Shape.SequenceEqual(where.Shape))
                throw new ArgumentException("assert failed: x.Shape == where.Shape", nameof(where));

            return Evaluate.ElementWiseBinaryOp(where, value, x, AssignImpl);
        }


        private static void AssignImpl<T>(in ReadOnlySpan<bool> w, in ReadOnlySpan<T> v, in Span<T> r)
        {
            for (var i = 0; i < w.Length; i++)
                if (w[i]) r[i] = v[i];
        }

        private static void AssignImpl<T>(in ReadOnlySpan<bool> w, T v, in Span<T> r)
        {
            for (var i = 0; i < w.Length; i++)
                if (w[i]) r[i] = v;
        }
    }
}
