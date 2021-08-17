using System.Text;
using Microsoft.CodeAnalysis.Text;

namespace Numeral.CodeGeneration.Arithmetic
{
    public class UnaryMethod : OperatorMethod
    {
        public UnaryMethod(string name) : base(name) { }

        public override string GenerateOperatorOverloadCode()
        {
            if (!HasOperatorOverload)
                return string.Empty;

            return $@"
                public static NDArray<T> operator {Op}(in NDArray<T> x) => NDArrayOperatorBindings.{Name}(x);
            ";
        }

        /// <summary>
        /// Methods that on NDArrays that utilize ufuncs
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override string GenerateArrayFunctionCode(TypeConfig type)
        {
            var t = type.ClassName;
            var ufuncName = $"UniversalFunctions.{Name}_{t}";

            return $@"
                public static NDArray<{t}> {Name}(this in NDArray<{t}> x, in NDArray<{t}> result)
                    => Evaluate.ElementWiseUnaryOp<{t},{t}>(x, result, {ufuncName});

                public static NDArray<{t}> {Name}InPlace(this in NDArray<{t}> x)
                    => {Name}(x, x);

                public static NDArray<{t}> {Name}(this in NDArray<{t}> x)
                    => Evaluate.ElementWiseUnaryOp<{t},{t}>(x, {ufuncName});
            ";
        }

        public override string GenerateUniversalFunctionCode(TypeConfig type)
        {
            var code = new StringBuilder();
            var t = type.ClassName;

            code.AppendLine($@"
                public static unsafe void {Name}_{t}(in ReadOnlySpan<{t}> x, in Span<{t}> result)
                {{
                    var i = 0;");

            if (SupportsVectorization)
            {
                code.AppendLine($@"
                    var offset = Vector<{t}>.Count;
                    if (x.Length >= offset)
                    {{
                        var n = x.Length / offset * offset;
                        fixed ({t}* px = x, pr = result)
                        {{
                            for (; i < n; i += offset)
                                Unsafe.Write(pr + i, Vector.{VectorizedName}(Unsafe.Read<Vector<{t}>>(px + i)));
                        }}
                    }}");

            }

            var call = UseOpFunc ?
                $"({t}){OpFunc}((double)x[i])" :
                $"{Op}x[i]";

            code.AppendLine($@"
                    for (; i < x.Length; i++)
                        result[i] = {call};
                }}");

            return code.ToString();
        }
    }
}
