namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Common;

using Microsoft.CodeAnalysis;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;

internal sealed class SemanticAttributeConstructorArgumentData
    : ISemanticAttributeConstructorArgumentData
{
    private readonly TypedConstant Argument;

    public SemanticAttributeConstructorArgumentData(
        TypedConstant argument)
    {
        Argument = argument;
    }

    TypedConstant ISemanticAttributeConstructorArgumentData.Argument => Argument;
}
