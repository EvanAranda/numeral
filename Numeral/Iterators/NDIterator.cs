using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Numeral.Iterators
{
    public unsafe struct NDIterator<T> : INDIterator<T>
    {
        public NDIterator(int rank, void* dataPointer)
        {
            FullRank = rank;
            Rank = rank;

            BaseDataPointer = dataPointer;
            DataPointer = dataPointer;

            Flags = IteratorFlags.None;
            BorrowedMemory = null;
            PrevDataPointer = null;
            OperandBuffer = null;
            OperandSize = -1;
        }

        public IteratorFlags Flags { get; set; }
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
        public fixed int Offsets[Constants.StaticAllocSize];
        public fixed int FullShape[Constants.StaticAllocSize];
        public fixed int Strides[Constants.StaticAllocSize];
        public fixed int Backstrides[Constants.StaticAllocSize];
        public fixed int Indices[Constants.StaticAllocSize];

        public void* BaseDataPointer { get; }
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
            return $"{Flags}, Rank={Rank}, OpSize={OperandSize}";
        }

        public Span<T> GetData()
        {
            if (DataPointer == PrevDataPointer)
                return new Span<T>(OperandBuffer, OperandSize);

            PrevDataPointer = DataPointer;

            var buf = Flags switch
            {
                IteratorFlags.Contiguous => new Span<T>(DataPointer, OperandSize),
                IteratorFlags.Filled => GetFilledData(),
                IteratorFlags.Buffered => GetBufferedData(),
                _ => throw new NotSupportedException(),
            };

            OperandBuffer = Unsafe.AsPointer(ref buf[0]);
            return buf;
        }

        public void PutData(in ReadOnlySpan<T> data)
        {
            Debug.Assert(data.Length == OperandSize);

            var p = PrevDataPointer;
            var stride = Strides[Rank - 1];
            for (var i = 0; i < data.Length; i++)
            {
                Unsafe.Write(p, data[i]);
                p = Unsafe.Add<T>(p, stride);
            }
        }

        public bool MoveNext()
        {
            if (Rank == 0)
                return false;

            for (var i = Rank - 1; i >= 0; i--)
            {
                if (Indices[i] == FullShape[i] - 1)
                {
                    if (i == 0)
                        return false;

                    DataPointer = Unsafe.Subtract<T>(DataPointer, Backstrides[i]);
                    Indices[i] = Offsets[i];

                    continue;
                }

                DataPointer = Unsafe.Add<T>(DataPointer, Strides[i]);
                Indices[i]++;

                break;
            }

            return true;
        }

        private unsafe Span<T> GetFilledData()
        {
            var span = BorrowedMemory.Memory.Span.Slice(0, OperandSize);
            span.Fill(Unsafe.Read<T>(DataPointer));
            return span;
        }

        private unsafe Span<T> GetBufferedData()
        {
            var length = OperandSize;
            var span = BorrowedMemory.Memory.Span.Slice(0, length);

            span[0] = Unsafe.Read<T>(DataPointer);
            for (var i = 1; i < length; i++)
            {
                MoveNext();
                span[i] = Unsafe.Read<T>(DataPointer);
            }

            return span;
        }

        public void MoveTo(int linearOffset)
        {
            DataPointer = Unsafe.Add<T>(BaseDataPointer, linearOffset);
        }
    }
}
