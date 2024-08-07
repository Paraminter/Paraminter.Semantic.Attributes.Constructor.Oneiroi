namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associators.Commands;
using Paraminter.Commands.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Recorders.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        Mock<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> recorderMock = new();
        Mock<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>> invalidatorMock = new();

        SemanticAttributeConstructorAssociator sut = new(recorderMock.Object, invalidatorMock.Object);

        return new Fixture(sut, recorderMock, invalidatorMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> Sut;

        private readonly Mock<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> RecorderMock;
        private readonly Mock<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>> InvalidatorMock;

        public Fixture(
            ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> sut,
            Mock<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> recorderMock,
            Mock<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>> invalidatorMock)
        {
            Sut = sut;

            RecorderMock = recorderMock;
            InvalidatorMock = invalidatorMock;
        }

        ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> IFixture.Sut => Sut;

        Mock<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> IFixture.RecorderMock => RecorderMock;
        Mock<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>> IFixture.InvalidatorMock => InvalidatorMock;
    }
}
