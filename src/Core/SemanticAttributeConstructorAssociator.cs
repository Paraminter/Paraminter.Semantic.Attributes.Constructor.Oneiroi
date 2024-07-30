namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Paraminter.Associators.Queries;
using Paraminter.Queries.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Common;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Handlers;

using System;

/// <summary>Associates semantic attribute constructor arguments.</summary>
public sealed class SemanticAttributeConstructorAssociator
    : IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler>
{
    /// <summary>Instantiates a <see cref="SemanticAttributeConstructorAssociator"/>, associating semantic attribute constructor arguments.</summary>
    public SemanticAttributeConstructorAssociator() { }

    void IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler>.Handle(
        IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData> query,
        IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler queryResponseHandler)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        if (queryResponseHandler is null)
        {
            throw new ArgumentNullException(nameof(queryResponseHandler));
        }

        if (query.Data.Parameters.Count != query.Data.Arguments.Count)
        {
            queryResponseHandler.Invalidator.Handle(InvalidateQueryResponseCommand.Instance);

            return;
        }

        for (var i = 0; i < query.Data.Parameters.Count; i++)
        {
            var parameter = query.Data.Parameters[i];
            var argument = query.Data.Arguments[i];

            var command = new AddSemanticAttributeConstructorAssociationCommand(parameter, argument);

            queryResponseHandler.AssociationCollector.Handle(command);
        }
    }
}
