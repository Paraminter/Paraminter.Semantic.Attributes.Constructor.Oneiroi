namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;

using Moq;

using Paraminter.Cqs.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;

internal interface IFixture
{
    public abstract ISemanticAttributeConstructorAssociatorErrorHandler Sut { get; }

    public abstract Mock<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>> DifferentNumberOfArgumentsAndParametersMock { get; }
}
