namespace Numeral.Internals
{
    public interface ITensorCore<T>
    {
        ITensorArithmetic<T> Basic { get; }
    }

}
