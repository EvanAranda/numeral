
using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;

using Numeral.Internals;

namespace Numeral
{
    public abstract partial class Tensor<T>
    {
        public static Tensor<T> operator +(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.Add, x, y);
        public static Tensor<T> operator +(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.Add, x, (Scalar<T>)y);
        public static Tensor<T> operator +(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.Add, (Scalar<T>)x, y);
        public static Tensor<T> operator -(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.Subtract, x, y);
        public static Tensor<T> operator -(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.Subtract, x, (Scalar<T>)y);
        public static Tensor<T> operator -(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.Subtract, (Scalar<T>)x, y);
        public static Tensor<T> operator *(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.Multiply, x, y);
        public static Tensor<T> operator *(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.Multiply, x, (Scalar<T>)y);
        public static Tensor<T> operator *(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.Multiply, (Scalar<T>)x, y);
        public static Tensor<T> operator /(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.Divide, x, y);
        public static Tensor<T> operator /(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.Divide, x, (Scalar<T>)y);
        public static Tensor<T> operator /(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.Divide, (Scalar<T>)x, y);
        public static Tensor<T> operator &(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.BitAnd, x, y);
        public static Tensor<T> operator &(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.BitAnd, x, (Scalar<T>)y);
        public static Tensor<T> operator &(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.BitAnd, (Scalar<T>)x, y);
        public static Tensor<T> operator |(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.BitOr, x, y);
        public static Tensor<T> operator |(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.BitOr, x, (Scalar<T>)y);
        public static Tensor<T> operator |(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.BitOr, (Scalar<T>)x, y);
        public static Tensor<T> operator ^(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.Xor, x, y);
        public static Tensor<T> operator ^(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.Xor, x, (Scalar<T>)y);
        public static Tensor<T> operator ^(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.Xor, (Scalar<T>)x, y);
        public static Tensor<bool> operator <(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.LessThan, x, y);
        public static Tensor<bool> operator <(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.LessThan, x, (Scalar<T>)y);
        public static Tensor<bool> operator <(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.LessThan, (Scalar<T>)x, y);
        public static Tensor<bool> operator <=(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.LessThanOrEqual, x, y);
        public static Tensor<bool> operator <=(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.LessThanOrEqual, x, (Scalar<T>)y);
        public static Tensor<bool> operator <=(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.LessThanOrEqual, (Scalar<T>)x, y);
        public static Tensor<bool> operator >(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.GreaterThan, x, y);
        public static Tensor<bool> operator >(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.GreaterThan, x, (Scalar<T>)y);
        public static Tensor<bool> operator >(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.GreaterThan, (Scalar<T>)x, y);
        public static Tensor<bool> operator >=(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.GreaterThanOrEqual, x, y);
        public static Tensor<bool> operator >=(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.GreaterThanOrEqual, x, (Scalar<T>)y);
        public static Tensor<bool> operator >=(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.GreaterThanOrEqual, (Scalar<T>)x, y);
        public static Tensor<bool> operator ==(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.ArrayEqual, x, y);
        public static Tensor<bool> operator ==(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.ArrayEqual, x, (Scalar<T>)y);
        public static Tensor<bool> operator ==(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.ArrayEqual, (Scalar<T>)x, y);
        public static Tensor<bool> operator !=(Tensor<T> x, Tensor<T> y) => Dispatcher.Call(x.Core.Basic.ArrayNotEqual, x, y);
        public static Tensor<bool> operator !=(Tensor<T> x, T y) => Dispatcher.Call(x.Core.Basic.ArrayNotEqual, x, (Scalar<T>)y);
        public static Tensor<bool> operator !=(T x, Tensor<T> y) => Dispatcher.Call(y.Core.Basic.ArrayNotEqual, (Scalar<T>)x, y);
        public static Tensor<T> operator -(Tensor<T> x) => Dispatcher.Call(x.Core.Basic.Negate, x);
        public static Tensor<T> operator ~(Tensor<T> x) => Dispatcher.Call(x.Core.Basic.BitNot, x);
    }
}