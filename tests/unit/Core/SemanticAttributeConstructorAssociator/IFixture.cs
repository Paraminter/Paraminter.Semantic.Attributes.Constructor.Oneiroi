namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Commands;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

internal interface IFixture
{
    public abstract ICommandHandler<IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData>> Sut { get; }

    public abstract Mock<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> IndividualAssociatorMock { get; }
    public abstract Mock<ISemanticAttributeConstructorAssociatorErrorHandler> ErrorHandlerMock { get; }
}
