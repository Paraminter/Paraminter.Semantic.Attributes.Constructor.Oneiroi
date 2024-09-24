namespace Paraminter.Associating.Semantic.Attributes.Constructor.Oneiroi.Errors.Commands;

internal sealed class HandleDifferentNumberOfArgumentsAndParametersCommand
    : IHandleDifferentNumberOfArgumentsAndParametersCommand
{
    public static IHandleDifferentNumberOfArgumentsAndParametersCommand Instance { get; } = new HandleDifferentNumberOfArgumentsAndParametersCommand();

    private HandleDifferentNumberOfArgumentsAndParametersCommand() { }
}
