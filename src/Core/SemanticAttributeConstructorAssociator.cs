namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associating.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Pairing.Commands;
using Paraminter.Parameters.Method.Models;

using System;

/// <summary>Associates semantic attribute constructor arguments with parameters.</summary>
public sealed class SemanticAttributeConstructorAssociator
    : ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>>
{
    private readonly ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> Pairer;
    private readonly ISemanticAttributeConstructorAssociatorErrorHandler ErrorHandler;

    /// <summary>Instantiates an associator of semantic attribute constructor arguments with parameters.</summary>
    /// <param name="pairer">Pairs semantic attribute constructor arguments with parameters.</param>
    /// <param name="errorHandler">Handles encountered errors.</param>
    public SemanticAttributeConstructorAssociator(
        ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> pairer,
        ISemanticAttributeConstructorAssociatorErrorHandler errorHandler)
    {
        Pairer = pairer ?? throw new ArgumentNullException(nameof(pairer));
        ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    void ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>>.Handle(
        IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Data.Parameters.Count != command.Data.Arguments.Count)
        {
            ErrorHandler.DifferentNumberOfArgumentsAndParameters.Handle(HandleDifferentNumberOfArgumentsAndParametersCommand.Instance);

            return;
        }

        for (var i = 0; i < command.Data.Parameters.Count; i++)
        {
            PairArgument(command.Data.Parameters[i], command.Data.Arguments[i]);
        }
    }

    private void PairArgument(
        IParameterSymbol parameterSymbol,
        TypedConstant argument)
    {
        var parameter = new MethodParameter(parameterSymbol);
        var argumentData = new SemanticAttributeConstructorArgumentData(argument);

        var command = new PairArgumentCommand(parameter, argumentData);

        Pairer.Handle(command);
    }
}
