using System;

namespace Numeral
{
    public class DType<T>
    {
        public DType(bool isNumeric = true, bool isFloat = false, T zero = default, T one = default)
        {
            Type = typeof(T);
            IsNumeric = isNumeric;
            IsFloat = isFloat;

            Zero = zero ?? (T)Convert.ChangeType(0, typeof(T));
            One = one ?? (T)Convert.ChangeType(1, typeof(T));
        }

        public Type Type { get; }
        public bool IsNumeric { get; }
        public bool IsFloat { get; }
        public string TypeName => Type.Name;

        public T Zero { get; }
        public T One { get; }
        public T Cast(object value) => (T)Convert.ChangeType(value, Type);
    }

    public static class DTypes
    {
        public static readonly DType<int> Int = new();
        public static readonly DType<float> Float = new(isFloat: true, one: 1, zero: 0);
        public static readonly DType<double> Double = new(isFloat: true, one: 1, zero: 0);
        public static readonly DType<byte> Byte = new();
        public static readonly DType<char> Char = new();
        public static readonly DType<bool> Bool = new(isNumeric: false, zero: false, one: true);

        public static DType<T> GetDType<T>()
        {
            if (typeof(T) == typeof(int))
                return (DType<T>)(object)Int;
            else if (typeof(T) == typeof(float))
                return (DType<T>)(object)Float;
            else if (typeof(T) == typeof(double))
                return (DType<T>)(object)Double;
            else if (typeof(T) == typeof(byte))
                return (DType<T>)(object)Byte;
            else if (typeof(T) == typeof(char))
                return (DType<T>)(object)Char;
            else if (typeof(T) == typeof(bool))
                return (DType<T>)(object)Bool;

            throw new NotSupportedException();
        }
    }

}
