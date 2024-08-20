namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Commands;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

using System;

/// <summary>Associates semantic attribute constructor arguments with parameters.</summary>
public sealed class SemanticAttributeConstructorAssociator
    : ICommandHandler<IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData>>
{
    private readonly ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> IndividualAssociator;
    private readonly ISemanticAttributeConstructorAssociatorErrorHandler ErrorHandler;

    /// <summary>Instantiates an associator of semantic attribute constructor arguments with parameters.</summary>
    /// <param name="individualAssociator">Associates individual semantic attribute constructor arguments with parameters.</param>
    /// <param name="errorHandler">Handles encountered errors.</param>
    public SemanticAttributeConstructorAssociator(
        ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> individualAssociator,
        ISemanticAttributeConstructorAssociatorErrorHandler errorHandler)
    {
        IndividualAssociator = individualAssociator ?? throw new ArgumentNullException(nameof(individualAssociator));
        ErrorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }

    void ICommandHandler<IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData>>.Handle(
        IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData> command)
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
            AssociateArgument(command.Data.Parameters[i], command.Data.Arguments[i]);
        }
    }

    private void AssociateArgument(
        IParameterSymbol parameterSymbol,
        TypedConstant argument)
    {
        var parameter = new MethodParameter(parameterSymbol);
        var argumentData = new SemanticAttributeConstructorArgumentData(argument);

        var command = new AssociateSingleArgumentCommand(parameter, argumentData);

        IndividualAssociator.Handle(command);
    }
}
