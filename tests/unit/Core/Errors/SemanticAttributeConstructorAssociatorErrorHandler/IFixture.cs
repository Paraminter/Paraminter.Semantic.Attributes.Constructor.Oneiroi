namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors;

using Moq;

using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;
using Paraminter.Cqs;

internal interface IFixture
{
    public abstract ISemanticAttributeConstructorAssociatorErrorHandler Sut { get; }

    public abstract Mock<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>> DifferentNumberOfArgumentsAndParametersMock { get; }
}
