// using System;
// using System.Runtime.CompilerServices;
// using System.Runtime.InteropServices;
// using System.Text;

// namespace Numeral.Internals
// {
//     [StructLayout(LayoutKind.Sequential)]
//     public unsafe struct StaticArray
//     {
//         private const int MaxSize = 8;
//         private fixed int _data[MaxSize];

//         internal StaticArray(int length)
//         {
//             if (length < 0)
//                 throw new ArgumentOutOfRangeException(nameof(length), "length < 0");

//             if (length > MaxSize)
//                 throw new ArgumentOutOfRangeException(nameof(length), "length > MaxSize");

//             Length = length;
//         }

//         public StaticArray(in ReadOnlySpan<int> values)
//             : this(values.Length)
//         {
//             for (var i = 0; i < Length; i++)
//                 _data[i] = values[i];
//             // fixed (int* dst = _data, src = values)
//             //     Unsafe.CopyBlock(dst, src, (uint)(Length * sizeof(int)));
//         }

//         public static StaticArray Constant(int x)
//         {
//             var arr = new StaticArray(1);
//             arr._data[0] = x;
//             return arr;
//         }

//         public static StaticArray Constant(int x0, int x1)
//         {
//             var arr = new StaticArray(2);
//             arr._data[0] = x0;
//             arr._data[1] = x1;
//             return arr;
//         }

//         public static StaticArray Constant(int x0, int x1, int x2)
//         {
//             var arr = new StaticArray(3);
//             arr._data[0] = x0;
//             arr._data[1] = x1;
//             arr._data[2] = x2;
//             return arr;
//         }

//         public int Length { get; }
//         public ref readonly int this[int index] => ref _data[index];

//         // public ReadOnlySpan<int> AsSpan()
//         // {
//         //     fixed (int* p = _data)
//         //         return new ReadOnlySpan<int>(p, Length);
//         // }

//         public override string ToString()
//         {
//             if (Length == 0)
//                 return string.Empty;

//             var sb = new StringBuilder(25);
//             for (var i = 0; i < Length - 1; i++)
//             {
//                 sb.Append(this[i]);
//                 sb.Append(',');
//             }

//             sb.Append(this[Length - 1]);

//             return sb.ToString();
//         }

//         // public static implicit operator ReadOnlySpan<int>(StaticArray array) => array.AsSpan();
//         public static implicit operator StaticArray(Span<int> values) => new(values);
//         public static implicit operator StaticArray(ReadOnlySpan<int> values) => new(values);
//     }

// }
