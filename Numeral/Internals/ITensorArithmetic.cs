namespace Numeral.Internals
{
    public interface ITensorArithmetic<T>
    {
        IBinaryOperation<T, T, T> Add { get; }
        IBinaryOperation<T, T, T> Subtract { get; }
        IBinaryOperation<T, T, T> Divide { get; }
        IBinaryOperation<T, T, T> Multiply { get; }
        IBinaryOperation<T, T, T> Pow { get; }
        IBinaryOperation<T, T, T> Mod { get; }
        IBinaryOperation<T, T, T> BitAnd { get; }
        IBinaryOperation<T, T, T> BitOr { get; }
        IBinaryOperation<T, T, T> Xor { get; }
        IBinaryOperation<T, T, T> Maximum { get; }
        IBinaryOperation<T, T, T> Minimum { get; }
        IBinaryOperation<T, T, T> Log { get; }
        IBinaryOperation<T, T, T> Atan2 { get; }
        IBinaryOperation<T, T, bool> LessThan { get; }
        IBinaryOperation<T, T, bool> GreaterThan { get; }
        IBinaryOperation<T, T, bool> LessThanOrEqual { get; }
        IBinaryOperation<T, T, bool> GreaterThanOrEqual { get; }
        IBinaryOperation<T, T, bool> ArrayEqual { get; }
        IBinaryOperation<T, T, bool> ArrayNotEqual { get; }
        IUnaryOperation<T, T> Negate { get; }
        IUnaryOperation<T, T> Abs { get; }
        IUnaryOperation<T, T> BitNot { get; }
        IUnaryOperation<T, T> Floor { get; }
        IUnaryOperation<T, T> Ceiling { get; }
        IUnaryOperation<T, T> Sqrt { get; }
        IUnaryOperation<T, T> Exp { get; }
        IUnaryOperation<T, T> Ln { get; }
        IUnaryOperation<T, T> Log10 { get; }
        IUnaryOperation<T, T> Sin { get; }
        IUnaryOperation<T, T> Cos { get; }
        IUnaryOperation<T, T> Tan { get; }
        IUnaryOperation<T, T> Asin { get; }
        IUnaryOperation<T, T> Acos { get; }
        IUnaryOperation<T, T> Atan { get; }
        IReductionOperation<T, T> Min { get; }
        IReductionOperation<T, T> Max { get; }
        IReductionOperation<T, T> Sum { get; }
        IAccumulateOperation<T, T> Cumsum { get; }
        IAccumulateOperation<T, T> Cumprod { get; }
    }

}
