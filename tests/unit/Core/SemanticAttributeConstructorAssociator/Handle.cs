namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Moq;

using Paraminter.Associators.Queries;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Collectors;

using System;

using Xunit;

public sealed class Handle
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullQuery_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullQueryResponseCollector_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void DifferentNumberOfParametersAndArguments_Invalidates()
    {
        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> queryResponseCollectorMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup(static (query) => query.Data.Parameters).Returns([]);
        queryMock.Setup(static (query) => query.Data.Arguments).Returns([TypedConstantStore.GetNext()]);

        Target(queryMock.Object, queryResponseCollectorMock.Object);

        queryResponseCollectorMock.Verify(static (collector) => collector.Invalidator.Invalidate(), Times.Once());
    }

    [Fact]
    public void NoParametersOrArguments_AddsNone()
    {
        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> queryResponseCollectorMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup(static (query) => query.Data.Parameters).Returns([]);
        queryMock.Setup(static (query) => query.Data.Arguments).Returns([]);

        Target(queryMock.Object, queryResponseCollectorMock.Object);

        queryResponseCollectorMock.Verify(static (collector) => collector.Invalidator.Invalidate(), Times.Never());
        queryResponseCollectorMock.Verify(static (collector) => collector.Associations.Add(It.IsAny<IParameterSymbol>(), It.IsAny<TypedConstant>()), Times.Never());
    }

    [Fact]
    public void SameNumberOfParametersAndArguments_AddsAllPairwise()
    {
        var parameter1 = Mock.Of<IParameterSymbol>();
        var parameter2 = Mock.Of<IParameterSymbol>();

        var argument1 = TypedConstantStore.GetNext();
        var argument2 = TypedConstantStore.GetNext();

        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> queryResponseCollectorMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns([parameter1, parameter2]);
        queryMock.Setup((query) => query.Data.Arguments).Returns([argument1, argument2]);

        Target(queryMock.Object, queryResponseCollectorMock.Object);

        queryResponseCollectorMock.Verify(static (collector) => collector.Invalidator.Invalidate(), Times.Never());
        queryResponseCollectorMock.Verify(static (collector) => collector.Associations.Add(It.IsAny<IParameterSymbol>(), It.IsAny<TypedConstant>()), Times.Exactly(2));
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameter1, argument1), Times.Once());
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameter2, argument2), Times.Once());
    }

    private void Target(
        IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData> query,
        IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector queryResponseCollector)
    {
        Fixture.Sut.Handle(query, queryResponseCollector);
    }
}
