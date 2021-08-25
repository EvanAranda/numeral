
using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;

using Numeral.Internals;

namespace Numeral
{
    public static partial class Tensor
    {
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0)
        {
            x0 = tensor.GetFlat(0);
        }

        public static void Deconstruct<T>(this T[] array, out T x0)
        {
            x0 = array[0];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1)
        {
            x0 = array[0];
            x1 = array[1];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
            x10 = tensor.GetFlat(10);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
            x10 = array[10];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
            x10 = tensor.GetFlat(10);
            x11 = tensor.GetFlat(11);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
            x10 = array[10];
            x11 = array[11];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
            x10 = tensor.GetFlat(10);
            x11 = tensor.GetFlat(11);
            x12 = tensor.GetFlat(12);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
            x10 = array[10];
            x11 = array[11];
            x12 = array[12];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12, out T x13)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
            x10 = tensor.GetFlat(10);
            x11 = tensor.GetFlat(11);
            x12 = tensor.GetFlat(12);
            x13 = tensor.GetFlat(13);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12, out T x13)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
            x10 = array[10];
            x11 = array[11];
            x12 = array[12];
            x13 = array[13];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12, out T x13, out T x14)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
            x10 = tensor.GetFlat(10);
            x11 = tensor.GetFlat(11);
            x12 = tensor.GetFlat(12);
            x13 = tensor.GetFlat(13);
            x14 = tensor.GetFlat(14);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12, out T x13, out T x14)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
            x10 = array[10];
            x11 = array[11];
            x12 = array[12];
            x13 = array[13];
            x14 = array[14];
        }
        public static void Deconstruct<T>(this Tensor<T> tensor, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12, out T x13, out T x14, out T x15)
        {
            x0 = tensor.GetFlat(0);
            x1 = tensor.GetFlat(1);
            x2 = tensor.GetFlat(2);
            x3 = tensor.GetFlat(3);
            x4 = tensor.GetFlat(4);
            x5 = tensor.GetFlat(5);
            x6 = tensor.GetFlat(6);
            x7 = tensor.GetFlat(7);
            x8 = tensor.GetFlat(8);
            x9 = tensor.GetFlat(9);
            x10 = tensor.GetFlat(10);
            x11 = tensor.GetFlat(11);
            x12 = tensor.GetFlat(12);
            x13 = tensor.GetFlat(13);
            x14 = tensor.GetFlat(14);
            x15 = tensor.GetFlat(15);
        }

        public static void Deconstruct<T>(this T[] array, out T x0, out T x1, out T x2, out T x3, out T x4, out T x5, out T x6, out T x7, out T x8, out T x9, out T x10, out T x11, out T x12, out T x13, out T x14, out T x15)
        {
            x0 = array[0];
            x1 = array[1];
            x2 = array[2];
            x3 = array[3];
            x4 = array[4];
            x5 = array[5];
            x6 = array[6];
            x7 = array[7];
            x8 = array[8];
            x9 = array[9];
            x10 = array[10];
            x11 = array[11];
            x12 = array[12];
            x13 = array[13];
            x14 = array[14];
            x15 = array[15];
        }
    }
}