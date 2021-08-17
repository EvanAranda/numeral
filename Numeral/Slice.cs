using System;

namespace Numeral
{
    public readonly struct Slice
    {
        public Slice(Range range)
        {
            Range = range;
        }

        public Range Range { get; }

        public static implicit operator Slice(in Range range)
        {
            return new Slice(range);
        }

        public static implicit operator Slice(int index)
        {
            return new Range(index, index + 1);
        }

        public static implicit operator Slice(in Index index)
        {
            if (index.IsFromEnd)
                return new Range(index, Index.FromEnd(index.Value - 1));

            return new Range(index, Index.FromStart(index.Value + 1));
        }
    }


}
