using System;
using Numeral.Internals;

namespace Numeral
{
    public class Cast<T, R> : UnaryOperation<T, R>
    {
        internal Cast() : base(Op) { }
        public static readonly Cast<T, R> Instance = new();

        public static void Op(in ReadOnlySpan<T> x, in Span<R> result)
        {
            var dtype = DTypes.GetDType<R>();
            for (var i = 0; i < x.Length; i++)
                result[i] = dtype.Cast(x[i]);
        }
    }
}
