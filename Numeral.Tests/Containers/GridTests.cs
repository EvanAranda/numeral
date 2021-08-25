using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests.Containers
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void LinSpace()
        {
            var line = Tensor.LinSpace<double>(0, 1, 1000);
        }


        [TestMethod]
        public void GridSpace()
        {
            var grid = Tensor.GridSpace<double>(
                new double[] { 0, 0 },
                new double[] { 1, 1 },
                100);
        }

    }
}
