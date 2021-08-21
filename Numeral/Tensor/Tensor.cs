using System;
using Numeral.Internals;
using Numeral.Iterators;

namespace Numeral
{

    public abstract partial class Tensor<T> : TensorBase
    {
        internal Tensor(
            in ReadOnlyMemory<int> shape,
            in ReadOnlyMemory<int> strides,
            int count)
            : base(shape, strides, count)
        {
        }

        public abstract ref T GetFlat(int index);

        public abstract ref T this[params int[] indices] { get; }
        public abstract ref T this[params Index[] indices] { get; }
        public abstract ref T this[in ReadOnlySpan<int> indices] { get; }
        public abstract ref T this[in ReadOnlySpan<Index> indices] { get; }

        public abstract Tensor<T> Squeeze();
        public abstract Tensor<T> Reshape(params int[] newShape);
        public abstract Tensor<T> Reshape(in ReadOnlySpan<int> newShape);
        public abstract Tensor<T> Transpose();
        public abstract Tensor<T> Transpose(params int[] indices);
        public abstract Tensor<T> Transpose(in ReadOnlySpan<int> indices);
        public abstract Tensor<T>[] Split(int segments, int axis = 0);
        public abstract Tensor<T> Slice(params Slice[] slices);
        public abstract Tensor<T> Slice(in ReadOnlySpan<Slice> slices);
        public abstract Tensor<T> Copy();
        public abstract Tensor<R> Cast<R>();
        public abstract Tensor<R> ReinterpretCast<R>();

        public abstract ITensorCore<T> Core { get; }
        public abstract IFactory Factory { get; }
        public abstract INDIterator<T> GetIterator(int operandRank = 1);
        public abstract INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, int operandRank = 1);
        public abstract INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, in ReadOnlySpan<int> order, int operandRank = 1);

        public static implicit operator Tensor<T>(T[] array) => array.AsTensor();
        public static implicit operator Tensor<T>(T[,] array) => array.AsTensor();
        public static implicit operator Tensor<T>(T[,,] array) => array.AsTensor();
    }

    public interface INDIterator<T> : IDisposable
    {
        IteratorFlags Flags { get; }
        Span<T> GetData();
        void PutData(in ReadOnlySpan<T> data);
        bool MoveNext();
    }

}
