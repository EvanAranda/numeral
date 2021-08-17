using System;
using System.Runtime.CompilerServices;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        public static unsafe T[,] ToArray2d<T>(this in NDArray<T> array)
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

        public static unsafe T[,,] ToArray3d<T>(this in NDArray<T> array)
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

        public static void SumF(NDArray<float> x, Func<float, float> f)
        {
            void Op(in ReadOnlySpan<float> arg, in Span<float> result)
            {
                var sum = 0.0f;
                for (int i = 0; i < arg.Length; i++)
                    sum += f(arg[i]);
                result[0] = sum;
            };
            Evaluators.Evaluate.ReduceUnaryOp<float, float>(x, Op, 0);
        }
    }
}