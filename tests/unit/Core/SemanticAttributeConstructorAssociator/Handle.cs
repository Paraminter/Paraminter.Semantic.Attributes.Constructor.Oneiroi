namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Moq;

using Paraminter.Associators.Commands;
using Paraminter.Commands.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;

using System;
using System.Linq.Expressions;

using Xunit;

public sealed class Handle
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullCommand_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void DifferentNumberOfParametersAndArguments_RecordsNone()
    {
        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns([]);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns([TypedConstantStore.GetNext()]);

        Target(commandMock.Object);

        Fixture.RecorderMock.Verify(static (recorder) => recorder.Handle(It.IsAny<IRecordSemanticAttributeConstructorAssociationCommand>()), Times.Never());
    }

    [Fact]
    public void NoParametersOrArguments_RecordsNone()
    {
        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns([]);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns([]);

        Target(commandMock.Object);

        Fixture.RecorderMock.Verify(static (recorder) => recorder.Handle(It.IsAny<IRecordSemanticAttributeConstructorAssociationCommand>()), Times.Never());
    }

    [Fact]
    public void SameNumberOfParametersAndArguments_RecordsAllPairwise()
    {
        var parameter1 = Mock.Of<IParameterSymbol>();
        var parameter2 = Mock.Of<IParameterSymbol>();

        var argument1 = TypedConstantStore.GetNext();
        var argument2 = TypedConstantStore.GetNext();

        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns([parameter1, parameter2]);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns([argument1, argument2]);

        Target(commandMock.Object);

        Fixture.RecorderMock.Verify(static (recorder) => recorder.Handle(It.IsAny<IRecordSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(2));
        Fixture.RecorderMock.Verify(RecordExpression(parameter1, argument1), Times.Once());
        Fixture.RecorderMock.Verify(RecordExpression(parameter2, argument2), Times.Once());
    }

    private static Expression<Action<ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand>>> RecordExpression(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        return (recorder) => recorder.Handle(It.Is(MatchRecordCommand(parameter, argument)));
    }

    private static Expression<Func<IRecordSemanticAttributeConstructorAssociationCommand, bool>> MatchRecordCommand(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        return (command) => ReferenceEquals(command.Parameter, parameter) && Equals(command.Argument, argument);
    }

    private void Target(
        IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData> command)
    {
        Fixture.Sut.Handle(command);
    }
}
