using System;

namespace Numeral
{
    public class SparseSymmetricTensor<T> : SparseTensor<T>
    {
        internal SparseSymmetricTensor(in ReadOnlyMemory<int> shape, int count) : base(shape, count)
        {
        }
    }
}
