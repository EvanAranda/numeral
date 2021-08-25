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
}
