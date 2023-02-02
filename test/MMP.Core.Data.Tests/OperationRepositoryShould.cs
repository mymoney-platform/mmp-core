using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MMP.Core.Data.Entities;
using MMP.Core.Data.Extensions;
using MMP.Core.Data.Repositories;
using MMP.Core.Domain.Enums;

namespace MMP.Core.Data.Tests;

public class OperationRepositoryShould
{
    [Fact]
    public async Task InsertOperation()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        var context = new DatabaseContext(options);
        var repository = new OperationRepository(context);

        var op = new Operation();
        op.AccountId = Guid.NewGuid();
        op.OperationId = Guid.NewGuid();
        op.Id = 1;
        op.Description = "description";
        op.ExternalId = Guid.NewGuid();
        op.OperationCategory = OperationCategory.Home;
        op.OperationType = OperationType.Expense;

        var operation = await repository.InsertOperation(op.ToDomain());
        
        operation.HasValue.Should().Be(true);
        operation.Value.GetInternalId().Should().Be(op.Id);
    }
}