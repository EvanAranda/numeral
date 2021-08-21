using System.Linq;

namespace Numeral.CodeGeneration
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
            bool include = true;

            if (IncludeTypes != null)
                include = IncludeTypes.Contains(type);

            if (ExcludeTypes != null)
                include &= !ExcludeTypes.Contains(type);

            return include;
        }
    }
}
