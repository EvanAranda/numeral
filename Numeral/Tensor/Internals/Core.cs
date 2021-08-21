using System;

namespace Numeral.Internals
{
    public class CpuArithmetic<T>
    {
        public static readonly ITensorArithmetic<T> Instance = GetInstance();

        public static ITensorArithmetic<T> GetInstance()
        {
            if (typeof(T) == typeof(float))
                return (ITensorArithmetic<T>)new CpuArithmetic_float();
            if (typeof(T) == typeof(double))
                return (ITensorArithmetic<T>)new CpuArithmetic_double();
            if (typeof(T) == typeof(int))
                return (ITensorArithmetic<T>)new CpuArithmetic_int();
            if (typeof(T) == typeof(byte))
                return (ITensorArithmetic<T>)new CpuArithmetic_byte();

            throw new NotSupportedException();
        }
    }

    public class Core<T> : ITensorCore<T>
    {
        public Core(ITensorArithmetic<T> tensorArithmetic = null)
        {
            Basic = tensorArithmetic ?? CpuArithmetic<T>.Instance;
        }

        public ITensorArithmetic<T> Basic { get; }

        public static readonly Core<T> Instance = new Core<T>();
    }
}
