namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associating.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Models;
using Paraminter.Pairing.Commands;
using Paraminter.Parameters.Method.Models;

internal interface IFixture
{
    public abstract ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> Sut { get; }

    public abstract Mock<ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> PairerMock { get; }
    public abstract Mock<ISemanticAttributeConstructorAssociatorErrorHandler> ErrorHandlerMock { get; }
}
