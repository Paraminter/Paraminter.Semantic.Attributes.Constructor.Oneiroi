namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Commands.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Commands;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullRecorder_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsAssociator()
    {
        var result = Target(Mock.Of<ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand>>());

        Assert.NotNull(result);
    }

    private static SemanticAttributeConstructorAssociator Target(
        ICommandHandler<IRecordSemanticAttributeConstructorAssociationCommand> recorder)
    {
        return new SemanticAttributeConstructorAssociator(recorder);
    }
}
