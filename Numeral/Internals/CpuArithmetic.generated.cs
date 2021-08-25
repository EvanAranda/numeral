
using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Numeral.Internals
{
    public class CpuArithmetic_float : ITensorArithmetic<float>
    {
        public IBinaryOperation<float, float, float> Add { get; } = Add_float.Instance;
        public IBinaryOperation<float, float, float> Subtract { get; } = Subtract_float.Instance;
        public IBinaryOperation<float, float, float> Multiply { get; } = Multiply_float.Instance;
        public IBinaryOperation<float, float, float> Divide { get; } = Divide_float.Instance;
        public IBinaryOperation<float, float, float> BitAnd => throw new NotSupportedException();
        public IBinaryOperation<float, float, float> BitOr => throw new NotSupportedException();
        public IBinaryOperation<float, float, float> Xor => throw new NotSupportedException();
        public IBinaryOperation<float, float, float> Pow { get; } = Pow_float.Instance;
        public IBinaryOperation<float, float, float> Mod => throw new NotSupportedException();
        public IBinaryOperation<float, float, float> Log { get; } = Log_float.Instance;
        public IBinaryOperation<float, float, float> Maximum { get; } = Maximum_float.Instance;
        public IBinaryOperation<float, float, float> Minimum { get; } = Minimum_float.Instance;
        public IBinaryOperation<float, float, float> Atan2 { get; } = Atan2_float.Instance;
        public IBinaryOperation<float, float, bool> LessThan { get; } = LessThan_float.Instance;
        public IBinaryOperation<float, float, bool> LessThanOrEqual { get; } = LessThanOrEqual_float.Instance;
        public IBinaryOperation<float, float, bool> GreaterThan { get; } = GreaterThan_float.Instance;
        public IBinaryOperation<float, float, bool> GreaterThanOrEqual { get; } = GreaterThanOrEqual_float.Instance;
        public IBinaryOperation<float, float, bool> ArrayEqual { get; } = ArrayEqual_float.Instance;
        public IBinaryOperation<float, float, bool> ArrayNotEqual { get; } = ArrayNotEqual_float.Instance;
        public IUnaryOperation<float, float> Negate { get; } = Negate_float.Instance;
        public IUnaryOperation<float, float> Abs { get; } = Abs_float.Instance;
        public IUnaryOperation<float, float> BitNot => throw new NotSupportedException();
        public IUnaryOperation<float, float> Floor { get; } = Floor_float.Instance;
        public IUnaryOperation<float, float> Ceiling { get; } = Ceiling_float.Instance;
        public IUnaryOperation<float, float> Sqrt { get; } = Sqrt_float.Instance;
        public IUnaryOperation<float, float> Exp { get; } = Exp_float.Instance;
        public IUnaryOperation<float, float> Ln { get; } = Ln_float.Instance;
        public IUnaryOperation<float, float> Log10 { get; } = Log10_float.Instance;
        public IUnaryOperation<float, float> Sin { get; } = Sin_float.Instance;
        public IUnaryOperation<float, float> Cos { get; } = Cos_float.Instance;
        public IUnaryOperation<float, float> Tan { get; } = Tan_float.Instance;
        public IUnaryOperation<float, float> Asin { get; } = Asin_float.Instance;
        public IUnaryOperation<float, float> Acos { get; } = Acos_float.Instance;
        public IUnaryOperation<float, float> Atan { get; } = Atan_float.Instance;
        public IReductionOperation<float, float> Min { get; } = Min_float.Instance;
        public IReductionOperation<float, float> Max { get; } = Max_float.Instance;
        public IReductionOperation<float, float> Sum { get; } = Sum_float.Instance;
        public IAccumulateOperation<float, float> Cumsum { get; } = Cumsum_float.Instance;
        public IAccumulateOperation<float, float> Cumprod { get; } = Cumprod_float.Instance;
    }
    public class CpuArithmetic_double : ITensorArithmetic<double>
    {
        public IBinaryOperation<double, double, double> Add { get; } = Add_double.Instance;
        public IBinaryOperation<double, double, double> Subtract { get; } = Subtract_double.Instance;
        public IBinaryOperation<double, double, double> Multiply { get; } = Multiply_double.Instance;
        public IBinaryOperation<double, double, double> Divide { get; } = Divide_double.Instance;
        public IBinaryOperation<double, double, double> BitAnd => throw new NotSupportedException();
        public IBinaryOperation<double, double, double> BitOr => throw new NotSupportedException();
        public IBinaryOperation<double, double, double> Xor => throw new NotSupportedException();
        public IBinaryOperation<double, double, double> Pow { get; } = Pow_double.Instance;
        public IBinaryOperation<double, double, double> Mod => throw new NotSupportedException();
        public IBinaryOperation<double, double, double> Log { get; } = Log_double.Instance;
        public IBinaryOperation<double, double, double> Maximum { get; } = Maximum_double.Instance;
        public IBinaryOperation<double, double, double> Minimum { get; } = Minimum_double.Instance;
        public IBinaryOperation<double, double, double> Atan2 { get; } = Atan2_double.Instance;
        public IBinaryOperation<double, double, bool> LessThan { get; } = LessThan_double.Instance;
        public IBinaryOperation<double, double, bool> LessThanOrEqual { get; } = LessThanOrEqual_double.Instance;
        public IBinaryOperation<double, double, bool> GreaterThan { get; } = GreaterThan_double.Instance;
        public IBinaryOperation<double, double, bool> GreaterThanOrEqual { get; } = GreaterThanOrEqual_double.Instance;
        public IBinaryOperation<double, double, bool> ArrayEqual { get; } = ArrayEqual_double.Instance;
        public IBinaryOperation<double, double, bool> ArrayNotEqual { get; } = ArrayNotEqual_double.Instance;
        public IUnaryOperation<double, double> Negate { get; } = Negate_double.Instance;
        public IUnaryOperation<double, double> Abs { get; } = Abs_double.Instance;
        public IUnaryOperation<double, double> BitNot => throw new NotSupportedException();
        public IUnaryOperation<double, double> Floor { get; } = Floor_double.Instance;
        public IUnaryOperation<double, double> Ceiling { get; } = Ceiling_double.Instance;
        public IUnaryOperation<double, double> Sqrt { get; } = Sqrt_double.Instance;
        public IUnaryOperation<double, double> Exp { get; } = Exp_double.Instance;
        public IUnaryOperation<double, double> Ln { get; } = Ln_double.Instance;
        public IUnaryOperation<double, double> Log10 { get; } = Log10_double.Instance;
        public IUnaryOperation<double, double> Sin { get; } = Sin_double.Instance;
        public IUnaryOperation<double, double> Cos { get; } = Cos_double.Instance;
        public IUnaryOperation<double, double> Tan { get; } = Tan_double.Instance;
        public IUnaryOperation<double, double> Asin { get; } = Asin_double.Instance;
        public IUnaryOperation<double, double> Acos { get; } = Acos_double.Instance;
        public IUnaryOperation<double, double> Atan { get; } = Atan_double.Instance;
        public IReductionOperation<double, double> Min { get; } = Min_double.Instance;
        public IReductionOperation<double, double> Max { get; } = Max_double.Instance;
        public IReductionOperation<double, double> Sum { get; } = Sum_double.Instance;
        public IAccumulateOperation<double, double> Cumsum { get; } = Cumsum_double.Instance;
        public IAccumulateOperation<double, double> Cumprod { get; } = Cumprod_double.Instance;
    }
    public class CpuArithmetic_int : ITensorArithmetic<int>
    {
        public IBinaryOperation<int, int, int> Add { get; } = Add_int.Instance;
        public IBinaryOperation<int, int, int> Subtract { get; } = Subtract_int.Instance;
        public IBinaryOperation<int, int, int> Multiply { get; } = Multiply_int.Instance;
        public IBinaryOperation<int, int, int> Divide { get; } = Divide_int.Instance;
        public IBinaryOperation<int, int, int> BitAnd { get; } = BitAnd_int.Instance;
        public IBinaryOperation<int, int, int> BitOr { get; } = BitOr_int.Instance;
        public IBinaryOperation<int, int, int> Xor { get; } = Xor_int.Instance;
        public IBinaryOperation<int, int, int> Pow { get; } = Pow_int.Instance;
        public IBinaryOperation<int, int, int> Mod { get; } = Mod_int.Instance;
        public IBinaryOperation<int, int, int> Log { get; } = Log_int.Instance;
        public IBinaryOperation<int, int, int> Maximum { get; } = Maximum_int.Instance;
        public IBinaryOperation<int, int, int> Minimum { get; } = Minimum_int.Instance;
        public IBinaryOperation<int, int, int> Atan2 { get; } = Atan2_int.Instance;
        public IBinaryOperation<int, int, bool> LessThan { get; } = LessThan_int.Instance;
        public IBinaryOperation<int, int, bool> LessThanOrEqual { get; } = LessThanOrEqual_int.Instance;
        public IBinaryOperation<int, int, bool> GreaterThan { get; } = GreaterThan_int.Instance;
        public IBinaryOperation<int, int, bool> GreaterThanOrEqual { get; } = GreaterThanOrEqual_int.Instance;
        public IBinaryOperation<int, int, bool> ArrayEqual { get; } = ArrayEqual_int.Instance;
        public IBinaryOperation<int, int, bool> ArrayNotEqual { get; } = ArrayNotEqual_int.Instance;
        public IUnaryOperation<int, int> Negate { get; } = Negate_int.Instance;
        public IUnaryOperation<int, int> Abs { get; } = Abs_int.Instance;
        public IUnaryOperation<int, int> BitNot { get; } = BitNot_int.Instance;
        public IUnaryOperation<int, int> Floor => throw new NotSupportedException();
        public IUnaryOperation<int, int> Ceiling => throw new NotSupportedException();
        public IUnaryOperation<int, int> Sqrt => throw new NotSupportedException();
        public IUnaryOperation<int, int> Exp => throw new NotSupportedException();
        public IUnaryOperation<int, int> Ln => throw new NotSupportedException();
        public IUnaryOperation<int, int> Log10 => throw new NotSupportedException();
        public IUnaryOperation<int, int> Sin => throw new NotSupportedException();
        public IUnaryOperation<int, int> Cos => throw new NotSupportedException();
        public IUnaryOperation<int, int> Tan => throw new NotSupportedException();
        public IUnaryOperation<int, int> Asin => throw new NotSupportedException();
        public IUnaryOperation<int, int> Acos => throw new NotSupportedException();
        public IUnaryOperation<int, int> Atan => throw new NotSupportedException();
        public IReductionOperation<int, int> Min { get; } = Min_int.Instance;
        public IReductionOperation<int, int> Max { get; } = Max_int.Instance;
        public IReductionOperation<int, int> Sum { get; } = Sum_int.Instance;
        public IAccumulateOperation<int, int> Cumsum { get; } = Cumsum_int.Instance;
        public IAccumulateOperation<int, int> Cumprod { get; } = Cumprod_int.Instance;
    }
    public class CpuArithmetic_byte : ITensorArithmetic<byte>
    {
        public IBinaryOperation<byte, byte, byte> Add { get; } = Add_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Subtract { get; } = Subtract_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Multiply { get; } = Multiply_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Divide { get; } = Divide_byte.Instance;
        public IBinaryOperation<byte, byte, byte> BitAnd { get; } = BitAnd_byte.Instance;
        public IBinaryOperation<byte, byte, byte> BitOr { get; } = BitOr_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Xor { get; } = Xor_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Pow { get; } = Pow_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Mod => throw new NotSupportedException();
        public IBinaryOperation<byte, byte, byte> Log { get; } = Log_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Maximum { get; } = Maximum_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Minimum { get; } = Minimum_byte.Instance;
        public IBinaryOperation<byte, byte, byte> Atan2 { get; } = Atan2_byte.Instance;
        public IBinaryOperation<byte, byte, bool> LessThan { get; } = LessThan_byte.Instance;
        public IBinaryOperation<byte, byte, bool> LessThanOrEqual { get; } = LessThanOrEqual_byte.Instance;
        public IBinaryOperation<byte, byte, bool> GreaterThan { get; } = GreaterThan_byte.Instance;
        public IBinaryOperation<byte, byte, bool> GreaterThanOrEqual { get; } = GreaterThanOrEqual_byte.Instance;
        public IBinaryOperation<byte, byte, bool> ArrayEqual { get; } = ArrayEqual_byte.Instance;
        public IBinaryOperation<byte, byte, bool> ArrayNotEqual { get; } = ArrayNotEqual_byte.Instance;
        public IUnaryOperation<byte, byte> Negate { get; } = Negate_byte.Instance;
        public IUnaryOperation<byte, byte> Abs { get; } = Abs_byte.Instance;
        public IUnaryOperation<byte, byte> BitNot { get; } = BitNot_byte.Instance;
        public IUnaryOperation<byte, byte> Floor => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Ceiling => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Sqrt => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Exp => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Ln => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Log10 => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Sin => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Cos => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Tan => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Asin => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Acos => throw new NotSupportedException();
        public IUnaryOperation<byte, byte> Atan => throw new NotSupportedException();
        public IReductionOperation<byte, byte> Min { get; } = Min_byte.Instance;
        public IReductionOperation<byte, byte> Max { get; } = Max_byte.Instance;
        public IReductionOperation<byte, byte> Sum { get; } = Sum_byte.Instance;
        public IAccumulateOperation<byte, byte> Cumsum { get; } = Cumsum_byte.Instance;
        public IAccumulateOperation<byte, byte> Cumprod { get; } = Cumprod_byte.Instance;
    }
}