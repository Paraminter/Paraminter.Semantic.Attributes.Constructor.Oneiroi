namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Commands;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Recorders.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

using System;

/// <summary>Associates semantic attribute constructor arguments.</summary>
public sealed class SemanticAttributeConstructorAssociator
    : ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>>
{
    private readonly ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> Recorder;
    private readonly ICommandHandler<IInvalidateArgumentAssociationsRecordCommand> Invalidator;

    /// <summary>Instantiates a <see cref="SemanticAttributeConstructorAssociator"/>, associating semantic attribute constructor arguments.</summary>
    /// <param name="recorder">Records associated semantic attribute constructor arguments.</param>
    /// <param name="invalidator">Invalidates the record of associated semantic attribute constructor arguments.</param>
    public SemanticAttributeConstructorAssociator(
        ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> recorder,
        ICommandHandler<IInvalidateArgumentAssociationsRecordCommand> invalidator)
    {
        Recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
        Invalidator = invalidator ?? throw new ArgumentNullException(nameof(invalidator));
    }

    void ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>>.Handle(
        IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Data.Parameters.Count != command.Data.Arguments.Count)
        {
            Invalidator.Handle(InvalidateArgumentAssociationsRecordCommand.Instance);

            return;
        }

        for (var i = 0; i < command.Data.Parameters.Count; i++)
        {
            var parameter = new MethodParameter(command.Data.Parameters[i]);
            var argumentData = new SemanticAttributeConstructorArgumentData(command.Data.Arguments[i]);

            var recordCommand = new RecordSemanticAttributeConstructorAssociationCommand(parameter, argumentData);

            Recorder.Handle(recordCommand);
        }
    }
}
