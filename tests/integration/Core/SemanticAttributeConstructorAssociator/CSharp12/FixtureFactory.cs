namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associating.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Models;
using Paraminter.Cqs;
using Paraminter.Pairing.Commands;
using Paraminter.Parameters.Method.Models;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        Mock<ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> pairerMock = new();
        Mock<ISemanticAttributeConstructorAssociatorErrorHandler> errorHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        SemanticAttributeConstructorAssociator sut = new(pairerMock.Object, errorHandlerMock.Object);

        return new Fixture(sut, pairerMock, errorHandlerMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> Sut;

        private readonly Mock<ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> PairerMock;
        private readonly Mock<ISemanticAttributeConstructorAssociatorErrorHandler> ErrorHandlerMock;

        public Fixture(
            ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> sut,
            Mock<ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> pairerMock,
            Mock<ISemanticAttributeConstructorAssociatorErrorHandler> errorHandlerMock)
        {
            Sut = sut;

            PairerMock = pairerMock;
            ErrorHandlerMock = errorHandlerMock;
        }

        ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> IFixture.Sut => Sut;

        Mock<ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> IFixture.PairerMock => PairerMock;
        Mock<ISemanticAttributeConstructorAssociatorErrorHandler> IFixture.ErrorHandlerMock => ErrorHandlerMock;
    }
}
