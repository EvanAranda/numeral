using System;
using System.Runtime.CompilerServices;
using Numeral.Iterators;

namespace Numeral
{
    public interface IFactory
    {
        Tensor<T> Zeros<T>(params int[] shape);
        Tensor<T> Zeros<T>(in ReadOnlySpan<int> shape);
        Tensor<Y> ZerosLike<X, Y>(Tensor<X> tensor);
        Tensor<T> Ones<T>(params int[] shape);
        Tensor<T> Ones<T>(in ReadOnlySpan<int> shape);
        Tensor<T> Eye<T>(params int[] shape);
        Tensor<T> Eye<T>(in ReadOnlySpan<int> shape);
    }

    public abstract class FactoryBase : IFactory
    {
        public Tensor<T> Eye<T>(params int[] shape)
            => Eye<T>((ReadOnlySpan<int>)shape);

        public Tensor<T> Ones<T>(params int[] shape)
            => Ones<T>((ReadOnlySpan<int>)shape);

        public Tensor<T> Zeros<T>(params int[] shape)
            => Zeros<T>((ReadOnlySpan<int>)shape);

        public abstract Tensor<T> Eye<T>(in ReadOnlySpan<int> shape);
        public abstract Tensor<T> Ones<T>(in ReadOnlySpan<int> shape);
        public abstract Tensor<T> Zeros<T>(in ReadOnlySpan<int> shape);
        public abstract Tensor<Y> ZerosLike<X, Y>(Tensor<X> tensor);
    }

    public class DenseTensorFactory : FactoryBase, IFactory
    {
        public static readonly DenseTensorFactory Instance = new();

        public override Tensor<T> Zeros<T>(in ReadOnlySpan<int> shape)
        {
            var count = ArrayHelpers.GetProductSum(shape);
            var buffer = new T[count];
            return new DenseTensor<T>(
                buffer, shape.ToArray(), IteratorHelpers.GetStrides(shape),
                count, ArrayFlags.IsContiguous);
        }

        public override Tensor<T> Ones<T>(in ReadOnlySpan<int> shape)
            => Constant<T>(DTypes.GetDType<T>().One, shape);

        public override Tensor<R> ZerosLike<T, R>(Tensor<T> arr)
        {
            return new DenseTensor<R>(new R[arr.Count], arr._shape,
                IteratorHelpers.GetStrides(arr.Shape), arr.Count,
                ArrayFlags.IsContiguous);
        }

        public static Tensor<T> Constant<T>(T value, in ReadOnlySpan<int> shape)
        {
            var buffer = new T[ArrayHelpers.GetProductSum(shape)];
            buffer.AsSpan().Fill(value);
            return new DenseTensor<T>(buffer, shape.ToArray());
        }

        public override Tensor<T> Eye<T>(in ReadOnlySpan<int> shape)
        {
            var tensor = Zeros<T>(shape);
            Span<int> indices = stackalloc int[tensor.Rank];
            var one = DTypes.GetDType<T>().One;

            for (var i = 0; i < indices.Length; i++)
            {
                indices.Fill(i);
                tensor[indices] = one;
            }

            return tensor;
        }

    }

    /// <summary>
    /// NDArray constructors
    /// </summary>
    public static partial class Tensor
    {
        public static readonly DenseTensorFactory Dense = DenseTensorFactory.Instance;

        /// <summary>
        /// Create a 1-d grid space
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="npoints"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Grid<T> LinSpace<T>(T start, T end, int npoints = 2)
            where T : unmanaged
        {
            return new Grid<T>(
                new T[] { start },
                new T[] { end },
                new int[] { npoints },
                npoints
            );
        }

        /// <summary>
        /// Create an n-d grid space with uniform spacing of points on each 
        /// axis.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="npoints"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Grid<T> GridSpace<T>(in ReadOnlySpan<T> start, in ReadOnlySpan<T> end, int npoints)
            where T : unmanaged
        {
            Span<int> npoints_arr = stackalloc int[start.Length];
            npoints_arr.Fill(npoints);

            return new Grid<T>(
                start.ToArray(),
                end.ToArray(),
                npoints_arr.ToArray(),
                ArrayHelpers.GetProductSum(npoints_arr));
        }

        /// <summary>
        /// Create an n-d grid space with custom point spacing on each axis.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="npoints"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Grid<T> GridSpace<T>(in ReadOnlySpan<T> start, in ReadOnlySpan<T> end, in ReadOnlySpan<int> npoints)
            where T : unmanaged
        {
            return new Grid<T>(
                start.ToArray(),
                end.ToArray(),
                npoints.ToArray(),
                npoints.GetProductSum());
        }

