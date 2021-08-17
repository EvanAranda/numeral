using System;
using Numeral.Evaluators;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        private static void Copy<T>(in ReadOnlySpan<T> x, in Span<T> result)
            => x.CopyTo(result);

        public static NDArray<T> Copy<T>(this in NDArray<T> array)
        {
            return Evaluate.ElementWiseUnaryOp<T, T>(array, Copy);
        }
    }

}