using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Numeral.CodeGeneration.Arithmetic
{

    public class BinaryMethod : OperatorMethod
    {

        public BinaryMethod(string name) : base(name) { }

        public bool IsComparison { get; set; }

        public override string GenerateOperatorOverloadCode()
        {
            if (!HasOperatorOverload)
                return string.Empty;

            var r = IsComparison ? "bool" : "T";

            return $@"
                public static NDArray<{r}> operator {Op}(in NDArray<T> x, in NDArray<T> y) => NDArrayOperatorBindings.{Name}(x, y);
                public static NDArray<{r}> operator {Op}(in NDArray<T> x, T y) => NDArrayOperatorBindings.{Name}(x, y);
                public static NDArray<{r}> operator {Op}(T x, in NDArray<T> y) => NDArrayOperatorBindings.{Name}(x, y);
            ";
        }

        /// <summary>
        /// Methods that on NDArrays that utilize ufuncs
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override string GenerateArrayFunctionCode(TypeConfig type)
        {
            var T = type.ClassName;
            var R = IsComparison ? "bool" : T;
            var ufuncName = $"UniversalFunctions.{Name}_{T}";

            var code = new StringBuilder();

            code.AppendLine($@"
                public static NDArray<{R}> {Name}(this in NDArray<{T}> x, in NDArray<{T}> y, in NDArray<{R}> result)
                    => Evaluate.ElementWiseBinaryOp<{T},{T},{R}>(x, y, result, {ufuncName});
                    
                public static NDArray<{R}> {Name}(this in NDArray<{T}> x, in NDArray<{T}> y)
                    => Evaluate.ElementWiseBinaryOp<{T},{T},{R}>(x, y, {ufuncName});

                public static NDArray<{R}> {Name}(this in NDArray<{T}> x, {T} y, in NDArray<{R}> result)
                    => Evaluate.ElementWiseBinaryOp<{T},{T},{R}>(x, y, result, {ufuncName});

                public static NDArray<{R}> {Name}(this in NDArray<{T}> x, {T} y)
                    => Evaluate.ElementWiseBinaryOp<{T},{T},{R}>(x, y, {ufuncName});

                public static NDArray<{R}> {Name}({T} x, in NDArray<{T}> y, in NDArray<{R}> result)
                    => Evaluate.ElementWiseBinaryOp<{T},{T},{R}>(x, y, result, {ufuncName});

                public static NDArray<{R}> {Name}({T} x, in NDArray<{T}> y)
                    => Evaluate.ElementWiseBinaryOp<{T},{T},{R}>(x, y, {ufuncName});
            ");

            if (!IsComparison)
                code.AppendLine($@"
                    public static NDArray<{R}> {Name}InPlace(this in NDArray<{T}> x, in NDArray<{T}> y)
                        => {Name}(x, y, x);
                    public static NDArray<{R}> {Name}InPlace(this in NDArray<{T}> x, {T} y)
                        => {Name}(x, y, x);
                ");

            return code.ToString();
        }

        protected virtual string GetVectorizedOp(string x, string y)
        {
            if (IsComparison)
            {
                return $@"
                    var t = Vector.{VectorizedName}({x}, {y});
                    
                    for (int j = 0; j < offset; j++)
                        Unsafe.Write(pr + i + j, t[j] == -1);";
            }

            return $@"
                Unsafe.Write(pr + i, Vector.{VectorizedName}({x}, {y}));";
        }

        protected virtual string GetOp(string x, string y, TypeConfig type)
        {
            if (UseOpFunc)
            {
                return $"({type.ClassName}){OpFunc}({x}, {y})";
            }

            return $"{x} {Op} {y}";
        }

        public override string GenerateUniversalFunctionCode(TypeConfig type)
        {
            var code = new StringBuilder();

            var t = type.ClassName;
            var r = IsComparison ? "bool" : t;
            var name = $"{Name}_{t}";

            // array, array
            code.AppendLine($@"
                public static unsafe void {name}(in ReadOnlySpan<{t}> x, in ReadOnlySpan<{t}> y, in Span<{r}> result)
                {{
                    var i = 0;");

            if (SupportsVectorization)
            {
                code.AppendLine($@"
                    var offset = Vector<{t}>.Count;
                    if (x.Length >= offset)
                    {{
                        var n = x.Length / offset * offset;
                        fixed ({r}* pr = result)
                        fixed ({t}* px = x, py = y)
                        {{
                            for (; i < n; i += offset)
                            {{
                                {GetVectorizedOp(
                                    $"Unsafe.Read<Vector<{t}>>(px + i)",
                                    $"Unsafe.Read<Vector<{t}>>(py + i)")}
                            }}
                        }}
                    }}");
            }

            code.AppendLine($@"
                    for (; i < x.Length; i++)
                        result[i] = {GetOp("x[i]", "y[i]", type)};
                }}");

            // array, scalar
            code.AppendLine($@"
                public static unsafe void {name}(in ReadOnlySpan<{t}> x, {t} y, in Span<{r}> result)
                {{
                    var i = 0;");

            if (SupportsVectorization)
            {
                code.AppendLine($@"
                    var offset = Vector<{t}>.Count;
                    if (x.Length >= offset)
                    {{
                        var n = x.Length / offset * offset;
                        var vy = new Vector<{t}>(y);
                        fixed ({r}* pr = result)
                        fixed ({t}* px = x)
                        {{
                            for (; i < n; i += offset)
                            {{
                                {GetVectorizedOp(
                                    $"Unsafe.Read<Vector<{t}>>(px + i)",
                                    "vy")}
                            }}
                        }}
                    }}");
            }

            code.AppendLine($@"
                    for (; i < x.Length; i++)
                        result[i] = {GetOp("x[i]", "y", type)};
                }}");

            // scalar, array
            code.AppendLine($@"
                public static unsafe void {name}({t} x, in ReadOnlySpan<{t}> y, in Span<{r}> result)
                {{
                    var i = 0;");

            if (SupportsVectorization)
            {
                code.AppendLine($@"
                    var offset = Vector<{t}>.Count;
                    if (y.Length >= offset)
                    {{
                        var n = y.Length / offset * offset;
                        var vx = new Vector<{t}>(x);
                        fixed ({r}* pr = result)
                        fixed ({t}* py = y)
                        {{
                            for (; i < n; i += offset)
                            {{
                                {GetVectorizedOp("vx", $"Unsafe.Read<Vector<{t}>>(py + i)")}
                            }}
                        }}
                    }}");
            }

            code.AppendLine($@"
                    for (; i < y.Length; i++)
                        result[i] = {GetOp("x", "y[i]", type)};
                }}");

            return code.ToString();
        }
    }
}
