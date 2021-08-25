using System;
using System.Threading.Tasks;
using Numeral.Internals;
using Numeral.Iterators;

namespace Numeral
{
    public class Grid<T> : Tensor<T>
        where T : unmanaged
    {
        private readonly ReadOnlyMemory<T> _min;
        private readonly ReadOnlyMemory<T> _max;
        private readonly ReadOnlyMemory<T> _steps;

        internal Grid(
            in ReadOnlyMemory<T> min,
            in ReadOnlyMemory<T> max,
            in ReadOnlyMemory<int> shape,
            int count)
            : base(shape, count)
        {
            _min = min;
            _max = max;

            var steps = new T[Rank];
            Span<T> shape_t = stackalloc T[Rank];
            Cast<int, T>.Op(shape.Span, shape_t);

            Span<T> ones = stackalloc T[Rank];
            ones.Fill(DTypes.GetDType<T>().One);

            s_sub(shape_t, ones, shape_t);
            s_sub(Max, Min, steps);
            s_div(steps, shape_t, steps);

            _steps = steps;
        }

        private static readonly BinaryOp<T, T, T> s_add = (CpuArithmetic<T>.Instance.Add as ICpuBinaryOperation<T, T, T>).Op;
        private static readonly BinaryOp<T, T, T> s_sub = (CpuArithmetic<T>.Instance.Subtract as ICpuBinaryOperation<T, T, T>).Op;
        private static readonly BinaryOp<T, T, T> s_mul = (CpuArithmetic<T>.Instance.Multiply as ICpuBinaryOperation<T, T, T>).Op;
        private static readonly BinaryOp<T, T, T> s_div = (CpuArithmetic<T>.Instance.Divide as ICpuBinaryOperation<T, T, T>).Op;

        public ReadOnlySpan<T> Min => _min.Span;

        public ReadOnlySpan<T> Max => _max.Span;

        public ReadOnlySpan<T> Steps => _steps.Span;

        public override ITensorCore<T> Core => Core<T>.Instance;

        public override IFactory Factory => DenseTensorFactory.Instance;

        public override Tensor<R> Cast<R>()
        {
            throw new NotImplementedException();
        }

        public override Tensor<T> Copy()
        {
            // this is already readonly
            return this;
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

        public override Tensor<T> Slice(in ReadOnlySpan<Slice> slices)
        {
            throw new NotImplementedException();
        }

        public override Tensor<T>[] Split(int segments, int axis = 0)
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

        #region NotSupported

        public override ref T this[params int[] indices] => throw new NotSupportedException();

        // private ref T GetElement(int[] indices)
        // {
        //     Span<T> p = stackalloc T[Rank];
        //     Cast<int, T>.Op(indices, p);

        //     s_mul(p, _steps.Span, p);
        //     s_add(p, Min, p);
        // }

        public override ref T this[params Index[] indices] => throw new NotSupportedException();

        public override ref T this[in ReadOnlySpan<int> indices] => throw new NotSupportedException();

        public override ref T this[in ReadOnlySpan<Index> indices] => throw new NotSupportedException();

        public override Tensor<T> Squeeze()
        {
            throw new NotSupportedException();
        }

        public override ref T GetFlat(int index)
        {
            throw new NotSupportedException();
        }

        public override Tensor<R> ReinterpretCast<R>()
        {
            throw new NotSupportedException();
        }

        public override Tensor<T> Reshape(in Span<int> newShape)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    // internal struct GridPointsGenerator<T> : INDIterator<T>
    //     where T : unmanaged
    // {
    //     private readonly Grid<T> _grid;
    //     private 
    //     private readonly Memory<int> _

    //     public IteratorFlags Flags => IteratorFlags.Contiguous;

    //     public GridPointsGenerator(Grid<T> grid)
    //     {
    //         _grid = grid;
    //     }

    //     public void Dispose()
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public Span<T> GetData()
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public bool MoveNext()
    //     {
    //         throw new NotImplementedException();
    //     }

    //     public void PutData(in ReadOnlySpan<T> data)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}
