namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;

using Moq;

using Paraminter.Cqs.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullDifferentNumberOfArgumentsAndParameters_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsHandler()
    {
        var result = Target(Mock.Of<ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand>>());

        Assert.NotNull(result);
    }

    private static SemanticAttributeConstructorAssociatorErrorHandler Target(
        ICommandHandler<IHandleDifferentNumberOfArgumentsAndParametersCommand> differentNumberOfArgumentsAndParameters)
    {
        return new SemanticAttributeConstructorAssociatorErrorHandler(differentNumberOfArgumentsAndParameters);
    }
}
