namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Microsoft.CodeAnalysis;

using Moq;

using Paraminter.Associators.Queries;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Collectors;

using Xunit;

public sealed class Handle
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void AttributeUsage_NormalArguments_AssociatesAll()
    {
        var source = """
            using System;

            [Test(1, "", false)]
            public class Foo { }

            public class TestAttribute : Attribute
            {
                public TestAttribute(int a, string b, bool c) { }
            }
            """;

        var compilation = CompilationFactory.Create(source);

        var type = compilation.GetTypeByMetadataName("Foo")!;
        var attribute = type.GetAttributes()[0];
        var parameters = attribute.AttributeConstructor!.Parameters;
        var arguments = attribute.ConstructorArguments;

        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> queryResponseCollectorMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns(parameters);
        queryMock.Setup((query) => query.Data.Arguments).Returns(arguments);

        Target(queryMock.Object, queryResponseCollectorMock.Object);

        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameters[0], arguments[0]), Times.Once());
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameters[1], arguments[1]), Times.Once());
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameters[2], arguments[2]), Times.Once());
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(It.IsAny<IParameterSymbol>(), It.IsAny<TypedConstant>()), Times.Exactly(3));

        queryResponseCollectorMock.Verify((collector) => collector.Invalidator.Invalidate(), Times.Never());
    }

    [Fact]
    public void AttributeUsage_ParamsArguments_AssociatesAll()
    {
        var source = """
            using System;

            [Test(1, 2, 3)]
            public class Foo { }

            public class TestAttribute : Attribute
            {
                public TestAttribute(params int[] a) { }
            }
            """;

        var compilation = CompilationFactory.Create(source);

        var type = compilation.GetTypeByMetadataName("Foo")!;
        var attribute = type.GetAttributes()[0];
        var parameters = attribute.AttributeConstructor!.Parameters;
        var arguments = attribute.ConstructorArguments;

        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> queryResponseCollectorMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns(parameters);
        queryMock.Setup((query) => query.Data.Arguments).Returns(arguments);

        Target(queryMock.Object, queryResponseCollectorMock.Object);

        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameters[0], arguments[0]), Times.Once());
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(It.IsAny<IParameterSymbol>(), It.IsAny<TypedConstant>()), Times.Exactly(1));

        queryResponseCollectorMock.Verify((collector) => collector.Invalidator.Invalidate(), Times.Never());
    }

    [Fact]
    public void AttributeUsage_DefaultArgument_AssociatesAll()
    {
        var source = """
            using System;

            [Test]
            public class Foo { }

            public class TestAttribute : Attribute
            {
                public TestAttribute(int a = 3) { }
            }
            """;

        var compilation = CompilationFactory.Create(source);

        var type = compilation.GetTypeByMetadataName("Foo")!;
        var attribute = type.GetAttributes()[0];
        var parameters = attribute.AttributeConstructor!.Parameters;
        var arguments = attribute.ConstructorArguments;

        Mock<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>> queryMock = new();
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> queryResponseCollectorMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns(parameters);
        queryMock.Setup((query) => query.Data.Arguments).Returns(arguments);

        Target(queryMock.Object, queryResponseCollectorMock.Object);

        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(parameters[0], arguments[0]), Times.Once());
        queryResponseCollectorMock.Verify((collector) => collector.Associations.Add(It.IsAny<IParameterSymbol>(), It.IsAny<TypedConstant>()), Times.Exactly(1));

        queryResponseCollectorMock.Verify((collector) => collector.Invalidator.Invalidate(), Times.Never());
    }

    private void Target(
        IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData> query,
        IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector queryResponseCollector)
    {
        Fixture.Sut.Handle(query, queryResponseCollector);
    }
}
