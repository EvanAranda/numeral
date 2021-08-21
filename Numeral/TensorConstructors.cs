using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Numeral.Iterators;

namespace Numeral
{
    public interface IFactory
    {
        Tensor<T> Zeros<T>(params int[] shape);
        Tensor<Y> ZerosLike<X, Y>(Tensor<X> tensor);
        Tensor<T> Ones<T>(params int[] shape);
        Tensor<T> Eye<T>(params int[] shape);
    }

    /// <summary>
    /// NDArray constructors
    /// </summary>
    public static partial class Tensor
    {
        //public static Tensor<T> Linspace<T>(T start, T end, int n = 2)
        //{

        //    if (typeof(T) == typeof(float))
        //        return Linspace_float((float)(object)start, (float)(object)end, n).ReinterpretCast<float, T>();
        //    if (typeof(T) == typeof(double))
        //        return Linspace_double((double)(object)start, (double)(object)end, n).ReinterpretCast<double, T>();
        //    throw new NotSupportedException();
        //}

        //private static Tensor<float> Linspace_float(float start, float end, int n = 2)
        //{
        //    if (n < 2)
        //        throw new ArgumentOutOfRangeException(nameof(n), "assert failed: n >= 2");

        //    var arr = Empty<float>(n);
        //    var s = (Span<float>)arr;

        //    var x = start;
        //    var step = (end - start) / (n - 1);
        //    for (var i = 0; i < n; i++)
        //    {
        //        s[i] = x;
        //        x += step;
        //    }

        //    return arr;
        //}

        //private static Tensor<double> Linspace_double(double start, double end, int n = 2)
        //{
        //    if (n < 2)
        //        throw new ArgumentOutOfRangeException(nameof(n), "assert failed: n >= 2");

        //    var arr = Empty<double>(n);
        //    var s = (Span<double>)arr;

        //    var x = start;
        //    var step = (end - start) / (n - 1);
        //    for (var i = 0; i < n; i++)
        //    {
        //        s[i] = x;
        //        x += step;
        //    }

        //    return arr;
        //}

        public static Tensor<T> Empty<T>(params int[] shape)
            => Empty<T>((ReadOnlyMemory<int>)shape);

        public static Tensor<T> Empty<T>(ReadOnlyMemory<int> shape)
        {
            var count = ArrayHelpers.GetProductSum(shape.Span);
            var buffer = new T[count];
            var strides = new int[shape.Length];
            IteratorHelpers.GetStrides(shape.Span, strides);
            return new DenseTensor<T>(buffer, shape, strides, count, ArrayFlags.IsContiguous);
        }

        /// <summary>
        /// Creates a clone of the same shape with contiguous strides
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Tensor<T> EmptyLike<T>(this Tensor<T> arr)
        {
            var strides = IteratorHelpers.GetStrides(arr.Shape);
            return new DenseTensor<T>(new T[arr.Count], arr._shape, strides, arr.Count, ArrayFlags.IsContiguous);
        }

        public static Tensor<R> EmptyLike<T, R>(this Tensor<T> arr)
        {
            var strides = IteratorHelpers.GetStrides(arr.Shape);
            return new DenseTensor<R>(new R[arr.Count], arr._shape, strides, arr.Count, ArrayFlags.IsContiguous);
        }

        public static Tensor<T> Arange<T>(int count, int startAt = 0)
        {
            var dtype = DTypes.GetDType<T>();
            if (!dtype.IsNumeric)
                throw new Exception();

            var data = new T[count];
            for (var i = 0; i < count; i++)
                data[i] = dtype.Cast(i + startAt);

            return new DenseTensor<T>(data);
        }

        public static Tensor<T> Ones<T>(params int[] shape)
            => Constant(DTypes.GetDType<T>().One, shape);

        public static Tensor<T> Zeros<T>(params int[] shape)
            => Constant(DTypes.GetDType<T>().Zero, shape);

        public static Tensor<T> Constant<T>(T value, in ReadOnlyMemory<int> shape)
        {
            var buffer = new T[ArrayHelpers.GetProductSum(shape.Span)];
            buffer.AsSpan().Fill(value);
            return new DenseTensor<T>(buffer, shape);
        }

        public static Tensor<T> AsTensor<T>(this in Memory<T> memory)
            => new DenseTensor<T>(memory);

        public static Tensor<T> AsTensor<T>(this in Memory<T> arr, params int[] shape)
            => AsTensor(arr, (ReadOnlyMemory<int>)shape);

        public static Tensor<T> AsTensor<T>(this in Memory<T> memory, in ReadOnlyMemory<int> shape)
            => new DenseTensor<T>(memory, shape);

        public static Tensor<T> AsTensor<T>(this T[] arr)
            => new DenseTensor<T>(arr);

        public static Tensor<T> AsTensor<T>(this T[] arr, params int[] shape)
            => AsTensor(arr, (ReadOnlyMemory<int>)shape);

        public static Tensor<T> AsTensor<T>(this T[] arr, in ReadOnlyMemory<int> shape)
            => new DenseTensor<T>(arr, shape);

        public static unsafe Tensor<T> AsTensor<T>(this T[,] arr)
        {
            var shape = new int[] { arr.GetLength(0), arr.GetLength(1) };
            var count = ArrayHelpers.GetProductSum(shape);
            var strides = IteratorHelpers.GetStrides(shape);
            var buffer = new T[count];

            var bytesCount = Unsafe.SizeOf<T>() * count;
            var pDest = Unsafe.AsPointer(ref buffer[0]);
            var pSrc = Unsafe.AsPointer(ref arr[0, 0]);
            Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return new DenseTensor<T>(buffer, shape, strides, count, ArrayFlags.IsContiguous);
        }

        public static unsafe Tensor<T> AsTensor<T>(this T[,,] arr)
        {
            var shape = new int[] { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2) };
            var count = ArrayHelpers.GetProductSum(shape);
            var strides = IteratorHelpers.GetStrides(shape);
            var buffer = new T[count];

            var bytesCount = Unsafe.SizeOf<T>() * count;
            var pDest = Unsafe.AsPointer(ref buffer[0]);
            var pSrc = Unsafe.AsPointer(ref arr[0, 0, 0]);
            Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return new DenseTensor<T>(buffer, shape, strides, count, ArrayFlags.IsContiguous);
        }

        public static Tensor<T> Stack<T>(IEnumerable<Tensor<T>> arrays, int axis)
        {
            throw new NotImplementedException();

            //Span<int> shape = stackalloc int[8];
            //var rank = 0;
            //bool first = true;

            //// 
            //foreach (var array in arrays)
            //{
            //    if (first)
            //    {
            //        rank = array.Rank;
            //        array.Shape.CopyTo(shape);
            //    }
            //    else
            //    {
            //        for (int i = 0; i < rank; i++)
            //        {
            //            if (i == axis)
            //            {
            //                shape[i] += array.Shape[i];
            //            }
            //            else
            //            {
            //                if (array.Shape[i] != shape[i])
            //                    throw new Exception("all arrays must have the same shape on all axes other than the stacking axis");
            //            }
            //        }
            //    }
            //}

            //var result = NDArray.Empty<T>(shape.ToArray());

            //// now need to copy each array into the result. this can get tricky
            //// if stacking 

            //return result;
        }

    }


}