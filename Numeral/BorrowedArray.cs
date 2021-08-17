using System;
using System.Buffers;

namespace Numeral
{
    public readonly struct BorrowedArray<T> : IDisposable
    {
        private readonly IMemoryOwner<T> _memoryOwner;
        private readonly NDArray<T> _array;

        public BorrowedArray(IMemoryOwner<T> memoryOwner, NDArray<T> array)
        {
            _memoryOwner = memoryOwner;
            _array = array;
        }

        public NDArray<T> Array => _array;

        public void Dispose() => _memoryOwner.Dispose();
        public static implicit operator NDArray<T>(BorrowedArray<T> borrowedArray) => borrowedArray.Array;
    }


}
