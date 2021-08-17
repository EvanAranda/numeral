using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests
{
    [TestClass]
    public class NDArrayExtensionsTests
    {
        [TestMethod]
        public void Reshape()
        {
            var arr = NDArray.Arange<float>(9).Reshape(3, 3);
            Assert.IsTrue(arr.Shape.SequenceEqual(new[] { 3, 3 }));
        }

        [TestMethod]
        public void Reshape_array_view()
        {
            var arr = NDArray.Arange<float>(56).Reshape(8, 7);
            var view = arr.Slice(2..6, 1..6);
            var viewReshaped = view.Reshape(-1, 2);
        }

        [TestMethod]
        public void Transpose_reverses_shape_and_strides()
        {
            var arr = NDArray.Ones<float>(10, 5);
            arr = arr.Transpose();

            Assert.AreEqual(5, arr.Shape[0]);
            Assert.AreEqual(10, arr.Shape[1]);
        }

        [TestMethod]
        public void Tranpose_1d_unaffected()
        {
            var arr = NDArray.Ones<float>(10);
            arr = arr.Transpose();

            Assert.AreEqual(1, arr.Rank);
            Assert.AreEqual(10, arr.Shape[0]);
        }

        [TestMethod]
        public void Tranpose_reorder_axes()
        {
            var arr = NDArray.Arange<float>(60).Reshape(5, 4, 3);
            var tranposed = arr.Transpose(1, 2, 0);

        }

        [TestMethod]
        public void Copy()
        {
            var arr = NDArray.Arange<float>(4).Reshape(2, 2);
            var copy = arr.Transpose().Copy();
        }

        [TestMethod]
        public void Select()
        {
            var x = NDArray.Arange<int>(9).Reshape(3, 3);
            var selected = x.Select(new int[] { 4 });
            selected = x.Select(new int[,] { { 2, 2 } });
        }

        [TestMethod]
        public void Assign()
        {
            var x = NDArray.Arange<int>(9).Reshape(3, 3);
            x.AssignInPlace(x > 4, -1);
        }

        [TestMethod]
        public void Accum()
        {
            var x = NDArray.Arange<int>(4).Reshape(2, 2).AddInPlace(1);
            var accumulated = x.Accum(0, (_x, _acc) => _x * _acc);
        }

        [TestMethod]
        public void Split_1d()
        {
            var x = NDArray.Arange<int>(4);
            var splits = x.Split(2);
        }

        [TestMethod]
        public void Split_2d()
        {
            var x = NDArray.Arange<int>(4).Reshape(2, 2);
            var splits = x.Split(2, axis: -1);
            var split0 = splits[0];
            var split1 = splits[1];
        }

    }
}
