namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associating.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;
using Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Models;
using Paraminter.Cqs;
using Paraminter.Pairing.Commands;
using Paraminter.Parameters.Method.Models;

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
    public void DifferentNumberOfParametersAndArguments_HandlesError()
    {
        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns([]);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns([TypedConstantStore.GetNext()]);

        Target(commandMock.Object);

        Fixture.ErrorHandlerMock.Verify(static (handler) => handler.DifferentNumberOfArgumentsAndParameters.Handle(It.IsAny<IHandleDifferentNumberOfArgumentsAndParametersCommand>()), Times.Once());

        Fixture.PairerMock.Verify(static (handler) => handler.Handle(It.IsAny<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>()), Times.Never());
    }

    [Fact]
    public void NoParametersOrArguments_PairsNone()
    {
        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns([]);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns([]);

        Target(commandMock.Object);

        Fixture.ErrorHandlerMock.Verify(static (handler) => handler.DifferentNumberOfArgumentsAndParameters.Handle(It.IsAny<IHandleDifferentNumberOfArgumentsAndParametersCommand>()), Times.Never());

        Fixture.PairerMock.Verify(static (handler) => handler.Handle(It.IsAny<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>()), Times.Never());
    }

    [Fact]
    public void SameNumberOfParametersAndArguments_PairsAll()
    {
        var parameter1 = Mock.Of<IParameterSymbol>();
        var parameter2 = Mock.Of<IParameterSymbol>();

        var argument1 = TypedConstantStore.GetNext();
        var argument2 = TypedConstantStore.GetNext();

        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns([parameter1, parameter2]);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns([argument1, argument2]);

        Target(commandMock.Object);

        Fixture.ErrorHandlerMock.Verify(static (handler) => handler.DifferentNumberOfArgumentsAndParameters.Handle(It.IsAny<IHandleDifferentNumberOfArgumentsAndParametersCommand>()), Times.Never());

        Fixture.PairerMock.Verify(PairArgumentExpression(parameter1, argument1), Times.Once());
        Fixture.PairerMock.Verify(PairArgumentExpression(parameter2, argument2), Times.Once());
        Fixture.PairerMock.Verify(static (handler) => handler.Handle(It.IsAny<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>()), Times.Exactly(2));
    }

    private static Expression<Action<ICommandHandler<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>>> PairArgumentExpression(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        return (handler) => handler.Handle(It.Is(MatchPairArgumentCommand(parameter, argument)));
    }

    private static Expression<Func<IPairArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>, bool>> MatchPairArgumentCommand(
        IParameterSymbol parameterSymbol,
        TypedConstant argument)
    {
        return (command) => MatchParameter(parameterSymbol, command.Parameter) && MatchArgumentData(argument, command.ArgumentData);
    }

    private static bool MatchParameter(
        IParameterSymbol parameterSymbol,
        IMethodParameter parameter)
    {
        return ReferenceEquals(parameterSymbol, parameter.Symbol);
    }

    private static bool MatchArgumentData(
        TypedConstant argument,
        ISemanticAttributeConstructorArgumentData argumentData)
    {
        return Equals(argument, argumentData.Argument);
    }

    private void Target(
        IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorArgumentsData> command)
    {
        Fixture.Sut.Handle(command);
    }
}
