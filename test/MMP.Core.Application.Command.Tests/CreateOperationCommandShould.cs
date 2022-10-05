using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MMP.Core.Application.Commands;
using MMP.Core.Application.Interfaces;
using MMP.Core.Domain.Models;
using Moq;
using Xunit;

namespace MMP.Core.Application.Command.Tests;

public class CreateOperationCommandShould
{
    [Fact]
    public async Task CreateOperationWhenGivenValidRequest()
    {
        //Arrange
        var request = TestHelper.SetupOperation();
        var operationRepositoryMock = new Mock<IOperationRepository>();
        var handler = new CreateOperationCommandHandler(operationRepositoryMock.Object);

        //Action
        var operationId = await handler.Handle(request, CancellationToken.None);

        //Assert
        operationId.Should().NotBeEmpty();
        operationRepositoryMock.Verify(o => o.Save(It.IsAny<Operation>()), Times.Once);
        operationRepositoryMock.Verify(o => o.Commit(), Times.Once);
    }
}