namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors;

using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;
using Paraminter.Cqs.Handlers;

/// <summary>Handles errors encountered when associating semantic attribute constructor arguments with parameters.</summary>
public interface ISemanticAttributeConstructorAssociatorErrorHandler
{
    /// <summary>Handles there being a different number of arguments and parameters.</summary>
    public abstract ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand> DifferentNumberOfArgumentsAndParameters { get; }
}