        public static Tensor<T> Arange<T>(int count, T startAt = default)
        {
            // var dtype = DTypes.GetDType<T>();
            // if (!dtype.IsNumeric)
            //     throw new Exception();

            // var data = new T[count];
            // for (var i = 0; i < count; i++)
            //     data[i] = dtype.Cast(i + startAt);
            // return new DenseTensor<T>(data);

            var tensor = Dense.Ones<T>(count);
            tensor.GetFlat(0) = startAt;
            return tensor.Cumsum(0, tensor);
        }

        public static Tensor<T> AsTensor<T>(this in Memory<T> memory)
            => new DenseTensor<T>(memory);

        public static Tensor<T> AsTensor<T>(this in Memory<T> arr, params int[] shape)
            => AsTensor(arr, (ReadOnlySpan<int>)shape);

        public static Tensor<T> AsTensor<T>(this in Memory<T> memory, in ReadOnlySpan<int> shape)
            => new DenseTensor<T>(memory, shape.ToArray());

        public static Tensor<T> AsTensor<T>(this T[] arr)
            => new DenseTensor<T>(arr);

        public static Tensor<T> AsTensor<T>(this T[] arr, params int[] shape)
            => AsTensor(arr, (ReadOnlySpan<int>)shape);

        public static Tensor<T> AsTensor<T>(this T[] arr, in ReadOnlySpan<int> shape)
            => new DenseTensor<T>(arr, shape.ToArray());

        public static unsafe Tensor<T> AsTensor<T>(this T[,] arr)
        {
            Span<int> shape = stackalloc int[]
            {
                arr.GetLength(0),
                arr.GetLength(1)
            };

            var count = ArrayHelpers.GetProductSum(shape);
            var strides = IteratorHelpers.GetStrides(shape);
            var buffer = new T[count];

            var bytesCount = Unsafe.SizeOf<T>() * count;
            var pDest = Unsafe.AsPointer(ref buffer[0]);
            var pSrc = Unsafe.AsPointer(ref arr[0, 0]);
            Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return new DenseTensor<T>(buffer, shape.ToArray(), strides, count,
                ArrayFlags.IsContiguous);
        }

        public static unsafe Tensor<T> AsTensor<T>(this T[,,] arr)
        {
            Span<int> shape = stackalloc int[]
            {
                arr.GetLength(0),
                arr.GetLength(1),
                arr.GetLength(2)
            };

            var count = ArrayHelpers.GetProductSum(shape);
            var strides = IteratorHelpers.GetStrides(shape);
            var buffer = new T[count];

            var bytesCount = Unsafe.SizeOf<T>() * count;
            var pDest = Unsafe.AsPointer(ref buffer[0]);
            var pSrc = Unsafe.AsPointer(ref arr[0, 0, 0]);
            Unsafe.CopyBlock(pDest, pSrc, (uint)bytesCount);

            return new DenseTensor<T>(buffer, shape.ToArray(), strides, count,
                ArrayFlags.IsContiguous);
        }

        public static Tensor<T> Stack<T>(Tensor<T>[] arrays, int axis)
        {
            Span<int> shape = arrays.Length > 256 ? new int[arrays.Length] : stackalloc int[arrays.Length];
            Span<int> segments = arrays.Length > 256 ? new int[arrays.Length + 1] : stackalloc int[arrays.Length + 1];

            var rank = 0;

            for (var i = 0; i < arrays.Length; i++)
            {
                var array = arrays[i];

                if (i == 0)
                {
                    rank = array.Rank;
                    array.Shape.CopyTo(shape);
                }
                else
                {
                    if (array.Rank != rank)
                        throw new Exception("all arrays must have the same rank");

                    // loop each axis
                    for (var j = 0; j < rank; j++)
                    {
                        if (j == axis)
                        {
                            shape[j] += array.Shape[j];
                            segments[i + 1] = segments[i] + array.Shape[j];
                        }
                        else
                        {
                            if (array.Shape[j] != shape[j])
                                throw new Exception("all arrays must have the same shape on all axes other than the stacking axis");
                        }
                    }
                }

                i++;
            }

            var result = Dense.Zeros<T>(shape);

            // copy each array into the slice of the result it should occupy
            Span<Slice> slices = stackalloc Slice[rank];
            slices.Fill(Slice.All);

            for (var j = 0; j < arrays.Length; j++)
            {
                slices[axis] = (segments[j]..segments[j + 1]);
                Copy<T>.Instance.Call(arrays[j], result.Slice(slices));
            }

            return result;
        }

    }


}