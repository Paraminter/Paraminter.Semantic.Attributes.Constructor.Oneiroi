namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Paraminter.Associators.Queries;
using Paraminter.Queries.Handlers;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Queries;
using Paraminter.Semantic.Attributes.Constructor.Queries.Handlers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        SemanticAttributeConstructorAssociator sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> Sut;

        public Fixture(
            IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> sut)
        {
            Sut = sut;
        }

        IQueryHandler<IAssociateArgumentsQuery<IAssociateSemanticAttributeConstructorData>, IInvalidatingAssociateSemanticAttributeConstructorQueryResponseHandler> IFixture.Sut => Sut;
    }
}
