using System;
using Numeral.Internals;

namespace Numeral
{

    public class SparseTensor<T> : Tensor<T>
    {
        internal SparseTensor(in ReadOnlyMemory<int> shape, int count) : base(shape, count)
        {
        }

        public override ref T this[params int[] indices] => throw new NotImplementedException();

        public override ref T this[params Index[] indices] => throw new NotImplementedException();

        public override ref T this[in ReadOnlySpan<int> indices] => throw new NotImplementedException();

        public override ref T this[in ReadOnlySpan<Index> indices] => throw new NotImplementedException();

        public override ITensorCore<T> Core => throw new NotImplementedException();

        public override IFactory Factory => throw new NotImplementedException();

        public override Tensor<R> Cast<R>()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Copy()
        {
            throw new NotImplementedException();
        }

        public override ref T GetFlat(int index)
        {
            throw new NotImplementedException();
        }

        public override INDIterator<T> GetIterator(int operandRank = 1)
        {
            throw new NotImplementedException();
        }

        public override INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, int operandRank = 1)
        {
            throw new NotImplementedException();
        }

        public override INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, in ReadOnlySpan<int> order, int operandRank = 1)
        {
            throw new NotImplementedException();
        }

        public override Tensor<R> ReinterpretCast<R>()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Reshape(in Span<int> newShape)
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Slice(in ReadOnlySpan<Slice> slices)
        {
            throw new NotImplementedException();
        }

        public override Tensor<T>[] Split(int segments, int axis = 0)
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Squeeze()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Transpose()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Transpose(in ReadOnlySpan<int> indices)
        {
            throw new NotImplementedException();
        }
    }
}
