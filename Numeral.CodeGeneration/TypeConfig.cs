using System;

namespace Numeral.CodeGeneration.Arithmetic
{
    public class TypeConfig
    {
        public TypeConfig(Type type, string name, string zero = "0", string one = "1")
        {
            Type = type;
            ClassName = name;
            Zero = zero;
            One = one;
            DisplayName = name.Substring(0, 1).ToUpper() + name.Substring(1);
        }

        public Type Type { get; }
        public string ClassName { get; }
        public string Zero { get; }
        public string One { get; }
        public string DisplayName { get; }

        public static TypeConfig Bool = new(typeof(bool), "bool", zero: "false", one: "true");
        public static TypeConfig Double = new(typeof(double), "double");
        public static TypeConfig Float = new(typeof(float), "float", zero: "0.0f", one: "1.0f");
        public static TypeConfig Int = new(typeof(int), "int");
        public static TypeConfig Byte = new(typeof(byte), "byte");
    }
}
