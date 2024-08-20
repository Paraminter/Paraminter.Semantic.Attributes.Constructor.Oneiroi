namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;

using Paraminter.Cqs.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;

using System;

/// <inheritdoc cref="ISemanticAttributeConstructorAssociatorErrorHandler"/>
public sealed class SemanticAttributeConstructorAssociatorErrorHandler
    : ISemanticAttributeConstructorAssociatorErrorHandler
{
    private readonly ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand> DifferentNumberOfArgumentsAndParameters;

    /// <summary>Instantiates a handler of errors encountered when associating semantic attribute constructor arguments.</summary>
    /// <param name="differentNumberOfArgumentsAndParameters">Handles there being a different number of arguments and parameters.</param>
    public SemanticAttributeConstructorAssociatorErrorHandler(
        ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand> differentNumberOfArgumentsAndParameters)
    {
        DifferentNumberOfArgumentsAndParameters = differentNumberOfArgumentsAndParameters ?? throw new ArgumentNullException(nameof(differentNumberOfArgumentsAndParameters));
    }

    ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand> ISemanticAttributeConstructorAssociatorErrorHandler.DifferentNumberOfArgumentsAndParameters => DifferentNumberOfArgumentsAndParameters;
}
