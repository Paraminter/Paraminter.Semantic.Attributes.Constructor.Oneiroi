namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Associators.Commands;
using Paraminter.Commands.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        Mock<ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand>> recorderMock = new();

        SemanticAttributeConstructorAssociator sut = new(recorderMock.Object);

        return new Fixture(sut, recorderMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> Sut;

        private readonly Mock<ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand>> RecorderMock;

        public Fixture(
            ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> sut,
            Mock<ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand>> recorderMock)
        {
            Sut = sut;

            RecorderMock = recorderMock;
        }

        ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> IFixture.Sut => Sut;

        Mock<ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand>> IFixture.RecorderMock => RecorderMock;
    }
}
