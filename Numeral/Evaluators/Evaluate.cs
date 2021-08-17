using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Numeral.Iterators;

namespace Numeral.Evaluators
{
    /// <summary>
    /// Binary operation on 2 array buffers
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="result"></param>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <returns></returns>
    public delegate void BinaryOp<X, Y, R>(
        in ReadOnlySpan<X> x, in ReadOnlySpan<Y> y, in Span<R> result);

    /// <summary>
    /// Binary operation on scalar and array buffer
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="result"></param>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <returns></returns>
    public delegate void BinaryOp1<X, Y, R>(
        X x, in ReadOnlySpan<Y> y, in Span<R> result);

    /// <summary>
    /// Binary operation on an array buffer and scalar
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="result"></param>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <returns></returns>
    public delegate void BinaryOp2<X, Y, R>(
        in ReadOnlySpan<X> x, Y y, in Span<R> result);

    /// <summary>
    /// Unary operation on an array buffer
    /// </summary>
    /// <param name="x"></param>
    /// <param name="result"></param>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <returns></returns>
    public delegate void UnaryOp<X, R>(
        in ReadOnlySpan<X> x, in Span<R> result);

    public static class Evaluate
    {
        private static bool AreContiguous<X, Y>(in NDArray<X> x, in NDArray<Y> y)
        {
            return x.Rank == y.Rank &&
                x.IsContiguous() && y.IsContiguous() &&
                x.Shape.SequenceEqual(y.Shape);
        }

        private static bool AreContiguous<X, Y, Z>(in NDArray<X> x, in NDArray<Y> y, in NDArray<Z> z)
        {
            return AreContiguous(x, y) && AreContiguous(x, z);
        }

        /// <summary>
        /// Element-wise unary operation where the result is created from the
        /// x operand.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="op"></param>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public static unsafe NDArray<R> ElementWiseUnaryOp<X, R>(
            in NDArray<X> x,
            in UnaryOp<X, R> op)
        {
            var result = NDArray.EmptyLike<X, R>(x);
            return ElementWiseUnaryOp(x, result, op);
        }

