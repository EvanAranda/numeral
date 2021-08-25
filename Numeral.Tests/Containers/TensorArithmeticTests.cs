using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Numeral.Internals;

namespace Numeral.Tests.Containers
{
    [Ignore]
    public abstract class TensorArithmeticTests<T>
    {
        protected static ITensorArithmetic<T> _tensorArithmetic;

        [DataTestMethod]
        [DynamicData(nameof(BinaryOperationCases), DynamicDataSourceType.Method)]
        public void TestBinaryOperation(IBinaryOperation<T, T, T> op, Tensor<T> x, Tensor<T> y)
        {
            var result = op.AllocateResult(x, y);
            op.Call(x, y, result);
        }

        private static IEnumerable<object[]> BinaryOperationCases()
        {
            var x = DenseTensorFactory.Instance.Zeros<T>(10, 10);
            var y = DenseTensorFactory.Instance.Zeros<T>(10, 10);
            yield return new object[] { _tensorArithmetic.Add, x, y };
        }
    }


    [TestClass]
    public class CpuArithmeticFloatTests : TensorArithmeticTests<float>
    {
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _tensorArithmetic = CpuArithmetic<float>.Instance;
        }
    }
}
