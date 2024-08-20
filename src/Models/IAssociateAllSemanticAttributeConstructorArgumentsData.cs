namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

using Microsoft.CodeAnalysis;

using Paraminter.Models;

using System.Collections.Generic;

/// <summary>Represents data used to associate all semantic attribute constructor arguments with parameters.</summary>
public interface IAssociateAllSemanticAttributeConstructorArgumentsData
    : IAssociateAllArgumentsData
{
    /// <summary>The attribute constructor parameters.</summary>
    public abstract IReadOnlyList<IParameterSymbol> Parameters { get; }

    /// <summary>The semantic attribute constructor arguments.</summary>
    public abstract IReadOnlyList<TypedConstant> Arguments { get; }
}
