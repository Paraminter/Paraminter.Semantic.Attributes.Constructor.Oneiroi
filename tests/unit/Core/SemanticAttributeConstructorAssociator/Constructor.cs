namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Commands;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Errors;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullIndividualAssociator_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!, Mock.Of<ISemanticAttributeConstructorAssociatorErrorHandler>()));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void NullErrorHandler_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(Mock.Of<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>>(), null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsAssociator()
    {
        var result = Target(Mock.Of<ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>>(), Mock.Of<ISemanticAttributeConstructorAssociatorErrorHandler>());

        Assert.NotNull(result);
    }

    private static SemanticAttributeConstructorAssociator Target(
        ICommandHandler<IAssociateSingleArgumentCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>> individualAssociator,
        ISemanticAttributeConstructorAssociatorErrorHandler errorHandler)
    {
        return new SemanticAttributeConstructorAssociator(individualAssociator, errorHandler);
    }
}
