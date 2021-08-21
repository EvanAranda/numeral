using System;
using Numeral.Internals;

namespace Numeral
{
    public class Scalar<T> : Tensor<T>
    {
        internal T _value;

        public Scalar(T value) : base(Constants.OneArray, Constants.OneArray, 1)
        {
            _value = value;
        }

        public override ref T this[params int[] indices] => throw new NotImplementedException();

        public override ref T this[params Index[] indices] => throw new NotImplementedException();

        public override ref T this[in ReadOnlySpan<int> indices] => throw new NotImplementedException();

        public override ref T this[in ReadOnlySpan<Index> indices] => throw new NotImplementedException();

        public override ITensorCore<T> Core => Core<T>.Instance;

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

        public override Tensor<R> ReinterpretCast<R>()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Reshape(params int[] newShape)
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Slice(params Slice[] slices)
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

        public override Tensor<T> Transpose(params int[] indices)
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

        public static implicit operator Scalar<T>(T value) => new Scalar<T>(value);
        public static implicit operator T(Scalar<T> tensor) => tensor._value;
    }
}
