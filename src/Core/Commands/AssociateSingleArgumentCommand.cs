namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Commands;
using Paraminter.Parameters.Method.Models;

internal sealed class AssociateSingleArgumentCommand
    : IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>
{
    private readonly IMethodParameter Parameter;
    private readonly ISemanticAttributeConstructorArgumentData ArgumentData;

    public AssociateSingleArgumentCommand(
        IMethodParameter parameter,
        ISemanticAttributeConstructorArgumentData argumentData)
    {
        Parameter = parameter;
        ArgumentData = argumentData;
    }

    IMethodParameter IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>.Parameter => Parameter;
    ISemanticAttributeConstructorArgumentData IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>.ArgumentData => ArgumentData;
}
