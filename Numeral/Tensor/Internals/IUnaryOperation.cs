using System;

namespace Numeral.Internals
{
    public interface IUnaryOperation<X, R>
    {
        Tensor<R> Call(Tensor<X> x, Tensor<R> result);
    }

    public delegate void UnaryOp<X, Y>(in ReadOnlySpan<X> x, in Span<Y> y);

}
