namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors;

using Moq;

using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;
using Paraminter.Cqs.Handlers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        var differentNumberOfArgumentsAndParametersMock = new Mock<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>>();

        var sut = new SemanticAttributeConstructorAssociatorErrorHandler(differentNumberOfArgumentsAndParametersMock.Object);

        return new Fixture(sut, differentNumberOfArgumentsAndParametersMock);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly ISemanticAttributeConstructorAssociatorErrorHandler Sut;

        private readonly Mock<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>> DifferentNumberOfArgumentsAndParametersMock;

        public Fixture(
            ISemanticAttributeConstructorAssociatorErrorHandler sut,
            Mock<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>> differentNumberOfArgumentsAndParametersMock)
        {
            Sut = sut;

            DifferentNumberOfArgumentsAndParametersMock = differentNumberOfArgumentsAndParametersMock;
        }

        ISemanticAttributeConstructorAssociatorErrorHandler IFixture.Sut => Sut;

        Mock<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>> IFixture.DifferentNumberOfArgumentsAndParametersMock => DifferentNumberOfArgumentsAndParametersMock;
    }
}
