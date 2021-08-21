namespace Numeral.Internals
{
    public interface IReductionOperation<X, R>
    {
        R Call(Tensor<X> x);
        Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis);
    }
}
