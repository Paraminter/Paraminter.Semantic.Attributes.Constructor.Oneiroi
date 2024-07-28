namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void ReturnsAssociator()
    {
        var result = Target();

        Assert.NotNull(result);
    }

    private static SemanticAttributeConstructorAssociator Target() => new();
}
