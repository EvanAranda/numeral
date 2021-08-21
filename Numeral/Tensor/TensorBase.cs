using System;

namespace Numeral
{
    /// <summary>
    /// Base class for all tensors that defines readonly properties that every
    /// type of tensor has.
    /// </summary>
    public abstract partial class TensorBase
    {
        internal readonly ReadOnlyMemory<int> _shape;
        internal readonly ReadOnlyMemory<int> _strides;

        internal TensorBase(
            in ReadOnlyMemory<int> shape,
            in ReadOnlyMemory<int> strides,
            int count)
        {
            _shape = shape;
            _strides = strides;
            Count = count;
        }

        public ReadOnlySpan<int> Shape => _shape.Span;
        public ReadOnlySpan<int> Strides => _strides.Span;
        public int Rank => Shape.Length;
        public int Count { get; }
    }



}
