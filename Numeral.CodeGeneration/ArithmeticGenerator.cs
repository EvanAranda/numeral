using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Numeral.CodeGeneration.Arithmetic
{

    [Generator]
    public class ArithmeticMethodsGenerator : ISourceGenerator
    {
        private static string[] Usings = new[] {
            "System",
            "System.CodeDom.Compiler",
            "System.Numerics",
            "System.Runtime.CompilerServices",
            "Numeral.Evaluators"
        };

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("ElemWiseUniversalFunctions.cs", GenerateElemWiseUFuncs());
            context.AddSource("ElemWiseArrayFunctions.cs", GenerateElemWiseAFuncs());
            context.AddSource("ReducerUniversalFunctions.cs", GenerateReducerUFuncs());
            context.AddSource("ReducerArrayFunctions.cs", GenerateReducerAFuncs());
            context.AddSource("NDArrayOperatorOverloads.cs", GenerateNDArrayOperatorOverloads());
            context.AddSource("NDArrayOperatorBindings.cs", GenerateNDArrayOperatorBindings());
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }

        private SourceText RenderClassFile(string className, string[] members, string nameSpace = "Numeral", string objectType = "class", string classModifier = "static partial")
        {
            return TemplateExtensions
                .RenderClassFile(Usings, className, members, nameSpace, objectType, classModifier)
                .NormalizeFile();
        }

        private SourceText GenerateElemWiseUFuncs()
        {
            var methods = GetOperatorMethods()
                .SelectMany(m => GetTypes()
                    .Where(t => m.SupportsType(t))
                    .Select(t => (method: m, type: t)))
                .Select(x => x.method
                    .GenerateUniversalFunctionCode(x.type))
                .ToArray();

            return RenderClassFile("UniversalFunctions", methods);
        }

        /// <summary>
        /// Generates implementations for common (broadcasted) functions on
        /// arrays.
        /// </summary>
        /// <returns></returns>
        private SourceText GenerateElemWiseAFuncs()
        {
            var methods = GetOperatorMethods()
                .SelectMany(m => GetTypes()
                    .Where(t => m.SupportsType(t))
                    .Select(t => (method: m, type: t)))
                .Select(x => x.method
                    .GenerateArrayFunctionCode(x.type))
                .ToArray();

            return RenderClassFile("NDArray", methods);
        }

        private SourceText GenerateReducerUFuncs()
        {
            var methods = GetReducerMethods()
                .SelectMany(m => GetTypes()
                    .Where(t => m.SupportsType(t))
                    .Select(t => (method: m, type: t)))
                .Select(x => x.method
                    .GenerateUniversalFunctionCode(x.type))
                .ToArray();

            return RenderClassFile("UniversalFunctions", methods);
        }

        /// <summary>
        /// Generates implementations for common (broadcasted) functions on
        /// arrays.
        /// </summary>
        /// <returns></returns>
        private SourceText GenerateReducerAFuncs()
        {
            var methods = GetReducerMethods()
                .SelectMany(m => GetTypes()
                    .Where(t => m.SupportsType(t))
                    .Select(t => (method: m, type: t)))
                .Select(x => x.method
                    .GenerateArrayFunctionCode(x.type))
                .ToArray();

            return RenderClassFile("NDArray", methods);
        }

        private SourceText GenerateNDArrayOperatorBindings()
        {
            var code = new StringBuilder();

            var binaryMethods = GetOperatorMethods()
                .OfType<BinaryMethod>()
                .Where(x => x.HasOperatorOverload);

            foreach (var method in binaryMethods)
            {
                var r = method.IsComparison ? "bool" : "T";
                code.Append($"public static NDArray<{r}> {method.Name}<T>(in NDArray<T> x, in NDArray<T> y) {{");
                foreach (var type in GetTypes())
                {
                    if (!method.SupportsType(type))
                        continue;

                    var t = type.ClassName;
                    r = method.IsComparison ? "bool" : t;
                    code.Append($@"
                        if (typeof(T) == typeof({t}))
                            return NDArray.{method.Name}(x.ReinterpretCast<T, {t}>(), y.ReinterpretCast<T, {t}>())");

                    if (method.IsComparison)
                        code.AppendLine(";");
                    else
                        code.AppendLine($".ReinterpretCast<{r}, T>();");
                }
                code.Append("throw new NotSupportedException();");
                code.Append("}");

                r = method.IsComparison ? "bool" : "T";
                code.Append($"public static NDArray<{r}> {method.Name}<T>(in NDArray<T> x, T y) {{");
                foreach (var type in GetTypes())
                {
                    if (!method.SupportsType(type))
                        continue;

                    var t = type.ClassName;
                    r = method.IsComparison ? "bool" : t;
                    code.Append($@"
                        if (typeof(T) == typeof({t}))
                            return NDArray.{method.Name}(x.ReinterpretCast<T, {t}>(), ({t})(object)y)");

                    if (method.IsComparison)
                        code.AppendLine(";");
                    else
                        code.AppendLine($".ReinterpretCast<{r}, T>();");
                }
                code.Append("throw new NotSupportedException();");
                code.Append("}");

                r = method.IsComparison ? "bool" : "T";
                code.Append($"public static NDArray<{r}> {method.Name}<T>(T x, in NDArray<T> y) {{");
                foreach (var type in GetTypes())
                {
                    if (!method.SupportsType(type))
                        continue;

                    var t = type.ClassName;
                    r = method.IsComparison ? "bool" : t;
                    code.Append($@"
                        if (typeof(T) == typeof({t}))
                            return NDArray.{method.Name}(({t})(object)x, y.ReinterpretCast<T, {t}>())");

                    if (method.IsComparison)
                        code.AppendLine(";");
                    else
                        code.AppendLine($".ReinterpretCast<{r}, T>();");
                }
                code.Append("throw new NotSupportedException();");
                code.Append("}");
            }

            var unaryMethods = GetOperatorMethods()
                .OfType<UnaryMethod>()
                .Where(x => x.HasOperatorOverload);

            foreach (var method in unaryMethods)
            {
                code.Append($"public static NDArray<T> {method.Name}<T>(in NDArray<T> x) {{");
                foreach (var type in GetTypes())
                {
                    if (!method.SupportsType(type))
                        continue;

                    code.Append($@"
                        if (typeof(T) == typeof({type.ClassName}))
                            return NDArray.{method.Name}(x.ReinterpretCast<T, {type.ClassName}>()).ReinterpretCast<{type.ClassName}, T>();");

                }
                code.Append("throw new NotSupportedException();");
                code.Append("}");
            }

            return RenderClassFile("NDArrayOperatorBindings", new[] { code.ToString() });
        }

        private SourceText GenerateNDArrayOperatorOverloads()
        {
            var overloads = GetOperatorMethods().OfType<OperatorMethod>()
                .Where(x => x.HasOperatorOverload)
                .Select(x => x.GenerateOperatorOverloadCode().ToString())
                .ToArray();

            return RenderClassFile("NDArray<T>", overloads,
                objectType: "struct",
                classModifier: "readonly partial");
        }

        public IEnumerable<OperatorMethod> GetOperatorMethods()
        {
            var floatTypes = new[] { TypeConfig.Float, TypeConfig.Double };

            yield return new BinaryMethod("Add") { Op = "+" };
            yield return new BinaryMethod("Subtract") { Op = "-" };
            yield return new BinaryMethod("Multiply") { Op = "*" };
            yield return new BinaryMethod("Divide") { Op = "/" };
            yield return new BinaryMethod("Pow") { SupportsVectorization = false, OpFunc = "Math.Pow" };

            yield return new BinaryMethod("Maximum") { VectorizedName = "Max", OpFunc = "Math.Max" };
            yield return new BinaryMethod("Minimum") { VectorizedName = "Min", OpFunc = "Math.Min" };

            yield return new BinaryMethod("ArrayEqual") { Op = "==", IsComparison = true, VectorizedName = "Equals" };
            yield return new ArrayNotEqual();
            yield return new BinaryMethod("LessThan") { Op = "<", IsComparison = true };
            yield return new BinaryMethod("GreaterThan") { Op = ">", IsComparison = true };
            yield return new BinaryMethod("LessThanOrEqual") { Op = "<=", IsComparison = true };
            yield return new BinaryMethod("GreaterThanOrEqual") { Op = ">=", IsComparison = true };

            yield return new UnaryMethod("Negate") { Op = "-" };
            yield return new UnaryMethod("Abs") { OpFunc = "Math.Abs" };
            yield return new UnaryMethod("Floor") { OpFunc = "Math.Floor", IncludeTypes = floatTypes };
            yield return new UnaryMethod("Ceiling") { OpFunc = "Math.Ceiling", IncludeTypes = floatTypes };

            yield return new UnaryMethod("Sin") { SupportsVectorization = false, OpFunc = "Math.Sin", IncludeTypes = floatTypes };
            yield return new UnaryMethod("Cos") { SupportsVectorization = false, OpFunc = "Math.Cos", IncludeTypes = floatTypes };
            yield return new UnaryMethod("Tan") { SupportsVectorization = false, OpFunc = "Math.Tan", IncludeTypes = floatTypes };
            yield return new UnaryMethod("Sqrt") { OpFunc = "Math.Sqrt", VectorizedName = "SquareRoot", IncludeTypes = floatTypes };
        }

        public IEnumerable<ReducerMethod> GetReducerMethods()
        {
            yield return new ReducerMethod("Min") { OpFunc = "Math.Min" };
            yield return new ReducerMethod("Max") { OpFunc = "Math.Max" };
            yield return new MeanReducer();
            yield return new SumReducer();
            yield return new ProductSumReducer();
        }

        public IEnumerable<TypeConfig> GetTypes()
        {
            yield return TypeConfig.Int;
            yield return TypeConfig.Float;
            yield return TypeConfig.Double;
        }
    }

    class SumReducer : ReducerMethod
    {
        public SumReducer()
            : base("Sum") { }

        protected override string GetReductionOp(string tmp, string arg, TypeConfig type)
        {
            return $@"{tmp} = {tmp} + {arg};";
        }
    }

    class ProductSumReducer : ReducerMethod
    {
        public ProductSumReducer()
            : base("ProductSum") { }

        protected override string GetReductionOp(string tmp, string arg, TypeConfig type)
        {
            return $@"{tmp} = {tmp} * {arg};";
        }

        protected override string GetInitializeTmp(string tmp, TypeConfig type)
        {
            return $"{type.ClassName} {tmp} = {type.One};";
        }

    }

    class MeanReducer : ReducerMethod
    {
        public MeanReducer()
            : base("Mean")
        {
        }

        public override string GenerateUniversalFunctionCode(TypeConfig type)
        {
            var t = type.ClassName;
            var name = $"{Name}_{t}";

            return $@"
                public static void {name}(this in ReadOnlySpan<{t}> x, in Span<{t}> result)
                {{
                    {GetInitializeTmp("tmp", type)}
                    for (int i = 0; i < x.Length; i++)
                    {{
                        {GetReductionOp("tmp", "x[i]", type)}
                    }}
                    result[0] = tmp / x.Length;
                }}
            ";
        }

        protected override string GetReductionOp(string tmp, string arg, TypeConfig type)
        {
            return $@"{tmp} = {tmp} + {arg};";
        }
    }
}
