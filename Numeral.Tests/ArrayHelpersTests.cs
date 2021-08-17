using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests
{
    [TestClass]
    public class ArrayHelpersTests
    {

        [TestMethod]
        public void ProductSum()
        {
            var psum = ArrayHelpers.GetProductSum(new int[] { 1, 2, 3 });
            Assert.AreEqual(6, psum);
        }


    }
}
