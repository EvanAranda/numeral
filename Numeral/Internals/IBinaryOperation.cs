using Numeral.Internals;

namespace Numeral
{
    public interface IBinaryOperation<X, Y, R>
    {
        Tensor<R> AllocateResult(Tensor<X> x, Tensor<Y> y);
        Tensor<R> Call(Tensor<X> x, Tensor<Y> y, Tensor<R> result);
    }

    public interface ICpuBinaryOperation<X, Y, R> : IBinaryOperation<X, Y, R>
    {
        BinaryOp<X, Y, R> Op { get; }
    }
}
