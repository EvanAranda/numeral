using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Numeral.Iterators;

namespace Numeral.Tests
{
    [TestClass]
    public class IndexHelpersTests
    {
        [TestMethod]
        public void GetIndices()
        {
            Span<int> shape = stackalloc int[] { 3, 2 };
            Span<int> strides = stackalloc int[2];
            IteratorHelpers.GetStrides(shape, strides);
            var cases = new (int, int[])[]
            {
                (0, new[] {0,0}),
                (1, new[] {0,1}),
                (2, new[] {1,0}),
                (5, new[] {2,1}),
            };

            Span<int> indices = stackalloc int[2];
            foreach (var (index, expected) in cases)
            {
                IndexHelpers.GetIndices(strides, index, indices);
                Assert.IsTrue(indices.SequenceEqual(expected));
            }
        }
    }
}
