namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Commands;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Pairing.Commands;
using Paraminter.Parameters.Method.Models;

internal sealed class PairArgumentCommand
    : IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>
{
    private readonly IMethodParameter Parameter;
    private readonly ISemanticAttributeConstructorArgumentData ArgumentData;

    public PairArgumentCommand(
        IMethodParameter parameter,
        ISemanticAttributeConstructorArgumentData argumentData)
    {
        Parameter = parameter;
        ArgumentData = argumentData;
    }

    IMethodParameter IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>.Parameter => Parameter;
    ISemanticAttributeConstructorArgumentData IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>.ArgumentData => ArgumentData;
}
