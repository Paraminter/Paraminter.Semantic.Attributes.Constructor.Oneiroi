namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi;

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

        var type = compilation.GetTypeByMetadataName("Foo")!;
        var attribute = type.GetAttributes()[0];

        return attribute.ConstructorArguments[0];
    }
}
