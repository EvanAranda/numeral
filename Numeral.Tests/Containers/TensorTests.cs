using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numeral.Tests.Containers
{
    [Ignore]
    /// <summary>
    /// Base class that tests implementations of methods on <see cref="Tensor{T}"/>
    /// </summary>
    public abstract class TensorTests<T>
    {
        protected static IFactory _factory;

        [DataTestMethod]
        [DynamicData(nameof(GetTensors), DynamicDataSourceType.Method)]
        public void GetIterator(Tensor<T> tensor)
        {
            using var iter = tensor.GetIterator();

            while (iter.MoveNext()) { }
        }

        public static IEnumerable<object[]> GetTensors()
        {
            yield return new object[] { _factory.Zeros<T>(10, 10) };
        }
    }

    [TestClass]
    public class DenseTensorTests : TensorTests<float>
    {
        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            _factory = DenseTensorFactory.Instance;
        }

        [TestMethod]
        public void Get_with_tensor_index()
        {
            var (x, y) = Tensor.Linspace<float>(0, 1, 10).AsGrid();
            var z = x.Sin() + y.Sin();

            z.Get(new[] { 0, 1 });
        }
    }
}
