using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Numeral.LinearAlgebra;

namespace Numeral.Tests.LinearAlgebra
{
    [TestClass]
    public class LinalgArrayFunctionsTests
    {
        [TestMethod]
        public void VDot_1d()
        {
            var x = NDArray.Ones<float>(64);
            var y = NDArray.Ones<float>(64);
            var z = x.VDot(y);
        }

        [TestMethod]
        public void VDot_nd()
        {
            var x = NDArray.Ones<float>(1000, 64);
            var y = NDArray.Ones<float>(64);
            var z = x.VDot(y);

            x.Add(y);
        }

        [TestMethod]
        public void Matmul()
        {
            var x = NDArray.Ones<float>(64, 64);
            var w = NDArray.Ones<float>(64, 64).Transpose();

            var sw = Stopwatch.StartNew();
            var xw = x.Matmul(w);//.SinInPlace();
        }

        [TestMethod]
        public void Vecmul_broadcasted_matrix()
        {
            var x = NDArray.Ones<float>(10, 64, 64);
            var y = NDArray.Ones<float>(64);
            var z = x.Vecmul(y);
        }

        [TestMethod]
        public void Vecmul_broadcasted_vector()
        {
            var x = NDArray.Ones<float>(64, 64);
            var y = NDArray.Ones<float>(10, 64);
            var z = x.Vecmul(y);
        }

        [TestMethod]
        public void Vecmul_broadcasted_both()
        {
            var x = NDArray.Ones<float>(10, 64, 64);
            var y = NDArray.Ones<float>(10, 64);
            var z = x.Vecmul(y);
        }

        [TestMethod]
        public void Vecmul()
        {
            var x = NDArray.Ones<float>(64, 64);
            var y = NDArray.Ones<float>(64);
            var z = x.Vecmul(y);
        }
    }
}
