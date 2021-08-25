using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Numeral.Internals
{

    public interface IUnaryOperation<X, R>
    {
        Tensor<R> AllocateResult(Tensor<X> x);
        Tensor<R> Call(Tensor<X> x, Tensor<R> result);
    }

    public delegate void UnaryOp<X, Y>(in ReadOnlySpan<X> x, in Span<Y> y);

}
