using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests
{
    [TestClass]
    public class NDArrayConstructorsTests
    {
        [TestMethod]
        public void Linspace()
        {
            var x = NDArray.Linspace<double>(-1, 1, 10);

            Assert.AreEqual(10, x.Count);
            Assert.AreEqual(1, x.Rank);
        }

    }
}
