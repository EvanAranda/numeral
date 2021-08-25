using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Numeral.Internals;

namespace Numeral
{
    public class Copy<T> : UnaryOperation<T, T>
    {
        internal Copy() : base(Op) { }
        public static readonly Copy<T> Instance = new();
        public static void Op(in ReadOnlySpan<T> x, in Span<T> result)
            => x.CopyTo(result);
    }
}