        /// <summary>
        /// Executes the op using element-wise outer broadcasting
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="result"></param>
        /// <param name="op"></param>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public static unsafe NDArray<R> ElementWiseUnaryOp<X, R>(
            in NDArray<X> x,
            in NDArray<R> result,
            in UnaryOp<X, R> op)
        {
            if (AreContiguous(x, result))
            {
                op(x, result);
                return result;
            }

            var ri = result.GetIterator(result.Shape, 1);
            var xi = x.GetIterator(result.Shape, 1);
            RunP(xi, ri, op);
            return result;
        }

        public static unsafe NDArray<R> ElementWiseBinaryOp<X, Y, R>(
            in NDArray<X> x,
            in NDArray<Y> y,
            in BinaryOp<X, Y, R> op)
        {
            var rank = Math.Max(x.Rank, y.Rank);
            Span<int> shape = new int[rank];

            IteratorHelpers.UpdateResultShape(shape, x.Shape);
            IteratorHelpers.UpdateResultShape(shape, y.Shape);

            var result = NDArray.Empty<R>(shape.ToArray());
            return ElementWiseBinaryOp(x, y, result, op);
        }

        public static unsafe NDArray<R> ElementWiseBinaryOp<X, Y, R>(
            X x, in NDArray<Y> y,
            in BinaryOp1<X, Y, R> op)
        {
            var result = NDArray.EmptyLike<Y, R>(y);
            return ElementWiseBinaryOp(x, y, result, op);
        }

        public static unsafe NDArray<R> ElementWiseBinaryOp<X, Y, R>(
            in NDArray<X> x, Y y,
            in BinaryOp2<X, Y, R> op)
        {
            var result = NDArray.EmptyLike<X, R>(x);
            return ElementWiseBinaryOp(x, y, result, op);
        }

        /// <summary>
        /// Executes the op using element-wise outer broadcasting
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="result"></param>
        /// <param name="op"></param>
        /// <typeparam name="X"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        public static unsafe NDArray<R> ElementWiseBinaryOp<X, Y, R>(
            in NDArray<X> x,
            in NDArray<Y> y,
            in NDArray<R> result,
            in BinaryOp<X, Y, R> op)
        {
            if (AreContiguous(x, y, result))
            {
                op(x, y, result);
                return result;
            }

            Span<int> xorder = stackalloc int[]
            {
                x.Rank - 2,
                x.Rank - 1
            };

            Span<int> yorder = stackalloc int[]
            {
                y.Rank - 2,
                y.Rank - 1
            };

            var xi = x.GetIterator(result.Shape, xorder, 1);
            var yi = y.GetIterator(result.Shape, yorder, 1);
            var ri = result.GetIterator(result.Shape, 1);

            Run(xi, yi, ri, op);

            return result;
        }

        public static unsafe NDArray<R> ElementWiseBinaryOp<X, Y, R>(
            X x, in NDArray<Y> y,
            in NDArray<R> result,
            in BinaryOp1<X, Y, R> op)
        {
            if (AreContiguous(y, result))
            {
                op(x, y, result);
                return result;
            }

            var ri = result.GetIterator(result.Shape, 1);
            var yi = y.GetIterator(result.Shape, 1);
            Run(x, yi, ri, op);
            return result;
        }

        public static unsafe NDArray<R> ElementWiseBinaryOp<X, Y, R>(
            in NDArray<X> x, Y y,
            in NDArray<R> result,
            in BinaryOp2<X, Y, R> op)
        {
            if (AreContiguous(x, result))
            {
                op(x, y, result);
                return result;
            }

            var ri = result.GetIterator(result.Shape, 1);
            var xi = x.GetIterator(result.Shape, 1);
            Run(xi, y, ri, op);
            return result;
        }

        public static unsafe R ReduceUnaryOp<X, R>(
            in NDArray<X> x,
            in UnaryOp<X, R> reduceOp)
            where R : unmanaged
        {
            Span<R> result = stackalloc R[1];
            reduceOp(x, result);
            return result[0];
        }

        public static unsafe NDArray<R> ReduceUnaryOp<X, R>(
            in NDArray<X> x,
            in UnaryOp<X, R> reduceOp,
            int axis)
        {
            if (axis > x.Rank - 1)
                throw new ArgumentOutOfRangeException(nameof(axis), "assert failed: axis <= x.Rank - 1");

            if (x.Rank == 1)
                return ReduceUnaryOp(x, NDArray.Empty<R>(1), reduceOp, axis);

            Span<int> resultShape = stackalloc int[x.Rank - 1];
            for (int i = 0, j = 0; i < x.Rank; i++)
            {
                if (i == axis) continue;
                resultShape[j++] = x.Shape[i];
            }

            var result = NDArray.Empty<R>(resultShape.ToArray());
            return ReduceUnaryOp(x, result, reduceOp, axis);
        }

        public static unsafe NDArray<R> ReduceUnaryOp<X, R>(
            in NDArray<X> x,
            in NDArray<R> result,
            in UnaryOp<X, R> reduceOp,
            int axis)
        {
            if (axis > x.Rank - 1)
                throw new ArgumentOutOfRangeException(nameof(axis), "assert failed: axis <= x.Rank - 1");

            if (!result.IsContiguous())
                throw new Exception();

            Span<int> order = stackalloc int[x.Rank];
            CreateOrderForAxis(order, axis);

            var xi = x.GetIterator(x.Shape, order, 1);
            var ri = result.GetIterator(result.Shape, 0);
            Run(xi, ri, reduceOp);
            return result;
        }

        public static unsafe NDArray<R> AccumulateUnaryOp<X, R>(
            in NDArray<X> x,
            in UnaryOp<X, R> accOp,
            int axis)
            => AccumulateUnaryOp(x, NDArray.EmptyLike<X, R>(x), accOp, axis);

        public static unsafe NDArray<R> AccumulateUnaryOp<X, R>(
            in NDArray<X> x,
            in NDArray<R> result,
            in UnaryOp<X, R> accOp,
            int axis)
        {
            Span<int> order = stackalloc int[x.Rank];
            CreateOrderForAxis(order, axis);

            var xi = x.GetIterator(x.Shape, order, 1);
            var ri = result.GetIterator(result.Shape, order, 1);
            Run(xi, ri, accOp);
            return result;
        }

        private static void CreateOrderForAxis(in Span<int> order, int axis)
        {
            order[^1] = axis;
            for (int i = 0, j = 0; i < order.Length; i++)
            {
                if (i == axis) continue;
                order[j++] = i;
            }
        }

        public static void Run<A, B, C>(
            NDIterator<A> i1,
            NDIterator<B> i2,
            NDIterator<C> i3,
            in BinaryOp<A, B, C> op)
        {
            while (true)
            {
                var resultBuffer = i3.GetData();
                op(i1.GetData(), i2.GetData(), resultBuffer);

                if (i3.InnerLoopType == StrideType.Buffered)
                    i3.GetResultFromBuffer(resultBuffer);

                if (!i3.IncrementPointer()) break;
                i1.IncrementPointer();
                i2.IncrementPointer();
            }

            i3.Dispose();
            i2.Dispose();
            i1.Dispose();
        }

        public static void Run<A, B, C>(
            A a,
            NDIterator<B> i2,
            NDIterator<C> i3,
            in BinaryOp1<A, B, C> op)
        {
            while (true)
            {
                var resultBuffer = i3.GetData();
                op(a, i2.GetData(), resultBuffer);

                if (i3.InnerLoopType == StrideType.Buffered)
                    i3.GetResultFromBuffer(resultBuffer);

                if (!i3.IncrementPointer()) break;
                i2.IncrementPointer();
            }

            i3.Dispose();
            i2.Dispose();
        }

        public static void Run<A, B, C>(
            NDIterator<A> i1,
            B b,
            NDIterator<C> i3,
            in BinaryOp2<A, B, C> op)
        {
            while (true)
            {
                var resultBuffer = i3.GetData();
                op(i1.GetData(), b, resultBuffer);

                if (i3.InnerLoopType == StrideType.Buffered)
                    i3.GetResultFromBuffer(resultBuffer);

                if (!i3.IncrementPointer()) break;
                i1.IncrementPointer();
            }

            i3.Dispose();
            i1.Dispose();
        }

        #region Element-wise Parallel Runners

        public static void RunP<A, B, C>(
            NDIterator<A> i1,
            NDIterator<B> i2,
            NDIterator<C> i3,
            BinaryOp<A, B, C> op)
        {
            var n = Environment.ProcessorCount;
            var axis = 0;

            Parallel.For(0, n, i
                => Run(
                    i1.Divide(axis, n, i),
                    i2.Divide(axis, n, i),
                    i3.Divide(axis, n, i), op));

            i3.Dispose();
            i2.Dispose();
            i1.Dispose();
        }

        public static void RunP<A, B, C>(
            A a,
            NDIterator<B> i2,
            NDIterator<C> i3,
            BinaryOp1<A, B, C> op)
        {
            var n = Environment.ProcessorCount;
            Parallel.For(0, n, i
                => Run(
                    a,
                    i2.Divide(0, n, i),
                    i3.Divide(0, n, i), op));

            i3.Dispose();
            i2.Dispose();
        }

        public static void RunP<A, B, C>(
            NDIterator<A> i1,
            B b,
            NDIterator<C> i3,
            BinaryOp2<A, B, C> op)
        {
            var n = Environment.ProcessorCount;
            Parallel.For(0, n, i
                => Run(
                    i1.Divide(0, n, i),
                    b,
                    i3.Divide(0, n, i), op));

            i3.Dispose();
            i1.Dispose();
        }

        #endregion

        public static unsafe void Run<A, B>(
            NDIterator<A> i1,
            NDIterator<B> i2,
            in UnaryOp<A, B> op)
        {
            while (true)
            {
                var resultBuffer = i2.GetData();
                op(i1.GetData(), resultBuffer);

                // need to copy data into the result since
                // a temporary buffer was used
                if (i2.InnerLoopType == StrideType.Buffered)
                    i2.GetResultFromBuffer(resultBuffer);

                if (!i2.IncrementPointer()) break;
                i1.IncrementPointer();
            }

            i2.Dispose();
            i1.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="i3"></param>
        /// <param name="op"></param>
        /// <typeparam name="T"></typeparam>
        public static void RunP<A, B>(
            NDIterator<A> i1,
            NDIterator<B> i2,
            UnaryOp<A, B> op)
        {
            var n = Environment.ProcessorCount;

            Parallel.For(0, n, i =>
                Run(i1.Divide(0, n, i), i2.Divide(0, n, i), op));

            i2.Dispose();
            i1.Dispose();
        }

        /// <summary>
        /// Copies values from <paramref name="tmpBuffer"/> into the memory
        /// being iterated. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iter"></param>
        /// <param name="tmpBuffer"></param>
        private static unsafe void GetResultFromBuffer<T>(this in NDIterator<T> iter, in ReadOnlySpan<T> tmpBuffer)
        {
            var p = iter.PrevDataPointer;
            var stride = iter.Strides[iter.Rank - 1];
            for (var i = 0; i < iter.OperandSize; i++)
            {
                Unsafe.Write(p, tmpBuffer[i]);
                p = Unsafe.Add<T>(p, stride);
            }
        }
    }

}
