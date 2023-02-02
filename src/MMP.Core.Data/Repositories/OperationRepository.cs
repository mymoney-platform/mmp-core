using Microsoft.EntityFrameworkCore;
using MMP.Core.Data.Extensions;
using MMP.Core.Domain.Interfaces;
using MMP.Core.Domain.Models;
using MMP.Core.Shared;

namespace MMP.Core.Data.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly DatabaseContext _context;

    public OperationRepository(DatabaseContext context) 
    {
        _context = context;
    }
    
    public async Task<Optional<Operation>> GetByAccountAndOperationId(Guid accountId, Guid operationId)
    {
        var operation = await _context.Operations
            .FirstOrDefaultAsync(op => op.AccountId == accountId && op.OperationId == operationId);

        Optional<Operation> optionalResult = operation.ToDomain();
        
        return optionalResult;
    }

    public async Task<Optional<Operation>> InsertOperation(Operation operation)
    {
        var entity = operation.ToEntity();
        entity.Id = 0; 
        
        await _context.Operations.AddAsync(entity);
        
        Optional<Operation> optionalResult = entity.ToDomain();
        return optionalResult;
    }
}