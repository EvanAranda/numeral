using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests
{
    [TestClass]
    public class ArrayHelpersTests
    {
        [TestMethod]
        public void GetProductSum()
        {
            var psum = ArrayHelpers.GetProductSum(new int[] { 1, 2, 3 });
            Assert.AreEqual(6, psum);
        }
    }
}
