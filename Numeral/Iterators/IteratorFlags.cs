using System;

namespace Numeral.Iterators
{
    [Flags]
    public enum IteratorFlags : byte
    {
        None,
        Contiguous = 1 << 1,
        Filled = 1 << 2,
        Buffered = 1 << 3,
        Sparse = 1 << 4,
    }
}
