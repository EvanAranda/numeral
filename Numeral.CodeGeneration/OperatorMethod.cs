using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.Text;

namespace Numeral.CodeGeneration.Arithmetic
{
    public abstract class ArrayMethod
    {
        public ArrayMethod(string name)
        {
            Name = name;
            VectorizedName = name;
        }

        public string Name { get; }
        public object VectorizedName { get; set; }
        public TypeConfig[] IncludeTypes { get; set; }
        public TypeConfig[] ExcludeTypes { get; set; }

        /// <summary>
        /// Generate the universal function code
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract string GenerateUniversalFunctionCode(TypeConfig type);
        public abstract string GenerateArrayFunctionCode(TypeConfig type);

        public virtual bool SupportsType(TypeConfig type)
        {
            if (IncludeTypes != null)
                return IncludeTypes.Contains(type);

            if (ExcludeTypes != null)
                return !ExcludeTypes.Contains(type);

            return true;
        }
    }

    public abstract class OperatorMethod : ArrayMethod
    {
        public OperatorMethod(string name) : base(name)
        {
            SupportsVectorization = true;
        }

        public string Op { get; set; }
        public string OpFunc { get; set; }
        public bool HasOperatorOverload => Op != null;
        public bool UseOpFunc => OpFunc != null;
        public bool SupportsVectorization { get; set; }
        public abstract string GenerateOperatorOverloadCode();

    }
}
