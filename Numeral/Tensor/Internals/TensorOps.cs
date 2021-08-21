
using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Numeral.Internals
{
    public class Add_float : BinaryOperation<float, float, float>
    {
        internal Add_float() : base(VecOp) { }
        public static readonly Add_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(x[i] + y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Add(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Subtract_float : BinaryOperation<float, float, float>
    {
        internal Subtract_float() : base(VecOp) { }
        public static readonly Subtract_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(x[i] - y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Subtract(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Multiply_float : BinaryOperation<float, float, float>
    {
        internal Multiply_float() : base(VecOp) { }
        public static readonly Multiply_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(x[i] * y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Multiply(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Divide_float : BinaryOperation<float, float, float>
    {
        internal Divide_float() : base(VecOp) { }
        public static readonly Divide_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(x[i] / y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Divide(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Pow_float : BinaryOperation<float, float, float>
    {
        internal Pow_float() : base(Op) { }
        public static readonly Pow_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Pow(x[i], y[i]));
        }
    }
    public class Log_float : BinaryOperation<float, float, float>
    {
        internal Log_float() : base(Op) { }
        public static readonly Log_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Log(x[i], y[i]));
        }
    }
    public class Maximum_float : BinaryOperation<float, float, float>
    {
        internal Maximum_float() : base(VecOp) { }
        public static readonly Maximum_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Max(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Max(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Minimum_float : BinaryOperation<float, float, float>
    {
        internal Minimum_float() : base(VecOp) { }
        public static readonly Minimum_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Min(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Min(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Atan2_float : BinaryOperation<float, float, float>
    {
        internal Atan2_float() : base(Op) { }
        public static readonly Atan2_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Atan2(x[i], y[i]));
        }
    }
    public class LessThan_float : BinaryOperation<float, float, bool>
    {
        internal LessThan_float() : base(VecOp) { }
        public static readonly LessThan_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] < y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThan(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class LessThanOrEqual_float : BinaryOperation<float, float, bool>
    {
        internal LessThanOrEqual_float() : base(VecOp) { }
        public static readonly LessThanOrEqual_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] <= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThanOrEqual(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThan_float : BinaryOperation<float, float, bool>
    {
        internal GreaterThan_float() : base(VecOp) { }
        public static readonly GreaterThan_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] > y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThan(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThanOrEqual_float : BinaryOperation<float, float, bool>
    {
        internal GreaterThanOrEqual_float() : base(VecOp) { }
        public static readonly GreaterThanOrEqual_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] >= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThanOrEqual(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayEqual_float : BinaryOperation<float, float, bool>
    {
        internal ArrayEqual_float() : base(VecOp) { }
        public static readonly ArrayEqual_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] == y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayNotEqual_float : BinaryOperation<float, float, bool>
    {
        internal ArrayNotEqual_float() : base(VecOp) { }
        public static readonly ArrayNotEqual_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] != y[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == 1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }

    public class Negate_float : UnaryOperation<float, float>
    {
        internal Negate_float() : base(VecOp) { }
        public static readonly Negate_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(-x[i]);
        }
        public static void VecOp(in ReadOnlySpan<float> x, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Negate(
                            Unsafe.Read<Vector<float>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Abs_float : UnaryOperation<float, float>
    {
        internal Abs_float() : base(VecOp) { }
        public static readonly Abs_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Abs(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<float> x, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Abs(
                            Unsafe.Read<Vector<float>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Floor_float : UnaryOperation<float, float>
    {
        internal Floor_float() : base(VecOp) { }
        public static readonly Floor_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Floor(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<float> x, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Floor(
                            Unsafe.Read<Vector<float>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Ceiling_float : UnaryOperation<float, float>
    {
        internal Ceiling_float() : base(VecOp) { }
        public static readonly Ceiling_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Ceiling(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<float> x, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x)
                    fixed (float* pz = z)
                    {
                        var t = Vector.Ceiling(
                            Unsafe.Read<Vector<float>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Sqrt_float : UnaryOperation<float, float>
    {
        internal Sqrt_float() : base(VecOp) { }
        public static readonly Sqrt_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Sqrt(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<float> x, in Span<float> z)
        {
            int i = 0;
            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (float* px = x)
                    fixed (float* pz = z)
                    {
                        var t = Vector.SquareRoot(
                            Unsafe.Read<Vector<float>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Exp_float : UnaryOperation<float, float>
    {
        internal Exp_float() : base(Op) { }
        public static readonly Exp_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Exp(x[i]));
        }
    }
    public class Ln_float : UnaryOperation<float, float>
    {
        internal Ln_float() : base(Op) { }
        public static readonly Ln_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Log(x[i]));
        }
    }
    public class Log10_float : UnaryOperation<float, float>
    {
        internal Log10_float() : base(Op) { }
        public static readonly Log10_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Log10(x[i]));
        }
    }
    public class Sin_float : UnaryOperation<float, float>
    {
        internal Sin_float() : base(Op) { }
        public static readonly Sin_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Sin(x[i]));
        }
    }
    public class Cos_float : UnaryOperation<float, float>
    {
        internal Cos_float() : base(Op) { }
        public static readonly Cos_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Cos(x[i]));
        }
    }
    public class Tan_float : UnaryOperation<float, float>
    {
        internal Tan_float() : base(Op) { }
        public static readonly Tan_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Tan(x[i]));
        }
    }
    public class Asin_float : UnaryOperation<float, float>
    {
        internal Asin_float() : base(Op) { }
        public static readonly Asin_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Asin(x[i]));
        }
    }
    public class Acos_float : UnaryOperation<float, float>
    {
        internal Acos_float() : base(Op) { }
        public static readonly Acos_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Acos(x[i]));
        }
    }
    public class Atan_float : UnaryOperation<float, float>
    {
        internal Atan_float() : base(Op) { }
        public static readonly Atan_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (float)(Math.Atan(x[i]));
        }
    }

    public class Min_float : ReductionOperation<float, float>
    {
        internal Min_float() : base(Op) { }
        public static readonly Min_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (float)(Math.Min(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Max_float : ReductionOperation<float, float>
    {
        internal Max_float() : base(Op) { }
        public static readonly Max_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (float)(Math.Max(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Sum_float : ReductionOperation<float, float>
    {
        internal Sum_float() : base(Op) { }
        public static readonly Sum_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (float)(x[i] + tmp);
            z[0] = tmp;
        }
    }
    public class Cumsum_float : AccumulateOperation<float, float>
    {
        internal Cumsum_float() : base(Op) { }
        public static readonly Cumsum_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (float)(x[i] + tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Cumprod_float : AccumulateOperation<float, float>
    {
        internal Cumprod_float() : base(Op) { }
        public static readonly Cumprod_float Instance = new();
        public static void Op(in ReadOnlySpan<float> x, in Span<float> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (float)(x[i] * tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Add_double : BinaryOperation<double, double, double>
    {
        internal Add_double() : base(VecOp) { }
        public static readonly Add_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(x[i] + y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Add(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Subtract_double : BinaryOperation<double, double, double>
    {
        internal Subtract_double() : base(VecOp) { }
        public static readonly Subtract_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(x[i] - y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Subtract(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Multiply_double : BinaryOperation<double, double, double>
    {
        internal Multiply_double() : base(VecOp) { }
        public static readonly Multiply_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(x[i] * y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Multiply(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Divide_double : BinaryOperation<double, double, double>
    {
        internal Divide_double() : base(VecOp) { }
        public static readonly Divide_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(x[i] / y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Divide(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Pow_double : BinaryOperation<double, double, double>
    {
        internal Pow_double() : base(Op) { }
        public static readonly Pow_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Pow(x[i], y[i]));
        }
    }
    public class Log_double : BinaryOperation<double, double, double>
    {
        internal Log_double() : base(Op) { }
        public static readonly Log_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Log(x[i], y[i]));
        }
    }
    public class Maximum_double : BinaryOperation<double, double, double>
    {
        internal Maximum_double() : base(VecOp) { }
        public static readonly Maximum_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Max(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Max(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Minimum_double : BinaryOperation<double, double, double>
    {
        internal Minimum_double() : base(VecOp) { }
        public static readonly Minimum_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Min(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Min(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Atan2_double : BinaryOperation<double, double, double>
    {
        internal Atan2_double() : base(Op) { }
        public static readonly Atan2_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Atan2(x[i], y[i]));
        }
    }
    public class LessThan_double : BinaryOperation<double, double, bool>
    {
        internal LessThan_double() : base(VecOp) { }
        public static readonly LessThan_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] < y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThan(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class LessThanOrEqual_double : BinaryOperation<double, double, bool>
    {
        internal LessThanOrEqual_double() : base(VecOp) { }
        public static readonly LessThanOrEqual_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] <= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThanOrEqual(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThan_double : BinaryOperation<double, double, bool>
    {
        internal GreaterThan_double() : base(VecOp) { }
        public static readonly GreaterThan_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] > y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThan(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThanOrEqual_double : BinaryOperation<double, double, bool>
    {
        internal GreaterThanOrEqual_double() : base(VecOp) { }
        public static readonly GreaterThanOrEqual_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] >= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThanOrEqual(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayEqual_double : BinaryOperation<double, double, bool>
    {
        internal ArrayEqual_double() : base(VecOp) { }
        public static readonly ArrayEqual_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] == y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayNotEqual_double : BinaryOperation<double, double, bool>
    {
        internal ArrayNotEqual_double() : base(VecOp) { }
        public static readonly ArrayNotEqual_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] != y[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in ReadOnlySpan<double> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<double>>(px + i),
                            Unsafe.Read<Vector<double>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == 1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }

    public class Negate_double : UnaryOperation<double, double>
    {
        internal Negate_double() : base(VecOp) { }
        public static readonly Negate_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(-x[i]);
        }
        public static void VecOp(in ReadOnlySpan<double> x, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Negate(
                            Unsafe.Read<Vector<double>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Abs_double : UnaryOperation<double, double>
    {
        internal Abs_double() : base(VecOp) { }
        public static readonly Abs_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Abs(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<double> x, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Abs(
                            Unsafe.Read<Vector<double>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Floor_double : UnaryOperation<double, double>
    {
        internal Floor_double() : base(VecOp) { }
        public static readonly Floor_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Floor(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<double> x, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Floor(
                            Unsafe.Read<Vector<double>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Ceiling_double : UnaryOperation<double, double>
    {
        internal Ceiling_double() : base(VecOp) { }
        public static readonly Ceiling_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Ceiling(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<double> x, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x)
                    fixed (double* pz = z)
                    {
                        var t = Vector.Ceiling(
                            Unsafe.Read<Vector<double>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Sqrt_double : UnaryOperation<double, double>
    {
        internal Sqrt_double() : base(VecOp) { }
        public static readonly Sqrt_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Sqrt(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<double> x, in Span<double> z)
        {
            int i = 0;
            var offset = Vector<double>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (double* px = x)
                    fixed (double* pz = z)
                    {
                        var t = Vector.SquareRoot(
                            Unsafe.Read<Vector<double>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Exp_double : UnaryOperation<double, double>
    {
        internal Exp_double() : base(Op) { }
        public static readonly Exp_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Exp(x[i]));
        }
    }
    public class Ln_double : UnaryOperation<double, double>
    {
        internal Ln_double() : base(Op) { }
        public static readonly Ln_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Log(x[i]));
        }
    }
    public class Log10_double : UnaryOperation<double, double>
    {
        internal Log10_double() : base(Op) { }
        public static readonly Log10_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Log10(x[i]));
        }
    }
    public class Sin_double : UnaryOperation<double, double>
    {
        internal Sin_double() : base(Op) { }
        public static readonly Sin_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Sin(x[i]));
        }
    }
    public class Cos_double : UnaryOperation<double, double>
    {
        internal Cos_double() : base(Op) { }
        public static readonly Cos_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Cos(x[i]));
        }
    }
    public class Tan_double : UnaryOperation<double, double>
    {
        internal Tan_double() : base(Op) { }
        public static readonly Tan_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Tan(x[i]));
        }
    }
    public class Asin_double : UnaryOperation<double, double>
    {
        internal Asin_double() : base(Op) { }
        public static readonly Asin_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Asin(x[i]));
        }
    }
    public class Acos_double : UnaryOperation<double, double>
    {
        internal Acos_double() : base(Op) { }
        public static readonly Acos_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Acos(x[i]));
        }
    }
    public class Atan_double : UnaryOperation<double, double>
    {
        internal Atan_double() : base(Op) { }
        public static readonly Atan_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (double)(Math.Atan(x[i]));
        }
    }

    public class Min_double : ReductionOperation<double, double>
    {
        internal Min_double() : base(Op) { }
        public static readonly Min_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (double)(Math.Min(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Max_double : ReductionOperation<double, double>
    {
        internal Max_double() : base(Op) { }
        public static readonly Max_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (double)(Math.Max(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Sum_double : ReductionOperation<double, double>
    {
        internal Sum_double() : base(Op) { }
        public static readonly Sum_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (double)(x[i] + tmp);
            z[0] = tmp;
        }
    }
    public class Cumsum_double : AccumulateOperation<double, double>
    {
        internal Cumsum_double() : base(Op) { }
        public static readonly Cumsum_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (double)(x[i] + tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Cumprod_double : AccumulateOperation<double, double>
    {
        internal Cumprod_double() : base(Op) { }
        public static readonly Cumprod_double Instance = new();
        public static void Op(in ReadOnlySpan<double> x, in Span<double> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (double)(x[i] * tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Add_int : BinaryOperation<int, int, int>
    {
        internal Add_int() : base(VecOp) { }
        public static readonly Add_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] + y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Add(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Subtract_int : BinaryOperation<int, int, int>
    {
        internal Subtract_int() : base(VecOp) { }
        public static readonly Subtract_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] - y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Subtract(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Multiply_int : BinaryOperation<int, int, int>
    {
        internal Multiply_int() : base(VecOp) { }
        public static readonly Multiply_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] * y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Multiply(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Divide_int : BinaryOperation<int, int, int>
    {
        internal Divide_int() : base(VecOp) { }
        public static readonly Divide_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] / y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Divide(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class BitAnd_int : BinaryOperation<int, int, int>
    {
        internal BitAnd_int() : base(VecOp) { }
        public static readonly BitAnd_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] & y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.BitwiseAnd(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class BitOr_int : BinaryOperation<int, int, int>
    {
        internal BitOr_int() : base(VecOp) { }
        public static readonly BitOr_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] | y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.BitwiseOr(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Xor_int : BinaryOperation<int, int, int>
    {
        internal Xor_int() : base(VecOp) { }
        public static readonly Xor_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(x[i] ^ y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Xor(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Pow_int : BinaryOperation<int, int, int>
    {
        internal Pow_int() : base(Op) { }
        public static readonly Pow_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(Math.Pow(x[i], y[i]));
        }
    }
    public class Log_int : BinaryOperation<int, int, int>
    {
        internal Log_int() : base(Op) { }
        public static readonly Log_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(Math.Log(x[i], y[i]));
        }
    }
    public class Maximum_int : BinaryOperation<int, int, int>
    {
        internal Maximum_int() : base(VecOp) { }
        public static readonly Maximum_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(Math.Max(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Max(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Minimum_int : BinaryOperation<int, int, int>
    {
        internal Minimum_int() : base(VecOp) { }
        public static readonly Minimum_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(Math.Min(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Min(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Atan2_int : BinaryOperation<int, int, int>
    {
        internal Atan2_int() : base(Op) { }
        public static readonly Atan2_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(Math.Atan2(x[i], y[i]));
        }
    }
    public class LessThan_int : BinaryOperation<int, int, bool>
    {
        internal LessThan_int() : base(VecOp) { }
        public static readonly LessThan_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] < y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThan(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class LessThanOrEqual_int : BinaryOperation<int, int, bool>
    {
        internal LessThanOrEqual_int() : base(VecOp) { }
        public static readonly LessThanOrEqual_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] <= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThanOrEqual(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThan_int : BinaryOperation<int, int, bool>
    {
        internal GreaterThan_int() : base(VecOp) { }
        public static readonly GreaterThan_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] > y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThan(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThanOrEqual_int : BinaryOperation<int, int, bool>
    {
        internal GreaterThanOrEqual_int() : base(VecOp) { }
        public static readonly GreaterThanOrEqual_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] >= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThanOrEqual(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayEqual_int : BinaryOperation<int, int, bool>
    {
        internal ArrayEqual_int() : base(VecOp) { }
        public static readonly ArrayEqual_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] == y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayNotEqual_int : BinaryOperation<int, int, bool>
    {
        internal ArrayNotEqual_int() : base(VecOp) { }
        public static readonly ArrayNotEqual_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] != y[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in ReadOnlySpan<int> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<int>>(px + i),
                            Unsafe.Read<Vector<int>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == 1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }

    public class Negate_int : UnaryOperation<int, int>
    {
        internal Negate_int() : base(VecOp) { }
        public static readonly Negate_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(-x[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Negate(
                            Unsafe.Read<Vector<int>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Abs_int : UnaryOperation<int, int>
    {
        internal Abs_int() : base(VecOp) { }
        public static readonly Abs_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(Math.Abs(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<int> x, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x)
                    fixed (int* pz = z)
                    {
                        var t = Vector.Abs(
                            Unsafe.Read<Vector<int>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class BitNot_int : UnaryOperation<int, int>
    {
        internal BitNot_int() : base(VecOp) { }
        public static readonly BitNot_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (int)(~x[i]);
        }
        public static void VecOp(in ReadOnlySpan<int> x, in Span<int> z)
        {
            int i = 0;
            var offset = Vector<int>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (int* px = x)
                    fixed (int* pz = z)
                    {
                        var t = Vector.OnesComplement(
                            Unsafe.Read<Vector<int>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }

    public class Min_int : ReductionOperation<int, int>
    {
        internal Min_int() : base(Op) { }
        public static readonly Min_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (int)(Math.Min(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Max_int : ReductionOperation<int, int>
    {
        internal Max_int() : base(Op) { }
        public static readonly Max_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (int)(Math.Max(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Sum_int : ReductionOperation<int, int>
    {
        internal Sum_int() : base(Op) { }
        public static readonly Sum_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (int)(x[i] + tmp);
            z[0] = tmp;
        }
    }
    public class Cumsum_int : AccumulateOperation<int, int>
    {
        internal Cumsum_int() : base(Op) { }
        public static readonly Cumsum_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (int)(x[i] + tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Cumprod_int : AccumulateOperation<int, int>
    {
        internal Cumprod_int() : base(Op) { }
        public static readonly Cumprod_int Instance = new();
        public static void Op(in ReadOnlySpan<int> x, in Span<int> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (int)(x[i] * tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Add_byte : BinaryOperation<byte, byte, byte>
    {
        internal Add_byte() : base(VecOp) { }
        public static readonly Add_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] + y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Add(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Subtract_byte : BinaryOperation<byte, byte, byte>
    {
        internal Subtract_byte() : base(VecOp) { }
        public static readonly Subtract_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] - y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Subtract(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Multiply_byte : BinaryOperation<byte, byte, byte>
    {
        internal Multiply_byte() : base(VecOp) { }
        public static readonly Multiply_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] * y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Multiply(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Divide_byte : BinaryOperation<byte, byte, byte>
    {
        internal Divide_byte() : base(VecOp) { }
        public static readonly Divide_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] / y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Divide(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class BitAnd_byte : BinaryOperation<byte, byte, byte>
    {
        internal BitAnd_byte() : base(VecOp) { }
        public static readonly BitAnd_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] & y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.BitwiseAnd(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class BitOr_byte : BinaryOperation<byte, byte, byte>
    {
        internal BitOr_byte() : base(VecOp) { }
        public static readonly BitOr_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] | y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.BitwiseOr(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Xor_byte : BinaryOperation<byte, byte, byte>
    {
        internal Xor_byte() : base(VecOp) { }
        public static readonly Xor_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(x[i] ^ y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Xor(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Pow_byte : BinaryOperation<byte, byte, byte>
    {
        internal Pow_byte() : base(Op) { }
        public static readonly Pow_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(Math.Pow(x[i], y[i]));
        }
    }
    public class Log_byte : BinaryOperation<byte, byte, byte>
    {
        internal Log_byte() : base(Op) { }
        public static readonly Log_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(Math.Log(x[i], y[i]));
        }
    }
    public class Maximum_byte : BinaryOperation<byte, byte, byte>
    {
        internal Maximum_byte() : base(VecOp) { }
        public static readonly Maximum_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(Math.Max(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Max(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Minimum_byte : BinaryOperation<byte, byte, byte>
    {
        internal Minimum_byte() : base(VecOp) { }
        public static readonly Minimum_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(Math.Min(x[i], y[i]));
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Min(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class Atan2_byte : BinaryOperation<byte, byte, byte>
    {
        internal Atan2_byte() : base(Op) { }
        public static readonly Atan2_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(Math.Atan2(x[i], y[i]));
        }
    }
    public class LessThan_byte : BinaryOperation<byte, byte, bool>
    {
        internal LessThan_byte() : base(VecOp) { }
        public static readonly LessThan_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] < y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThan(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class LessThanOrEqual_byte : BinaryOperation<byte, byte, bool>
    {
        internal LessThanOrEqual_byte() : base(VecOp) { }
        public static readonly LessThanOrEqual_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] <= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.LessThanOrEqual(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThan_byte : BinaryOperation<byte, byte, bool>
    {
        internal GreaterThan_byte() : base(VecOp) { }
        public static readonly GreaterThan_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] > y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThan(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class GreaterThanOrEqual_byte : BinaryOperation<byte, byte, bool>
    {
        internal GreaterThanOrEqual_byte() : base(VecOp) { }
        public static readonly GreaterThanOrEqual_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] >= y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.GreaterThanOrEqual(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayEqual_byte : BinaryOperation<byte, byte, bool>
    {
        internal ArrayEqual_byte() : base(VecOp) { }
        public static readonly ArrayEqual_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] == y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == -1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }
    public class ArrayNotEqual_byte : BinaryOperation<byte, byte, bool>
    {
        internal ArrayNotEqual_byte() : base(VecOp) { }
        public static readonly ArrayNotEqual_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (bool)(x[i] != y[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in ReadOnlySpan<byte> y, in Span<bool> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x, py = y)
                    fixed (bool* pz = z)
                    {
                        var t = Vector.Equals(
                            Unsafe.Read<Vector<byte>>(px + i),
                            Unsafe.Read<Vector<byte>>(py + i));
                        for (int j = i; j < offset; j++)
                            z[j] = t[j] == 1;
                    }
                }
            }   
            if (i < x.Length)
                Op(x[i..], y[i..], z[i..]);
        }
    }

    public class Negate_byte : UnaryOperation<byte, byte>
    {
        internal Negate_byte() : base(VecOp) { }
        public static readonly Negate_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(-x[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Negate(
                            Unsafe.Read<Vector<byte>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class Abs_byte : UnaryOperation<byte, byte>
    {
        internal Abs_byte() : base(VecOp) { }
        public static readonly Abs_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(Math.Abs(x[i]));
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.Abs(
                            Unsafe.Read<Vector<byte>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }
    public class BitNot_byte : UnaryOperation<byte, byte>
    {
        internal BitNot_byte() : base(VecOp) { }
        public static readonly BitNot_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            for (int i = 0; i < x.Length; i++)
                z[i] = (byte)(~x[i]);
        }
        public static void VecOp(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            int i = 0;
            var offset = Vector<byte>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                unsafe
                {
                    fixed (byte* px = x)
                    fixed (byte* pz = z)
                    {
                        var t = Vector.OnesComplement(
                            Unsafe.Read<Vector<byte>>(px + i));
                        Unsafe.Write(pz + i, t);
                    }
                }
            }
            if (i < x.Length)
                Op(x[i..], z[i..]);
        }
    }

    public class Min_byte : ReductionOperation<byte, byte>
    {
        internal Min_byte() : base(Op) { }
        public static readonly Min_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (byte)(Math.Min(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Max_byte : ReductionOperation<byte, byte>
    {
        internal Max_byte() : base(Op) { }
        public static readonly Max_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (byte)(Math.Max(x[i], tmp));
            z[0] = tmp;
        }
    }
    public class Sum_byte : ReductionOperation<byte, byte>
    {
        internal Sum_byte() : base(Op) { }
        public static readonly Sum_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            var tmp = x[0];
            for (int i = 1; i < x.Length; i++)
                tmp = (byte)(x[i] + tmp);
            z[0] = tmp;
        }
    }
    public class Cumsum_byte : AccumulateOperation<byte, byte>
    {
        internal Cumsum_byte() : base(Op) { }
        public static readonly Cumsum_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (byte)(x[i] + tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
    public class Cumprod_byte : AccumulateOperation<byte, byte>
    {
        internal Cumprod_byte() : base(Op) { }
        public static readonly Cumprod_byte Instance = new();
        public static void Op(in ReadOnlySpan<byte> x, in Span<byte> z)
        {
            var tmp = x[0];
            z[0] = tmp;
            for (int i = 1; i < x.Length; i++)
            {
                tmp = (byte)(x[i] * tmp);
                z[i] = tmp;
            }
            z[0] = tmp;
        }
    }
}