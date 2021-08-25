using System;
using Numeral.Internals;

namespace Numeral
{
    public class Scalar<T> : Tensor<T>
    {
        private static readonly int[] s_oneArray = new[] { 1 };
        internal T _value;

        public Scalar(T value) : base(s_oneArray, 1)
        {
            _value = value;
        }

        public override ref T this[params int[] indices] => throw new NotImplementedException();

        public override ref T this[params Index[] indices] => throw new NotImplementedException();

        public override ref T this[in ReadOnlySpan<int> indices] => throw new NotImplementedException();

        public override ref T this[in ReadOnlySpan<Index> indices] => throw new NotImplementedException();

        public override ITensorCore<T> Core => Core<T>.Instance;

        public override IFactory Factory => DenseTensorFactory.Instance;

        public override Tensor<R> Cast<R>()
        {
            if (typeof(T) == typeof(R))
                return this as Scalar<R>;

            return new Scalar<R>(DTypes.GetDType<R>().Cast(_value));
        }

        public override Tensor<T> Copy()
        {
            return new Scalar<T>(_value);
        }

        public override ref T GetFlat(int index)
        {
            if (index == 0)
                return ref _value;

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public override INDIterator<T> GetIterator(int operandRank = 1)
        {
            return IteratorExtensions.CreateIterator(this, Shape, operandRank);
        }

        public override INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, int operandRank = 1)
        {
            return IteratorExtensions.CreateIterator(this, shape, operandRank);
        }

        public override INDIterator<T> GetIterator(in ReadOnlySpan<int> shape, in ReadOnlySpan<int> order, int operandRank = 1)
        {
            return IteratorExtensions.CreateIterator(this, shape, operandRank);
        }

        public override Tensor<R> ReinterpretCast<R>()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Reshape(in Span<int> newShape)
        {
            if (newShape.Length == 1 && newShape[0] == 1)
                return this;

            throw new Exception("cannot reshape a scalar tensor");
        }

        public override Tensor<T> Slice(in ReadOnlySpan<Slice> slices)
        {
            throw new Exception("cannot slice a scalar tensor");
        }

        public override Tensor<T>[] Split(int segments, int axis = 0)
        {
            throw new Exception("cannot split a scalar tensor");
        }

        public override Tensor<T> Squeeze()
        {
            return this;
        }

        public override Tensor<T> Transpose()
        {
            return this;
        }

        public override Tensor<T> Transpose(in ReadOnlySpan<int> indices)
        {
            if (indices.Length == 1 && indices[0] == 0)
                return this;

            throw new Exception("cannot transpose the order of axis for a scalar tensor");
        }

        public static implicit operator Scalar<T>(T value) => new(value);
        public static implicit operator T(Scalar<T> tensor) => tensor._value;
    }
}
