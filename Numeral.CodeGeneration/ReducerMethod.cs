namespace Numeral.CodeGeneration.Arithmetic
{
    /// <summary>
    /// Functions that take an array and reduce it to a rank-1 array by 
    /// summarizing the values along one axis
    /// </summary>
    public class ReducerMethod : ArrayMethod
    {
        public ReducerMethod(string name)
            : base(name) { }

        public string OpFunc { get; set; }

        public override string GenerateArrayFunctionCode(TypeConfig type)
        {
            var t = type.ClassName;
            var r = t;
            var name = $"{Name}_{t}";
            var ufunc = $"UniversalFunctions.{name}";

            return $@"
                public static NDArray<{r}> {Name}(this in NDArray<{t}> x, in NDArray<{r}> result, int axis)
                    => Evaluate.ReduceUnaryOp<{t}, {r}>(x, result, {ufunc}, axis);

                public static NDArray<{r}> {Name}(this in NDArray<{t}> x, int axis)
                    => Evaluate.ReduceUnaryOp<{t}, {r}>(x, {ufunc}, axis);
                
                public static {r} {Name}(this in NDArray<{t}> x)
                    => Evaluate.ReduceUnaryOp<{t}, {r}>(x, {ufunc});
                ";
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
                    result[0] = tmp;
                }}
            ";
        }

        protected virtual string GetInitializeTmp(string tmp, TypeConfig type)
        {
            return $"{type.ClassName} {tmp} = {type.Zero};";
        }

        protected virtual string GetReductionOp(string tmp, string arg, TypeConfig type)
        {
            return $@"{tmp} = {OpFunc}({tmp}, {arg});";
        }
    }
}
