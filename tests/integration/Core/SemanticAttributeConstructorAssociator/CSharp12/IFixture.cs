namespace Paraminter.Semantic.Attributes.Constructor.Oneiroi;

using Moq;

using Paraminter.Arguments.Semantic.Attributes.Constructor.Models;
using Paraminter.Associators.Commands;
using Paraminter.Commands.Handlers;
using Paraminter.Parameters.Method.Models;
using Paraminter.Semantic.Attributes.Constructor.Oneiroi.Models;

internal interface IFixture
{
    public abstract ICommandHandler<IAssociateArgumentsCommand<IAssociateSemanticAttributeConstructorData>> Sut { get; }

    public abstract Mock<ICommandHandler<IRecordArgumentAssociationCommand<IMethodParameter, ISemanticAttributeConstructorArgumentData>>> RecorderMock { get; }
    public abstract Mock<ICommandHandler<IInvalidateArgumentAssociationsRecordCommand>> InvalidatorMock { get; }
}
