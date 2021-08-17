using System;

namespace Numeral
{
    public static partial class NDArrayExtensions
    {
        public static NDArray<T>[] Split<T>(this in NDArray<T> array, int count, int axis = 0)
        {
            if (axis < 0)
                axis = array.Rank + axis;

            if (axis < 0)
                throw new Exception("a negative value for axis was too large");

            if (axis > array.Rank - 1)
                throw new Exception("assert failed: axis > array.Rank - 1");

            var axisSize = array.Shape[axis];
            if ((axisSize % count) != 0)
                throw new Exception("assert failed: size % count != 0");

            // initialize slices to Range.All
            var slices = new Slice[axis + 1];
            for (var i = 0; i < slices.Length; i++)
                slices[i] = Range.All;

            // each sub array is constructed by adjusting
            // the slice of the axis
            var size = axisSize / count;
            var subArrays = new NDArray<T>[count];
            for (int i = 0; i < count; i++)
            {
                slices[axis] = (i * size)..((i + 1) * size);
                subArrays[i] = array.Slice(slices);
            }

            return subArrays;
        }
    }
}
