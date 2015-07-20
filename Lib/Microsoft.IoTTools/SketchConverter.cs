using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Microsoft.IoTTools
{
    public class SketchConverter
    {
        public string Convert()
        {
            // Create root compilation unit and add some using statements
            var cu = SF.CompilationUnit()
                .AddUsings(SF.UsingDirective(SF.IdentifierName("System")))
                .AddUsings(SF.UsingDirective(SF.IdentifierName("System.Generic")));

            // Add the root namespace
            var ns = SF.NamespaceDeclaration(SF.IdentifierName("Microsoft.IoT"));

            // Create a class and make it public
            var c = SF.ClassDeclaration("MyClass")
                .AddModifiers(SF.Token(SyntaxKind.PublicKeyword));

            // Add the class to the namespace
            ns = ns.AddMembers(c);

            // Add the namespace to the compilation unit
            cu = cu.AddMembers(ns);

            // Inject whitespace formatting using default C# rules
            cu = cu.NormalizeWhitespace();

            // Convert to string
            return cu.ToFullString();
        }
    }
}
