using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Numeral.CodeGeneration.Arithmetic
{
    public static class TemplateExtensions
    {
        public static string RenderClassFile(
            IEnumerable<string> usings,
            string className,
            IEnumerable<string> members,
            string @namespace = "Numeral",
            string classType = "class",
            string classModifier = "static partial"
        )
        {
            var usingsBlock = string.Join("\n",
                usings.Select(x => $"using {x};"));

            var code = $@"
{usingsBlock}

namespace {@namespace}
{{
    // [GeneratedCode]
    public {classModifier} {classType} {className}
    {{
        {string.Join("\n", members)}
    }}
}}
        
        ";

            return code;
        }

        public static SourceText NormalizeFile(this string codeText)
        {
            codeText = SyntaxFactory.ParseCompilationUnit(codeText)
                .NormalizeWhitespace()
                .GetText()
                .ToString();

            return SourceText.From(codeText, Encoding.UTF8);
        }
    }
}
