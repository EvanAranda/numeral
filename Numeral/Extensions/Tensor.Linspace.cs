using System;
using System.Diagnostics;
using System.Linq;

namespace Numeral
{
    public static partial class Tensor
    {
        public static Tensor<T>[] Orthogonalize<T>(params Tensor<T>[] axes)
        {
            Span<int> newShape = stackalloc int[axes.Length];

            for (var i = 0; i < newShape.Length; i++)
            {
                newShape.Fill(1);
                newShape[i] = axes[i].Count;
                axes[i] = axes[i].Reshape(newShape);
            }

            return axes;
        }

        public static Tensor<T>[] AsGrid<T>(this Tensor<T> axis, int rank = 2)
        {
            if (rank < 1)
                throw new ArgumentOutOfRangeException(nameof(rank));

            var axes = new Tensor<T>[rank];
            for (var i = 0; i < rank; i++)
                axes[i] = axis;

            return Orthogonalize(axes);
        }

        public static Tensor<T> Linspace<T>(T start, T end, int numPoints = 2)
            where T : unmanaged
        {
            if (numPoints < 2)
                throw new ArgumentOutOfRangeException(nameof(numPoints), "numPoints < 2");

            var values = new T[numPoints];

            if (typeof(T) == typeof(float))
                Fill_float((float)(object)start, (float)(object)end, numPoints, values as float[]);
            else if (typeof(T) == typeof(double))
                Fill_double((double)(object)start, (double)(object)end, numPoints, values as double[]);
            else
                throw new NotSupportedException($"type arg {typeof(T)} is not supported");

            return values;
        }

        private static void Fill_float(float start, float end, int numPoints, Span<float> values)
        {
            Debug.Assert(numPoints == values.Length);

            values[0] = start;
            values[^1] = end;

            var dx = (end - start) / (numPoints - 1);
            for (var i = 1; i < numPoints - 1; i++)
                values[i] = start + dx * i;
        }

        private static void Fill_double(double start, double end, int numPoints, Span<double> values)
        {
            Debug.Assert(numPoints == values.Length);

            values[0] = start;
            values[^1] = end;

            var dx = (end - start) / (numPoints - 1);
            for (var i = 1; i < numPoints - 1; i++)
                values[i] = start + dx * i;
        }
    }
}