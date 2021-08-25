//using System;
//using Numeral.Iterators;

//namespace Numeral.LinearAlgebra
//{
//    public static partial class LinalgArrayFunctions
//    {
//        /// <summary>
//        /// 1-d dot product between (flattened) x and y arrays
//        /// </summary>
//        /// <param name="x"></param>
//        /// <param name="y"></param>
//        /// <returns></returns>
//        public static float Dot(this in Tensor<float> x, in Tensor<float> y)
//        {
//            Span<float> result = stackalloc float[1];
//            LinalgUniversalFunctions.InnerProduct_float(x, y, result);
//            return result[0];
//        }

//        /// <summary>
//        /// Broadcasted dot product between arrays x and y. Broadcasting kicks
//        /// in if either are > rank-2.
//        /// </summary>
//        /// <param name="x"></param>
//        /// <param name="y"></param>
//        /// <returns></returns>
//        public static Tensor<float> VDot(this in Tensor<float> x, in Tensor<float> y)
//        {
//            var loopsRank = Math.Max(x.Rank, y.Rank);
//            Span<int> loopsShape = stackalloc int[loopsRank];

//            IteratorHelpers.UpdateResultShape(loopsShape, x.Shape);
//            IteratorHelpers.UpdateResultShape(loopsShape, y.Shape);

//            var xi = x.GetIterator(loopsShape, 1);
//            var yi = y.GetIterator(loopsShape, 1);

//            loopsShape[^1] = 1;
//            var result = Tensor.Empty<float>(loopsShape.ToArray());

//            var ri = result.GetIterator(loopsShape, 0);

//            if (loopsRank > 1)
//                Evaluate.RunP(xi, yi, ri, LinalgUniversalFunctions.InnerProduct_float);
//            else
//                Evaluate.Run(xi, yi, ri, LinalgUniversalFunctions.InnerProduct_float);

//            return result;
//        }

//        /// <summary>
//        /// Matrix * vector
//        /// </summary>
//        /// <param name="x"></param>
//        /// <param name="y"></param>
//        /// <returns></returns>
//        public static unsafe Tensor<float> Vecmul(this in Tensor<float> x, in Tensor<float> y)
//        {
//            var loopsRank = Math.Max(x.Rank, y.Rank + 1);
//            Span<int> loopsShape = stackalloc int[loopsRank];

//            Span<int> yorder = stackalloc int[] { y.Rank - 2, -1, y.Rank - 1 };

//            IteratorHelpers.UpdateResultShape(loopsShape, x.Shape);
//            IteratorHelpers.UpdateResultShape(loopsShape, y.Shape, yorder);

//            var xi = x.GetIterator(loopsShape, 1);
//            var yi = y.GetIterator(loopsShape, yorder, 1);

//            var resultShape = loopsShape[..^1];
//            var result = Tensor.Empty<float>(resultShape.ToArray());
//            var ri = result.GetIterator(resultShape, 0);

//            if (xi.FullShape[0] >= Environment.ProcessorCount)
//                Evaluate.RunP(xi, yi, ri, LinalgUniversalFunctions.InnerProduct_float);
//            else
//                Evaluate.Run(xi, yi, ri, LinalgUniversalFunctions.InnerProduct_float);

//            return result;
//        }

//        public static unsafe Tensor<float> Matmul(this in Tensor<float> x, in Tensor<float> y)
//        {
//            var loopsRank = Math.Max(x.Rank, y.Rank) + 1;
//            Span<int> loopsShape = stackalloc int[loopsRank];

//            // The 2nd and 3rd inner-most loops are transposed
//            // to reduce the number of copies of y columns, since y is
//            // likely to by row-contiguous and not column-contiguous.
//            // Without this change, each iteration of the loop would
//            // switch to a different y column and require filling a buffer.
//            Span<int> xorder = stackalloc int[] { -1, x.Rank - 2, x.Rank - 1 };
//            Span<int> yorder = stackalloc int[] { y.Rank - 1, -1, y.Rank - 2 };
//            Span<int> rorder = stackalloc int[] { 1, 0 };

//            IteratorHelpers.UpdateResultShape(loopsShape, x.Shape, xorder);
//            IteratorHelpers.UpdateResultShape(loopsShape, y.Shape, yorder);

//            var xi = x.GetIterator(loopsShape, xorder, 1);
//            var yi = y.GetIterator(loopsShape, yorder, 1);

//            var resultShape = loopsShape[..^1];
//            resultShape.Reverse();
//            var result = Tensor.Empty<float>(resultShape.ToArray());
//            var ri = result.GetIterator(loopsShape[..^1], rorder, 0);

//            if (xi.FullShape[0] >= Environment.ProcessorCount)
//                Evaluate.RunP(xi, yi, ri, LinalgUniversalFunctions.InnerProduct_float);
//            else
//                Evaluate.Run(xi, yi, ri, LinalgUniversalFunctions.InnerProduct_float);

//            return result;
//        }

//    }
//}
