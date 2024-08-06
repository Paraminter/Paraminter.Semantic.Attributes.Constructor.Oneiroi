namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associators.Commands;
using Paraminter.Commands.Handlers;
using Paraminter.Parameters.Method.Models;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullRecorder_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullInvalidator_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsAssociator()
    {
        var result = Target(Mock.Of<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>>(), Mock.Of<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>>());

        Assert.NotNull(result);
    }

    private static SemanticAttributeConstructorAssociator Target(
        ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> recorder,
        ICommandHandler<IInvalidateArgumentAssociationsRecordCommand> invalidator)
    {
        return new SemanticAttributeConstructorAssociator(recorder, invalidator);
    }
}
