using FluentAssertions;
using MMP.Core.Data.Repositories;
using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;
using Npgsql;

namespace MMP.Core.Data.Tests;

public class OperationRepositoryShould : IClassFixture<PostgresqlFixture>
{
    private readonly PostgresqlFixture _postgresqlFixture;
    private readonly DatabaseFactory _databaseFactory;
    
    public OperationRepositoryShould(PostgresqlFixture postgresqlFixture)
    {
        _postgresqlFixture = postgresqlFixture;
        
        var connectionString = _postgresqlFixture.GetConnectionString();
        _databaseFactory = new DatabaseFactory(connectionString);
    }
    [Fact]
    public async Task AddNewOperation()
    {
        var repo = new OperationRepository(_databaseFactory);
        Operation op =
            new(Guid.NewGuid(), Guid.NewGuid(), 10, OperationType.Deposit, OperationCategory.Clothes, "Test");

        var sut = await repo.Save(op);
        sut.HasValue.Should().Be(true);
        sut.Value.GetInternalId().Should().BeGreaterOrEqualTo(0);
    }
}