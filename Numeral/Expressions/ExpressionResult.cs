using System;
using System.Threading.Tasks;

namespace Numeral.Expressions
{
    public abstract class ExpressionResult<T> : IDisposable
    {
        public abstract Tensor<T> Result { get; }
        public abstract void Dispose();

        public static implicit operator Tensor<T>(ExpressionResult<T> result) => result.Result;
        public static implicit operator ExpressionResult<T>(Tensor<T> tensor) => tensor;
    }
}
