namespace Numeral.Internals
{
    public interface IAccumulateOperation<X, R>
    {
        Tensor<R> Call(Tensor<X> x, Tensor<R> result, int axis);
    }
}
