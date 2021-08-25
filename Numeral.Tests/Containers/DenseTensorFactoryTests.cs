using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Numeral.Internals;

namespace Numeral.Tests.Containers
{
    [Ignore]
    public abstract class TensorFactoryTests
    {
        protected IFactory _factory;

        protected abstract IFactory GetFactory();

        [TestInitialize]
        public void Setup()
        {
            _factory = GetFactory();
        }

        [TestMethod]
        public virtual void Zeros()
        {
            var x = _factory.Zeros<float>(10, 10);
            Assert.AreEqual(10, x.Shape[0]);
            Assert.AreEqual(10, x.Shape[1]);
        }

        [TestMethod]
        public virtual void ZerosLike()
        {
            var x = _factory.Zeros<float>(10, 10);
            var y = _factory.ZerosLike<float, float>(x);
            Assert.AreEqual(10, y.Shape[0]);
            Assert.AreEqual(10, y.Shape[1]);
        }

        [TestMethod]
        public virtual void Ones()
        {
            var x = _factory.Ones<float>(10, 10);

            Assert.AreEqual(10, x.Shape[0]);
            Assert.AreEqual(10, x.Shape[1]);
        }

        [TestMethod]
        public virtual void Eye()
        {
            var x = _factory.Ones<float>(10, 10);

            Assert.AreEqual(10, x.Shape[0]);
            Assert.AreEqual(10, x.Shape[1]);

            Span<int> indices = stackalloc int[2];
            for (var i = 0; i < x.Rank; i++)
            {
                indices.Fill(i);
                Assert.AreEqual(1, x[indices]);
            }
        }
    }


    [TestClass]
    public class DenseTensorFactoryTests : TensorFactoryTests
    {
        protected override IFactory GetFactory() => DenseTensorFactory.Instance;
    }

}