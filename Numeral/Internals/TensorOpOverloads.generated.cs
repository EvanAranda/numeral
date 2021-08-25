
using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;

using Numeral.Internals;

namespace Numeral
{
    public abstract partial class Tensor<T>
    {
        public static Tensor<T> operator +(Tensor<T> x, Tensor<T> y) => x.Core.Basic.Add.Call(x, y);
        public static Tensor<T> operator +(Tensor<T> x, T y) => x.Core.Basic.Add.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator +(T x, Tensor<T> y) => y.Core.Basic.Add.Call((Scalar<T>)x, y);
        public static Tensor<T> operator -(Tensor<T> x, Tensor<T> y) => x.Core.Basic.Subtract.Call(x, y);
        public static Tensor<T> operator -(Tensor<T> x, T y) => x.Core.Basic.Subtract.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator -(T x, Tensor<T> y) => y.Core.Basic.Subtract.Call((Scalar<T>)x, y);
        public static Tensor<T> operator *(Tensor<T> x, Tensor<T> y) => x.Core.Basic.Multiply.Call(x, y);
        public static Tensor<T> operator *(Tensor<T> x, T y) => x.Core.Basic.Multiply.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator *(T x, Tensor<T> y) => y.Core.Basic.Multiply.Call((Scalar<T>)x, y);
        public static Tensor<T> operator /(Tensor<T> x, Tensor<T> y) => x.Core.Basic.Divide.Call(x, y);
        public static Tensor<T> operator /(Tensor<T> x, T y) => x.Core.Basic.Divide.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator /(T x, Tensor<T> y) => y.Core.Basic.Divide.Call((Scalar<T>)x, y);
        public static Tensor<T> operator &(Tensor<T> x, Tensor<T> y) => x.Core.Basic.BitAnd.Call(x, y);
        public static Tensor<T> operator &(Tensor<T> x, T y) => x.Core.Basic.BitAnd.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator &(T x, Tensor<T> y) => y.Core.Basic.BitAnd.Call((Scalar<T>)x, y);
        public static Tensor<T> operator |(Tensor<T> x, Tensor<T> y) => x.Core.Basic.BitOr.Call(x, y);
        public static Tensor<T> operator |(Tensor<T> x, T y) => x.Core.Basic.BitOr.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator |(T x, Tensor<T> y) => y.Core.Basic.BitOr.Call((Scalar<T>)x, y);
        public static Tensor<T> operator ^(Tensor<T> x, Tensor<T> y) => x.Core.Basic.Xor.Call(x, y);
        public static Tensor<T> operator ^(Tensor<T> x, T y) => x.Core.Basic.Xor.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator ^(T x, Tensor<T> y) => y.Core.Basic.Xor.Call((Scalar<T>)x, y);
        public static Tensor<T> operator %(Tensor<T> x, Tensor<T> y) => x.Core.Basic.Mod.Call(x, y);
        public static Tensor<T> operator %(Tensor<T> x, T y) => x.Core.Basic.Mod.Call(x, (Scalar<T>)y);
        public static Tensor<T> operator %(T x, Tensor<T> y) => y.Core.Basic.Mod.Call((Scalar<T>)x, y);
        public static Tensor<bool> operator <(Tensor<T> x, Tensor<T> y) => x.Core.Basic.LessThan.Call(x, y);
        public static Tensor<bool> operator <(Tensor<T> x, T y) => x.Core.Basic.LessThan.Call(x, (Scalar<T>)y);
        public static Tensor<bool> operator <(T x, Tensor<T> y) => y.Core.Basic.LessThan.Call((Scalar<T>)x, y);
        public static Tensor<bool> operator <=(Tensor<T> x, Tensor<T> y) => x.Core.Basic.LessThanOrEqual.Call(x, y);
        public static Tensor<bool> operator <=(Tensor<T> x, T y) => x.Core.Basic.LessThanOrEqual.Call(x, (Scalar<T>)y);
        public static Tensor<bool> operator <=(T x, Tensor<T> y) => y.Core.Basic.LessThanOrEqual.Call((Scalar<T>)x, y);
        public static Tensor<bool> operator >(Tensor<T> x, Tensor<T> y) => x.Core.Basic.GreaterThan.Call(x, y);
        public static Tensor<bool> operator >(Tensor<T> x, T y) => x.Core.Basic.GreaterThan.Call(x, (Scalar<T>)y);
        public static Tensor<bool> operator >(T x, Tensor<T> y) => y.Core.Basic.GreaterThan.Call((Scalar<T>)x, y);
        public static Tensor<bool> operator >=(Tensor<T> x, Tensor<T> y) => x.Core.Basic.GreaterThanOrEqual.Call(x, y);
        public static Tensor<bool> operator >=(Tensor<T> x, T y) => x.Core.Basic.GreaterThanOrEqual.Call(x, (Scalar<T>)y);
        public static Tensor<bool> operator >=(T x, Tensor<T> y) => y.Core.Basic.GreaterThanOrEqual.Call((Scalar<T>)x, y);
        public static Tensor<bool> operator ==(Tensor<T> x, Tensor<T> y) => x.Core.Basic.ArrayEqual.Call(x, y);
        public static Tensor<bool> operator ==(Tensor<T> x, T y) => x.Core.Basic.ArrayEqual.Call(x, (Scalar<T>)y);
        public static Tensor<bool> operator ==(T x, Tensor<T> y) => y.Core.Basic.ArrayEqual.Call((Scalar<T>)x, y);
        public static Tensor<bool> operator !=(Tensor<T> x, Tensor<T> y) => x.Core.Basic.ArrayNotEqual.Call(x, y);
        public static Tensor<bool> operator !=(Tensor<T> x, T y) => x.Core.Basic.ArrayNotEqual.Call(x, (Scalar<T>)y);
        public static Tensor<bool> operator !=(T x, Tensor<T> y) => y.Core.Basic.ArrayNotEqual.Call((Scalar<T>)x, y);
        public static Tensor<T> operator -(Tensor<T> x) => x.Core.Basic.Negate.Call(x);
        public static Tensor<T> operator ~(Tensor<T> x) => x.Core.Basic.BitNot.Call(x);
    }
}