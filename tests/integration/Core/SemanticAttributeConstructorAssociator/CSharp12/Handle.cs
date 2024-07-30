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
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> queryResponseHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns(parameters);
        queryMock.Setup((query) => query.Data.Arguments).Returns(arguments);

        Target(queryMock.Object, queryResponseHandlerMock.Object);

        queryResponseHandlerMock.Verify((handler) => handler.Invalidator.Handle(It.IsAny<IInvalidateQueryResponseCommand>()), Times.Never());
        queryResponseHandlerMock.Verify(static (handler) => handler.AssociationCollector.Handle(It.IsAny<IAddSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(3));
        queryResponseHandlerMock.Verify(AssociationExpression(parameters[0], arguments[0]), Times.Once());
        queryResponseHandlerMock.Verify(AssociationExpression(parameters[1], arguments[1]), Times.Once());
        queryResponseHandlerMock.Verify(AssociationExpression(parameters[2], arguments[2]), Times.Once());
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
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> queryResponseHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns(parameters);
        queryMock.Setup((query) => query.Data.Arguments).Returns(arguments);

        Target(queryMock.Object, queryResponseHandlerMock.Object);

        queryResponseHandlerMock.Verify((handler) => handler.Invalidator.Handle(It.IsAny<IInvalidateQueryResponseCommand>()), Times.Never());
        queryResponseHandlerMock.Verify((handler) => handler.AssociationCollector.Handle(It.IsAny<IAddSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(1));
        queryResponseHandlerMock.Verify(AssociationExpression(parameters[0], arguments[0]), Times.Once());
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
        Mock<IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> queryResponseHandlerMock = new() { DefaultValue = DefaultValue.Mock };

        queryMock.Setup((query) => query.Data.Parameters).Returns(parameters);
        queryMock.Setup((query) => query.Data.Arguments).Returns(arguments);

        Target(queryMock.Object, queryResponseHandlerMock.Object);

        queryResponseHandlerMock.Verify((handler) => handler.Invalidator.Handle(It.IsAny<IInvalidateQueryResponseCommand>()), Times.Never());
        queryResponseHandlerMock.Verify((handler) => handler.AssociationCollector.Handle(It.IsAny<IAddSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(1));
        queryResponseHandlerMock.Verify(AssociationExpression(parameters[0], arguments[0]), Times.Once());
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
