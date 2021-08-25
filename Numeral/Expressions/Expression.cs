using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Numeral.Internals;

namespace Numeral.Expressions
{
    /// <summary>
    /// Building block for lazy tensor computation expressions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Expression<T>
    {
        /// <summary>
        /// Evaluate the expression and return the result as a Tensor
        /// </summary>
        /// <param name="isResult">Indicates if this is the outer-most 
        /// expression, which can be used to determine when temporary tensors
        /// should be used.
        /// </param>
        /// <returns></returns>
        public abstract ExpressionResult<T> Evaluate(bool isResult = false);

        public static implicit operator Tensor<T>(Expression<T> expression)
            => expression.Evaluate(isResult: true);

        public static implicit operator Expression<T>(Tensor<T> tensor)
            => (TensorArgument<T>)tensor;
    }

    public class TensorArgument<T> : Expression<T>
    {
        public TensorArgument(Tensor<T> tensor)
        {
            Tensor = tensor;
        }

        public Tensor<T> Tensor { get; }

        public override ExpressionResult<T> Evaluate(bool isResult = false)
            => new PermanentResult<T>(Tensor);

        public static implicit operator TensorArgument<T>(Tensor<T> tensor)
            => new(tensor);
    }

    public class BinaryExpression<T> : Expression<T>
    {
        public BinaryExpression(Expression<T> x, Expression<T> y, IBinaryOperation<T, T, T> op)
        {
            X = x;
            Y = y;
            Op = op;
        }

        public Expression<T> X { get; }
        public Expression<T> Y { get; }
        public IBinaryOperation<T, T, T> Op { get; }

        public override ExpressionResult<T> Evaluate(bool isResult = true)
        {
            // x and y are intermediary here results
            using var x = X.Evaluate(false);
            using var y = Y.Evaluate(false);

            ExpressionResult<T> result = isResult switch
            {
                true => Op.AllocateResult(x, y),
                false => throw new NotImplementedException()
            };


            Op.Call(x, y, result);

            return result;
        }
    }

    public class UnaryExpression<T> : Expression<T>
    {
        public UnaryExpression(Expression<T> x, IUnaryOperation<T, T> op)
        {
            X = x;
            Op = op;
        }

        public Expression<T> X { get; }
        public IUnaryOperation<T, T> Op { get; }

        public override ExpressionResult<T> Evaluate(bool isResult = false)
        {
            // x and y are intermediary results
            using var x = X.Evaluate(false);

            ExpressionResult<T> result = isResult switch
            {
                true => Op.AllocateResult(x),
                false => throw new NotImplementedException()
            };

            Op.Call(x, result);

            return result;
        }
    }
}