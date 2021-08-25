namespace Numeral.Internals
{
    public interface IReductionOperation<X, R>
    {
        Tensor<R> AllocateResult(Tensor<X> x, int axis);
        R Call(Tensor<X> x);
        Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis);
    }
}
