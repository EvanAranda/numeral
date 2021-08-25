// using System.Linq;
// using System.Diagnostics;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Numeral.LinearAlgebra;

// namespace Numeral.Tests.LinearAlgebra
// {
//     [TestClass]
//     public class LinalgArrayFunctionsTests
//     {
//         [TestMethod]
//         public void VDot_1d()
//         {
//             var x = Tensor.Ones<float>(64);
//             var y = Tensor.Ones<float>(64);
//             var z = x.VDot(y);

//             Assert.AreEqual(64, z[0]);
//         }

//         [TestMethod]
//         public void VDot_nd()
//         {
//             var x = Tensor.Ones<float>(1000, 64);
//             var y = Tensor.Ones<float>(64);
//             var z = x.VDot(y);
//             Assert.IsTrue((z == 64.0f).All(b => b));
//         }

//         [TestMethod]
//         public void Matmul()
//         {
//             var x = Tensor.Ones<float>(64, 64);
//             var w = Tensor.Ones<float>(64, 64).Transpose();
//             var xw = x.Matmul(w);

//             Assert.AreEqual(64, xw[0, 0]);
//         }

//         [TestMethod]
//         public void Vecmul_broadcasted_matrix()
//         {
//             var x = Tensor.Ones<float>(12, 64, 64);
//             var y = Tensor.Ones<float>(64);
//             var z = x.Vecmul(y);
//         }

//         [TestMethod]
//         public void Vecmul_broadcasted_vector()
//         {
//             var x = Tensor.Ones<float>(64, 64);
//             var y = Tensor.Ones<float>(15, 64);
//             var z = x.Vecmul(y);
//         }

//         [TestMethod]
//         public void Vecmul_broadcasted_both()
//         {
//             var x = Tensor.Ones<float>(10, 64, 64);
//             var y = Tensor.Ones<float>(10, 64);
//             var z = x.Vecmul(y);
//         }

//         [TestMethod]
//         public void Vecmul()
//         {
//             var x = Tensor.Ones<float>(64, 64);
//             var y = Tensor.Ones<float>(64);
//             var z = x.Vecmul(y);
//         }
//     }
// }
