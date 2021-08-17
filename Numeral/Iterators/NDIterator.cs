using System;
using System.Buffers;

namespace Numeral.Iterators
{
    public unsafe struct NDIterator<T>
    {
        public NDIterator(int rank)
        {
            FullRank = rank;
            Rank = rank;

            InnerLoopType = StrideType.None;
            BorrowedMemory = null;
            DataPointer = null;
            PrevDataPointer = null;
            OperandBuffer = null;
            OperandSize = -1;
        }

        public StrideType InnerLoopType { get; set; }
        public int FullRank { get; set; }
        public int Rank { get; set; }
        public int OperandSize { get; set; }

        /// <summary>
        /// A temporary buffer for non-contiguous arrays
        /// </summary>
        public IMemoryOwner<T> BorrowedMemory { get; set; }

        /// <summary>
        /// Offset in each dim where the iterator is reset / initialized to
        /// </summary>
        public fixed int Offsets[Constants.MaxStackAlloc];
        public fixed int FullShape[Constants.MaxStackAlloc];
        public fixed int Strides[Constants.MaxStackAlloc];
        public fixed int Backstrides[Constants.MaxStackAlloc];
        public fixed int Indices[Constants.MaxStackAlloc];

        public void* DataPointer { get; set; }
        public void* PrevDataPointer { get; set; }
        public void* OperandBuffer { get; set; }

        public void Dispose()
        {
            if (BorrowedMemory != null)
                BorrowedMemory.Dispose();
        }

        public override string ToString()
        {
            return $"{InnerLoopType}, Rank={Rank}, OpSize={OperandSize}";
        }
    }
}
