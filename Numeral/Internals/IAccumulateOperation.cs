namespace Numeral.Internals
{
    public interface IAccumulateOperation<X, R>
    {
        Tensor<R> AllocateResult(Tensor<X> x);
        Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis);
    }
}
