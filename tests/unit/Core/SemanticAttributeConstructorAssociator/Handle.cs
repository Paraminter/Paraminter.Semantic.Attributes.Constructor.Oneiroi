namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Moq;

using Paraminter.Associators.Queries;
using Paraminter.Queries.Invalidation.Commands;
using Paraminter.Semantic.Attributes.Constructor.Commands;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Handlers;

using System;
using System.Linq.Expressions;

using Xunit;

public sealed class Handle
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullQuery_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullQueryResponseHandler_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void DifferentNumberOfParametersAndArguments_Invalidates()
    {
        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> queryResponseHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup(static (query) => query.Data.Parameters).Returns([]);
        queryMock.Setup(static (query) => query.Data.Arguments).Returns([TypedConstantStore.GetNext()]);

        Target(queryMock.Object, queryResponseHandlerMock.Object);

        queryResponseHandlerMock.Verify(static (handler) => handler.Invalidator.Handle(It.IsAny<IInvalidateQueryResponseCommand>()), Times.Once());
    }

    [Fact]
    public void NoParametersOrArguments_AddsNone()
    {
        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> queryResponseHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup(static (query) => query.Data.Parameters).Returns([]);
        queryMock.Setup(static (query) => query.Data.Arguments).Returns([]);

        Target(queryMock.Object, queryResponseHandlerMock.Object);

        queryResponseHandlerMock.Verify(static (handler) => handler.Invalidator.Handle(It.IsAny<IInvalidateQueryResponseCommand>()), Times.Never());
        queryResponseHandlerMock.Verify(static (handler) => handler.AssociationCollector.Handle(It.IsAny<IAddSemanticAttributeConstructorAssociationCommand>()), Times.Never());
    }

    [Fact]
    public void SameNumberOfParametersAndArguments_AddsAllPairwise()
    {
        var parameter1 = Mock.Of<IParameterSymbol>();
        var parameter2 = Mock.Of<IParameterSymbol>();

        var argument1 = TypedConstantStore.GetNext();
        var argument2 = TypedConstantStore.GetNext();

        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> queryResponseHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns([parameter1, parameter2]);
        queryMock.Setup((query) => query.Data.Arguments).Returns([argument1, argument2]);

        Target(queryMock.Object, queryResponseHandlerMock.Object);

        queryResponseHandlerMock.Verify(static (handler) => handler.Invalidator.Handle(It.IsAny<IInvalidateQueryResponseCommand>()), Times.Never());
        queryResponseHandlerMock.Verify(static (handler) => handler.AssociationCollector.Handle(It.IsAny<IAddSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(2));
        queryResponseHandlerMock.Verify(AssociationExpression(parameter1, argument1), Times.Once());
        queryResponseHandlerMock.Verify(AssociationExpression(parameter2, argument2), Times.Once());
    }

    private static Expression<Action<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler>> AssociationExpression(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        return (handler) => handler.AssociationCollector.Handle(It.Is(MatchAssociationCommand(parameter, argument)));
    }

    private static Expression<Func<IAddSemanticAttributeConstructorAssociationCommand, bool>> MatchAssociationCommand(
        IParameterSymbol parameter,
        TypedConstant argument)
    {
        return (command) => ReferenceEquals(command.Parameter, parameter) && Equals(command.Argument, argument);
    }

    private void Target(
        IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData> query,
        IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler queryResponseHandler)
    {
        Fixture.Sut.Handle(query, queryResponseHandler);
    }
}
