using System;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        public static Memory<TTo> ReinterpretCast<TFrom, TTo>(this in Memory<TFrom> memory)
        {
            if (typeof(TFrom) == typeof(TTo))
                return (Memory<TTo>)(object)memory;

            throw new NotImplementedException();
        }

        public static NDArray<TTo> ReinterpretCast<TFrom, TTo>(this in NDArray<TFrom> arr)
        {
            return new NDArray<TTo>(
                arr._buffer.ReinterpretCast<TFrom, TTo>(),
                arr._shape,
                arr._strides,
                arr.Count,
                arr._flags);
        }

    }


}