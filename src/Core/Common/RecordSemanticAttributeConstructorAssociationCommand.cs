namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Common;

using Microsoft.CodeAnalysis;

using Paraminter.Semantic.Attributes.Constructor.Commands;

internal sealed class RecordSemanticAttributeConstructorAssociationCommand
    : IRecordSemanticAttributeConstructorAssociationCommand
{
    private readonly IParameterSymbol Parameter;
    private readonly TypedConstant Argument;

    public RecordSemanticAttributeConstructorAssociationCommand(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        Parameter = parameter;
        Argument = argument;
    }

    IParameterSymbol IRecordSemanticAttributeConstructorAssociationCommand.Parameter => Parameter;
    TypedConstant IRecordSemanticAttributeConstructorAssociationCommand.Argument => Argument;
}
