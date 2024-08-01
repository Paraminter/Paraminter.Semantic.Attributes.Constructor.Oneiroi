namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Commands;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents data used to associate semantic attribute constructor arguments.</summary>
public interface IAssociateSemanticAttributeConstructorData
{
    /// <summary>The attribute constructor parameters.</summary>
    public abstract IReadOnlyList<IParameterSymbol> Parameters { get; }

    /// <summary>The semantic attribute constructor arguments.</summary>
    public abstract IReadOnlyList<TypedConstant> Arguments { get; }
}
