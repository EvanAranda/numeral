namespace Numeral.Internals
{

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
