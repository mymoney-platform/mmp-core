using System;
using System.Threading;
using System.Threading.Tasks;
using MMP.Core.Application.Commands;
using MMP.Core.Application.Interfaces;
using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;
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
        operationRepositoryMock.Setup(op => op.Get(It.IsAny<Guid>()))
            .ReturnsAsync(Setup());

        var handler = new DeleteOperationCommandHandler(operationRepositoryMock.Object);

        //Action
        await handler.Handle(request, CancellationToken.None);

        //Assert
        operationRepositoryMock.Verify(o => o.Get(It.IsAny<Guid>()), Times.Once);
        operationRepositoryMock.Verify(o => o.Save(It.IsAny<Operation>()), Times.Once);
        operationRepositoryMock.Verify(o => o.Commit(), Times.Once);
    }

    private Operation Setup()
    {
        var operationId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var externalId = Guid.NewGuid();
        decimal value = 1000;
        var operationType = OperationType.Deposit;
        var operationCategory = OperationCategory.Home;
        var description = "some description";

        return new(operationId, accountId, value, operationType, operationCategory, description, externalId);
    }
}