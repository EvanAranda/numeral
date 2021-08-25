using System;
using System.Runtime.CompilerServices;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class Tensor
    {
        public static unsafe T[,] ToArray2d<T>(this DenseTensor<T> array)
            where T : unmanaged
        {
            if (array.Rank != 2)
                throw new ArgumentException("assert failed: array.Rank == 2", nameof(array));

            var iter = array.GetIterator(array.Shape, 0);
            var result = new T[array.Shape[0], array.Shape[1]];

            var src = (Span<T>)array;
            var bytesCount = sizeof(T) * array.Count;
            fixed (void* pDest = result, pSrc = src)
                Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return result;
        }

        public static unsafe T[,,] ToArray3d<T>(this DenseTensor<T> array)
            where T : unmanaged
        {
            if (array.Rank != 3)
                throw new ArgumentException("assert failed: array.Rank == 3", nameof(array));

            var iter = array.GetIterator(array.Shape, 0);
            var result = new T[array.Shape[0], array.Shape[1], array.Shape[2]];

            var src = (Span<T>)array;
            var bytesCount = sizeof(T) * array.Count;
            fixed (void* pDest = result, pSrc = src)
                Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return result;
        }
    }
}