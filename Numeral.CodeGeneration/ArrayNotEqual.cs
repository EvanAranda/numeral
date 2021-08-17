namespace Numeral.CodeGeneration.Arithmetic
{
    class ArrayNotEqual : BinaryMethod
    {
        public ArrayNotEqual()
            : base("ArrayNotEqual")
        {
            Op = "!=";
            SupportsVectorization = true;
            IsComparison = true;
        }

        protected override string GetVectorizedOp(string x, string y)
        {
            return $@"
                    var t = Vector.Negate(Vector.Equals({x}, {y}));
                    
                    for (int j = 0; j < offset; j++)
                        Unsafe.Write(pr + i + j, t[j] == -1);";
        }
    }
}
