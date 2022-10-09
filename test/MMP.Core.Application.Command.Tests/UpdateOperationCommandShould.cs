using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MMP.Core.Application.Commands;
using MMP.Core.Application.Interfaces;
using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;
using Moq;
using Xunit;

namespace MMP.Core.Application.Command.Tests;

public class UpdateOperationCommandShould
{
    [Fact]
    public async Task UpdateOperationWhenGivenValidRequest()
    {
        //Arrange
        var request = TestHelper.SetupUpdateOperation();
        var operationRepositoryMock = new Mock<IOperationCommandRepository>();
        var handler = new UpdateOperationCommandHandler(operationRepositoryMock.Object);
        
        operationRepositoryMock.Setup(op => op.Get(It.IsAny<Guid>()))
            .ReturnsAsync(Setup());
       
        //Action
        await handler.Handle(request, CancellationToken.None);

        //Assert
        operationRepositoryMock.Verify(o => o.Update(It.IsAny<Operation>()), Times.Once);
        operationRepositoryMock.Verify(o => o.Commit(), Times.Once);
    }
    
    public async Task ThrownExceptionWhenGivenInvalidOperationId()
    {
        //Arrange
        var request = TestHelper.SetupUpdateOperation();
        var operationRepositoryMock = new Mock<IOperationCommandRepository>();
        var handler = new UpdateOperationCommandHandler(operationRepositoryMock.Object);
        
        operationRepositoryMock.Setup(op => op.Get(It.IsAny<Guid>()))
            .ReturnsAsync((Operation)null!);
       
        //Action
        var action = async () => await handler.Handle(request, CancellationToken.None);

        //Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Resource not found");
        operationRepositoryMock.Verify(o => o.Update(It.IsAny<Operation>()), Times.Never);
        operationRepositoryMock.Verify(o => o.Commit(), Times.Never);
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