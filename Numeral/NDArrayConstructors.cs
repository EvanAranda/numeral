using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Numeral.Iterators;

namespace Numeral
{

    /// <summary>
    /// NDArray constructors
    /// </summary>
    public static partial class NDArray
    {
        public static NDArray<T> Linspace<T>(T start, T end, int n = 2)
        {
            if (typeof(T) == typeof(float))
                return Linspace_float((float)(object)start, (float)(object)end, n).ReinterpretCast<float, T>();
            if (typeof(T) == typeof(double))
                return Linspace_double((double)(object)start, (double)(object)end, n).ReinterpretCast<double, T>();
            throw new NotSupportedException();
        }

        private static NDArray<float> Linspace_float(float start, float end, int n = 2)
        {
            if (n < 2)
                throw new ArgumentOutOfRangeException(nameof(n), "assert failed: n >= 2");

            var arr = Empty<float>(n);
            var s = (Span<float>)arr;

            var x = start;
            var step = (end - start) / (n - 1);
            for (var i = 0; i < n; i++)
            {
                s[i] = x;
                x += step;
            }

            return arr;
        }

        private static NDArray<double> Linspace_double(double start, double end, int n = 2)
        {
            if (n < 2)
                throw new ArgumentOutOfRangeException(nameof(n), "assert failed: n >= 2");

            var arr = Empty<double>(n);
            var s = (Span<double>)arr;

            var x = start;
            var step = (end - start) / (n - 1);
            for (var i = 0; i < n; i++)
            {
                s[i] = x;
                x += step;
            }

            return arr;
        }

        public static BorrowedArray<T> Rent<T>(T arg)
        {
            var memoryOwner = MemoryPool<T>.Shared.Rent(1);
            var memory = memoryOwner.Memory;
            memory.Span[0] = arg;
            var array = new NDArray<T>(memory.Slice(0, 1));
            return new BorrowedArray<T>(memoryOwner, array);
        }

        public static NDArray<T> Empty<T>(params int[] shape)
            => Empty<T>((ReadOnlyMemory<int>)shape);

        public static NDArray<T> Empty<T>(ReadOnlyMemory<int> shape)
        {
            var count = ArrayHelpers.GetProductSum(shape.Span);
            var buffer = new T[count];
            var strides = new int[shape.Length];
            IteratorHelpers.GetStrides(shape.Span, strides);
            return new NDArray<T>(buffer, shape, strides, count, ArrayFlags.IS_CONTIGUOUS);
        }

        /// <summary>
        /// Creates a clone of the same shape with contiguous strides
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static NDArray<T> EmptyLike<T>(this in NDArray<T> arr)
        {
            var strides = IteratorHelpers.GetStrides(arr.Shape);
            return new NDArray<T>(new T[arr.Count], arr._shape, strides, arr.Count, ArrayFlags.IS_CONTIGUOUS);
        }

        public static NDArray<R> EmptyLike<T, R>(this in NDArray<T> arr)
        {
            var strides = IteratorHelpers.GetStrides(arr.Shape);
            return new NDArray<R>(new R[arr.Count], arr._shape, strides, arr.Count, ArrayFlags.IS_CONTIGUOUS);
        }

        public static NDArray<T> Arange<T>(int count)
        {
            var dtype = DTypes.GetDType<T>();
            if (!dtype.IsNumeric)
                throw new Exception();

            var arr = Empty<T>(new int[] { count });

            for (var i = 0; i < count; i++)
                arr._buffer.Span[i] = dtype.Cast(i);

            return arr;
        }

        public static NDArray<T> Ones<T>(params int[] shape)
            => Constant(DTypes.GetDType<T>().One, shape);

        public static NDArray<T> Zeros<T>(params int[] shape)
            => Constant(DTypes.GetDType<T>().Zero, shape);

        public static NDArray<T> Constant<T>(T value, in ReadOnlyMemory<int> shape)
        {
            var buffer = new T[ArrayHelpers.GetProductSum(shape.Span)];
            buffer.AsSpan().Fill(value);
            return new NDArray<T>(buffer, shape);
        }

        public static NDArray<T> AsNDArray<T>(this Memory<T> memory) => memory;

        public static NDArray<T> AsNDArray<T>(this Memory<T> arr, params int[] shape)
            => AsNDArray(arr, (ReadOnlyMemory<int>)shape);

        public static NDArray<T> AsNDArray<T>(this Memory<T> memory, ReadOnlyMemory<int> shape)
        {
            return new NDArray<T>(memory, shape);
        }

        public static NDArray<T> AsNDArray<T>(this T[] arr) => arr;

        public static NDArray<T> AsNDArray<T>(this T[] arr, params int[] shape)
            => AsNDArray(arr, (ReadOnlyMemory<int>)shape);

        public static NDArray<T> AsNDArray<T>(this T[] arr, ReadOnlyMemory<int> shape)
        {
            return new NDArray<T>(arr, shape);
        }

        public static unsafe NDArray<T> AsNDArray<T>(T[,] arr)
        {
            var shape = new int[] { arr.GetLength(0), arr.GetLength(1) };
            var count = ArrayHelpers.GetProductSum(shape);
            var strides = IteratorHelpers.GetStrides(shape);
            var buffer = new T[count];

            var bytesCount = Unsafe.SizeOf<T>() * count;
            var pDest = Unsafe.AsPointer(ref buffer[0]);
            var pSrc = Unsafe.AsPointer(ref arr[0, 0]);
            Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return new NDArray<T>(buffer, shape, strides, count, ArrayFlags.IS_CONTIGUOUS);
        }

        public static unsafe NDArray<T> AsNDArray<T>(T[,,] arr)
        {
            var shape = new int[] { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2) };
            var count = ArrayHelpers.GetProductSum(shape);
            var strides = IteratorHelpers.GetStrides(shape);
            var buffer = new T[count];

            var bytesCount = Unsafe.SizeOf<T>() * count;
            var pDest = Unsafe.AsPointer(ref buffer[0]);
            var pSrc = Unsafe.AsPointer(ref arr[0, 0, 0]);
            Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return new NDArray<T>(buffer, shape, strides, count, ArrayFlags.IS_CONTIGUOUS);
        }

        public static NDArray<T> Stack<T>(IEnumerable<NDArray<T>> arrays, int axis)
        {
            throw new NotImplementedException();
        }

    }


}