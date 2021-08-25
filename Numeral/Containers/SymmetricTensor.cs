using System;

namespace Numeral
{
    public class SymmetricTensor<T> : DenseTensor<T>
    {
        internal SymmetricTensor(in Memory<T> data, in ReadOnlyMemory<int> shape) : base(data, shape)
        {
        }
    }
}
