using System;
using System.Threading;
using System.Threading.Tasks;
using MMP.Core.Application.Commands;
using MMP.Core.Application.Interfaces;
using Moq;
using Xunit;

namespace MMP.Core.Application.Command.Tests;

public class DeleteOperationCommandShould
{
    [Fact]
    public async Task DeleteOperationWhenGivenValidRequest()
    {
        //Arrange
        var request = new DeleteOperationCommand { OperationId = Guid.NewGuid() };
        var operationRepositoryMock = new Mock<IOperationRepository>();
        var handler = new DeleteOperationCommandHandler(operationRepositoryMock.Object);

        //Action
        await handler.Handle(request, CancellationToken.None);

        //Assert
        operationRepositoryMock.Verify(o => o.Delete(It.IsAny<Guid>()), Times.Once);
    }
}