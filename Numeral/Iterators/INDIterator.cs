using System;
using Numeral.Iterators;

namespace Numeral
{
    public interface INDIterator<T> : IDisposable
    {
        IteratorFlags Flags { get; }
        Span<T> GetData();
        void PutData(in ReadOnlySpan<T> data);
        bool MoveNext();
        void MoveTo(int linearOffset);
    }

}
