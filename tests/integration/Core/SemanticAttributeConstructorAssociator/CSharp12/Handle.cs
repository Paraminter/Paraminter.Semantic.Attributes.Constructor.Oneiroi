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

        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns(parameters);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns(arguments);

        Target(commandMock.Object);

        Fixture.RecorderMock.Verify(static (recorder) => recorder.Handle(It.IsAny<IRecordSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(3));
        Fixture.RecorderMock.Verify(RecordExpression(parameters[0], arguments[0]), Times.Once());
        Fixture.RecorderMock.Verify(RecordExpression(parameters[1], arguments[1]), Times.Once());
        Fixture.RecorderMock.Verify(RecordExpression(parameters[2], arguments[2]), Times.Once());
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

        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns(parameters);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns(arguments);

        Target(commandMock.Object);

        Fixture.RecorderMock.Verify(static (recorder) => recorder.Handle(It.IsAny<IRecordSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(1));
        Fixture.RecorderMock.Verify(RecordExpression(parameters[0], arguments[0]), Times.Once());
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

        Mock<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> commandMock = new();

        commandMock.Setup(static (command) => command.Data.Parameters).Returns(parameters);
        commandMock.Setup(static (command) => command.Data.Arguments).Returns(arguments);

        Target(commandMock.Object);

        Fixture.RecorderMock.Verify(static (recorder) => recorder.Handle(It.IsAny<IRecordSemanticAttributeConstructorAssociationCommand>()), Times.Exactly(1));
        Fixture.RecorderMock.Verify(RecordExpression(parameters[0], arguments[0]), Times.Once());
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
