using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using Numeral.Internals;

namespace Numeral
{
    /// <summary>
    /// Base class for all tensors that defines readonly properties that every
    /// type of tensor has.
    /// </summary>
    public abstract partial class TensorBase
    {
        internal readonly ReadOnlyMemory<int> _shape;

        internal TensorBase(
            in ReadOnlyMemory<int> shape,
            int count)
        {
            _shape = shape;
            Count = count;
        }

        public ReadOnlySpan<int> Shape => _shape.Span;
        public int Rank => _shape.Length;
        public int Count { get; }
    }

}
