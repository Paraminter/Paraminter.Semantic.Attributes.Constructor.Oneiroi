namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

internal static class TypedConstantStore
{
    private static int Current;

    public static TypedConstant GetNext()
    {
        var source = $$"""
            using System;

            public class CustomAttribute : Attribute
            {
                public CustomAttribute(int value) { }
            }

            [CustomAttribute({{Current}})]
            public class Foo { }
            """;

        Current += 1;

        var compilation = CompilationFactory.Create(source);

#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable S1481 // Unused local variables should be removed
        var diagnostics = compilation.GetDiagnostics();
#pragma warning restore S1481 // Unused local variables should be removed
#pragma warning restore IDE0059 // Unnecessary assignment of a value

        var type = compilation.GetTypeByMetadataName("Foo")!;
        var attribute = type.GetAttributes()[0];

        return attribute.ConstructorArguments[0];
    }
}
