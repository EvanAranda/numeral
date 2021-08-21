namespace Numeral
{
    public interface IBinaryOperation<X, Y, R>
    {
        Tensor<R> Call(Tensor<X> x, Tensor<Y> y, Tensor<R> result);
        //void Apply(Tensor<X> x, Y y, Tensor<R> result);
        //void Apply(X x, Tensor<Y> y, Tensor<R> result);
    }

}
