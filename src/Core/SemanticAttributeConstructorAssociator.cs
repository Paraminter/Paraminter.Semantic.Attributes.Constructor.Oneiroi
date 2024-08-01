namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Paraminter.Associators.Commands;
using Paraminter.Commands.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Common;

using System;

/// <summary>Associates semantic attribute constructor arguments.</summary>
public sealed class SemanticAttributeConstructorAssociator
    : ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>>
{
    private readonly ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand> Recorder;

    /// <summary>Instantiates a <see cref="SemanticAttributeConstructorAssociator"/>, associating semantic attribute constructor arguments.</summary>
    /// <param name="recorder">Records associated semantic attribute constructor arguments.</param>
    public SemanticAttributeConstructorAssociator(
        ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand> recorder)
    {
        Recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
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
            return;
        }

        for (var i = 0; i < command.Data.Parameters.Count; i++)
        {
            var parameter = command.Data.Parameters[i];
            var argument = command.Data.Arguments[i];

            var recordCommand = new RecordSemanticAttributeConstructorAssociationCommand(parameter, argument);

            Recorder.Handle(recordCommand);
        }
    }
}
