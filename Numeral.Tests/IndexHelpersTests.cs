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
            var shape = new int[] { 3, 2 };
            var strides = IteratorHelpers.GetStrides(shape);
            var cases = new[]
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
