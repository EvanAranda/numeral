using System;
using System.Buffers;

namespace Numeral
{
    public readonly struct TemporaryTensor<T> : IDisposable
    {
        private readonly IMemoryOwner<T> _memoryOwner;
        private readonly Tensor<T> _tensor;

        public TemporaryTensor(IMemoryOwner<T> memoryOwner, Tensor<T> tensor)
        {
            _memoryOwner = memoryOwner;
            _tensor = tensor;
        }

        public Tensor<T> Tensor => _tensor;

        public void Dispose() => _memoryOwner.Dispose();
        public static implicit operator Tensor<T>(TemporaryTensor<T> temp) => temp.Tensor;
    }
}
