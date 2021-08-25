namespace Numeral.Expressions
{
    public class TemporaryResult<T> : ExpressionResult<T>
    {
        private readonly TemporaryTensor<T> _temporaryTensor;

        internal TemporaryResult(TemporaryTensor<T> temporaryTensor)
        {
            _temporaryTensor = temporaryTensor;
        }

        public override Tensor<T> Result => _temporaryTensor;

        public override void Dispose()
        {
            _temporaryTensor.Dispose();
        }
    }
}
