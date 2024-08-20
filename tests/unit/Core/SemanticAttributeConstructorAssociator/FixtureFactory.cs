namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Commands;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        Mock<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> individualAssociatorMock = new();
        Mock<ISemanticAttributeConstructorAssociatorErrorHandler> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        SemanticAttributeConstructorAssociator sut = new(individualAssociatorMock.Object, errorHandlerMock.Object);

        return new Fixture(sut, individualAssociatorMock, errorHandlerMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly ICommandHandler<IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData>> Sut;

        private readonly Mock<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> IndividualAssociatorMock;
        private readonly Mock<ISemanticAttributeConstructorAssociatorErrorHandler> ErrorHandlerMock;

        public Fixture(
            ICommandHandler<IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData>> sut,
            Mock<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> individualAssociatorMock,
            Mock<ISemanticAttributeConstructorAssociatorErrorHandler> errorHandlerMock)
        {
            Sut = sut;

            IndividualAssociatorMock = individualAssociatorMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        ICommandHandler<IAssociateAllArgumentsCommand<IAssociateAllSemanticAttributeConstructorArgumentsData>> IFixture.Sut => Sut;

        Mock<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> IFixture.IndividualAssociatorMock => IndividualAssociatorMock;
        Mock<ISemanticAttributeConstructorAssociatorErrorHandler> IFixture.ErrorHandlerMock => ErrorHandlerMock;
    }
}
