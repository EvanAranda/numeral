using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Numeral.Iterators;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        public static string Display<T>(
            this in NDArray<T> array,
            int fieldWidth = 8,
            string format = default,
            int limit = -1)
        {
            var sb = new StringBuilder();

            var rank = array.Rank;
            Span<int> indices = stackalloc int[rank];
            var strides = array.Strides;
            var shape = array.Shape;

            var index = -strides[^1];
            indices[^1] = index;

            Span<int> backstrides = stackalloc int[rank];
            IteratorHelpers.GetBackstrides(shape, strides, backstrides);

            Span<T> span = array;

            var looping = true;

            format ??= GetDefaultFormat<T>();
            var formatBase = $"{{0,{fieldWidth}:{format}}}";
            var formatSpaced = $", {formatBase}";

            // first step of each loop leading up to inner loop
            for (var i = 0; i < rank; i++)
                sb.Append('[');

            while (looping)
            {
                // i : loop counter, starts at inner most loop
                for (var i = rank - 1; i >= 0; i--)
                {
                    // check if output is being limited
                    if (limit != -1)
                    {
                        if (limit - 1 <= indices[i] && indices[i] <= shape[i] - 1 - limit)
                        {
                            var newLoc = shape[i] - 1 - limit;
                            index += strides[i] * (newLoc - indices[i]);
                            indices[i] = newLoc;

                            if (i == array.Rank - 1)
                            {
                                // add ellipsis and jump to other end
                                sb.Append($", \u22EF ");
                            }
                            else
                            {
                                // vertical ellipsis
                                sb.AppendLine(",");
                                sb.Append(" \u22EE");
                                // sb.AppendLine($" n - rows");
                                // sb.Append(" \u22EE");
                            }
                        }

                    }

                    // if not inner loop and not completed
                    if (i != rank - 1 && indices[i] < shape[i] - 1)
                    {
                        sb.AppendLine(",");

                        for (var j = i + 1; j > 0; j--)
                            sb.Append(' ');

                        for (var j = i; j < rank - 1; j++)
                            sb.Append('[');
                    }

                    // check if loop_i is done
                    if (indices[i] == shape[i] - 1)
                    {
                        sb.Append(']');

                        if (i == 0)
                        {
                            looping = false;
                            break;
                        }

                        // reset loop_i
                        indices[i] = 0;
                        index -= backstrides[i];

                        // move to next outer loop
                        continue;
                    }

                    // advance loop_i and data pointer
                    index += strides[i];
                    indices[i]++;

                    // write element
                    if (indices[^1] == 0)
                        sb.AppendFormat(formatBase, span[index]);
                    else
                        sb.AppendFormat(formatSpaced, span[index]);

                    break;
                }
            }

            return sb.ToString();
        }

        private static string GetDefaultFormat<T>()
        {
            if (typeof(T) == typeof(int) || typeof(T) == typeof(byte))
                return "F0";

            return "F3";
        }
    }
}
