namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Paraminter.Associators.Queries;
using Paraminter.Queries.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Collectors;

internal interface IFixture
{
    public abstract IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseCollector> Sut { get; }
}
