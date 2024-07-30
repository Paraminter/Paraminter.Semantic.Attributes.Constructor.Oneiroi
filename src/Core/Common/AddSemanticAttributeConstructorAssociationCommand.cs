namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Common;

using Microsoft.CodeAnalysis;

using Paraminter.Semantic.Attributes.Constructor.Commands;

internal sealed class AddSemanticAttributeConstructorAssociationCommand
    : IAddSemanticAttributeConstructorAssociationCommand
{
    private readonly IParameterSymbol Parameter;
    private readonly TypedConstant Argument;

    public AddSemanticAttributeConstructorAssociationCommand(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        Parameter = parameter;
        Argument = argument;
    }

    IParameterSymbol IAddSemanticAttributeConstructorAssociationCommand.Parameter => Parameter;
    TypedConstant IAddSemanticAttributeConstructorAssociationCommand.Argument => Argument;
}
