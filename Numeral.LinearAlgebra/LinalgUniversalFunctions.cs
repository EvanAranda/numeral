using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Numeral.LinearAlgebra
{

    public static partial class LinalgUniversalFunctions
    {
        public static unsafe void InnerProduct_float(in ReadOnlySpan<float> x, in ReadOnlySpan<float> y, in Span<float> result)
        {
            float tmp = 0;
            var i = 0;

            var offset = Vector<float>.Count;
            if (x.Length >= offset)
            {
                var n = x.Length / offset * offset;
                fixed (float* px = x, py = y)
                {
                    for (; i < n; i += offset)
                    {
                        tmp += Vector.Dot(
                            Unsafe.Read<Vector<float>>(px + i),
                            Unsafe.Read<Vector<float>>(py + i));
                    }
                }
            }

            for (; i < x.Length; i++)
                tmp += x[i] * y[i];

            result[0] = tmp;
        }
    }
}
