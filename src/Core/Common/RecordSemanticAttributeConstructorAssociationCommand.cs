namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Common;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Parameters.Method.Models;
using Paraminter.Recorders.Commands;

internal sealed class RecordSemanticAttributeConstructorAssociationCommand
    : IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>
{
    private readonly IMethodParameter Parameter;
    private readonly ISemanticAttributeConstructorArgumentData ArgumentData;

    public RecordSemanticAttributeConstructorAssociationCommand(
        IMethodParameter parameter,
        ISemanticAttributeConstructorArgumentData argumentData)
    {
        Parameter = parameter;
        ArgumentData = argumentData;
    }

    IMethodParameter IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>.Parameter => Parameter;
    ISemanticAttributeConstructorArgumentData IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>.ArgumentData => ArgumentData;
}
