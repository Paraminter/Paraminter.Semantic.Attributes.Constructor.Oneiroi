namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Paraminter.Associators.Queries;
using Paraminter.Queries.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Collectors;

using System;

/// <summary>Associates semantic attribute constructor arguments.</summary>
public sealed class SemanticAttributeConstructorAssociator
    : IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector>
{
    /// <summary>Instantiates a <see cref="SemanticAttributeConstructorAssociator"/>, associating semantic attribute constructor arguments.</summary>
    public SemanticAttributeConstructorAssociator() { }

    void IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector>.Handle(
        IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData> query,
        IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector queryResponseCollector)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        if (queryResponseCollector is null)
        {
            throw new ArgumentNullException(nameof(queryResponseCollector));
        }

        if (query.Data.Parameters.Count != query.Data.Arguments.Count)
        {
            queryResponseCollector.Invalidator.Invalidate();

            return;
        }

        for (var i = 0; i < query.Data.Parameters.Count; i++)
        {
            var parameter = query.Data.Parameters[i];
            var argumentData = query.Data.Arguments[i];

            queryResponseCollector.Associations.Add(parameter, argumentData);
        }
    }
}
