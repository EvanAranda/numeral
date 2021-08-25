namespace Numeral.Expressions
{
    public class PermanentResult<T> : ExpressionResult<T>
    {
        internal PermanentResult(Tensor<T> tensor)
        {
            Result = tensor;
        }

        public override Tensor<T> Result { get; }

        public override void Dispose() { }

        public static implicit operator PermanentResult<T>(Tensor<T> tensor)
            => new(tensor);
    }
}
