using System;
using Numeral.Expressions;
using Numeral.Internals;

namespace Numeral
{

    public abstract partial class Tensor<T> : TensorBase
    {
        internal Tensor(
            in ReadOnlyMemory<int> shape,
            int count)
            : base(shape, count)
        {
        }

        public abstract ref T GetFlat(int index);

        public abstract ref T this[params int[] indices] { get; }
        public abstract ref T this[params Index[] indices] { get; }
        public abstract ref T this[in ReadOnlySpan<int> indices] { get; }
        public abstract ref T this[in ReadOnlySpan<Index> indices] { get; }

        public virtual Tensor<T> this[Tensor<int> indices]
        {
            get => Tensor.Get(this, indices);
            set => Tensor.Set(this, indices, value);
        }

        public virtual Tensor<T> this[Tensor<bool> mask]
        {
            get => this[Tensor.ArgWhere(mask)];
            set => this[Tensor.ArgWhere(mask)] = value;
        }

        public abstract Tensor<T> Squeeze();
        public abstract Tensor<T> Reshape(in Span<int> newShape);
        public virtual Tensor<T> Reshape(params int[] newShape) => Reshape((Span<int>)newShape);
        public abstract Tensor<T> Transpose();
        public virtual Tensor<T> Transpose(params int[] indices) => Transpose((ReadOnlySpan<int>)indices);
        public abstract Tensor<T> Transpose(in ReadOnlySpan<int> indices);
        public abstract Tensor<T>[] Split(int segments, int axis = 0);
        public virtual Tensor<T> Slice(params Slice[] slices) => Slice((ReadOnlySpan<Slice>)slices);
        public abstract Tensor<T> Slice(in ReadOnlySpan<Slice> slices);
        public abstract Tensor<T> Copy();
        public abstract Tensor<R> Cast<R>();
        public abstract Tensor<R> ReinterpretCast<R>();

        public abstract ITensorCore<T> Core { get; }
        public abstract IFactory Factory { get; }
        public abstract INDIterator<T> GetIterator(int operandRank = 1);
        public abstract INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, int operandRank = 1);
        public abstract INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, in ReadOnlySpan<int> order, int operandRank = 1);

        public static implicit operator Tensor<T>(T scalar) => (Scalar<T>)scalar;
        public static implicit operator Tensor<T>(T[] array) => array.AsTensor();
        public static implicit operator Tensor<T>(Memory<T> memory) => memory.AsTensor();
        public static implicit operator Tensor<T>(T[,] array) => array.AsTensor();
        public static implicit operator Tensor<T>(T[,,] array) => array.AsTensor();
    }

}
