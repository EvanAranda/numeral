using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests
{
    [TestClass]
    public class NDArrayArithmeticTests
    {
        [TestMethod]
        public void Add()
        {
            NDArray<float> x = new float[] { 1, 2, 3 };
            var xx = x + x;

            var y = x.Sum(axis: 0);
            var yScalar = x.Sum();
            // x.Add(x, x);
            // var z = NDArrayOperatorBindings.Add(x, 1);
            // var z = x + 1.0f;
        }

        [TestMethod]
        public void Add_outer_broadcast()
        {
            var row = NDArray.Arange<int>(10).Reshape(1, -1);
            var col = NDArray.Arange<int>(10).Reshape(-1, 1);

            var mat = col + row;
        }

        [TestMethod]
        public void Sum()
        {
            var x = NDArray.Arange<int>(4).Reshape(2, 2);
            var y = x.Sum(axis: 0);
        }

    }
}
