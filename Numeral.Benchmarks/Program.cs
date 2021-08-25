using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
// using Numeral.LinearAlgebra;

namespace Numeral.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            // var summary = BenchmarkRunner.Run<ArithmeticBenchmarks>();
            // var summary = BenchmarkRunner.Run<LinearAlgebraBenchmarks>();
        }
    }

    // public class LinearAlgebraBenchmarks
    // {
    //     private Tensor<float> MatA;
    //     private Tensor<float> MatB;
    //     private Tensor<float> VecA;
    //     public LinearAlgebraBenchmarks()
    //     {
    //         MatA = Tensor.Ones<float>(64, 64);
    //         MatB = Tensor.Ones<float>(64, 64).Transpose();
    //         VecA = Tensor.Ones<float>(64);
    //     }

    //     [Benchmark]
    //     public int Matmul()
    //     {
    //         MatA.Matmul(MatB);
    //         return 0;
    //     }

    //     [Benchmark]
    //     public int Vecmul()
    //     {
    //         MatA.Vecmul(VecA);
    //         return 0;
    //     }
    // }

    // public class ArithmeticBenchmarks
    // {
    //     private int size;
    //     private float[] x;
    //     private float[] y;

    //     public ArithmeticBenchmarks()
    //     {
    //         size = 4096;
    //         x = new float[size];
    //         y = new float[size];

    //         var rng = new System.Random();
    //         for (var i = 0; i < size; i++)
    //         {
    //             x[i] = (float)rng.NextDouble();
    //             y[i] = (float)rng.NextDouble();
    //         }
    //     }

    //     [Benchmark]
    //     public float[] Add()
    //     {
    //         var result = new float[size];
    //         for (var i = 0; i < size; i++)
    //             result[i] = x[i] + y[i];
    //         return result;
    //     }

    //     [Benchmark]
    //     public float[] AddVectorized()
    //     {
    //         var result = new float[size];
    //         UniversalFunctions.Add_float(x, y, result);
    //         return result;
    //     }

    //     [Benchmark]
    //     public float[] Multiply()
    //     {
    //         var result = new float[size];
    //         for (var i = 0; i < size; i++)
    //             result[i] = x[i] * y[i];
    //         return result;
    //     }

    //     [Benchmark]
    //     public float[] MultiplyVectorized()
    //     {
    //         var result = new float[size];
    //         UniversalFunctions.Multiply_float(x, y, result);
    //         return result;
    //     }
    // }
}
