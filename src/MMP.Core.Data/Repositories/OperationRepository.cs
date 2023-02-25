using Dapper;
using Dapper.Contrib.Extensions;
using MMP.Core.Data.Extensions;
using MMP.Core.Domain.Interfaces;
using MMP.Core.Domain.Models;
using MMP.Core.Shared;

namespace MMP.Core.Data.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly DatabaseFactory _databaseFactory;

    public OperationRepository(DatabaseFactory databaseFactory)
    {
        _databaseFactory = databaseFactory;
    }
    
    public async Task<Optional<Operation>> GetByAccountAndOperationId(Guid accountId, Guid operationId)
    {
        using var dbConnection = _databaseFactory.DbConnection;
        var operation = await dbConnection.QueryFirstOrDefaultAsync<Entities.Operation>(
            "SELECT OperationId, AccountId, Value, OperationType, OperationCategory, ExternalId, Description " +
            $"FROM {Entities.Entity.GetTableName(nameof(Entities.Operation))} "+
            "WHERE OperationId = @OperationId AND AccountId=@AccountId",
            new { OperationId = operationId, AccountId = accountId });

        return operation.ToDomain();
    }

    public async Task<Optional<Operation>> Save(Operation operation)
    {
        var entity = operation.ToEntity();
        entity.Id = 0; 
        
        using var dbConnection = _databaseFactory.DbConnection;
        var script =
            @"insert into mmpcore_db.""Operations"" (OperationId, AccountId, Value, Description, ExternalId, OperationCategory, OperationType)" +
            "values (@OperationId, @AccountId, @Value, @Description, @ExternalId, @OperationCategory, @OperationType);";

        await dbConnection.ExecuteAsync(script, entity);
        
        return entity.ToDomain();
    }
    
    public async Task<Optional<Operation>> Update(Operation operation)
    {
        var entity = operation.ToEntity();
        
        using var dbConnection = _databaseFactory.DbConnection;
        await dbConnection.UpdateAsync(entity);
        
        return entity.ToDomain();
    }
}