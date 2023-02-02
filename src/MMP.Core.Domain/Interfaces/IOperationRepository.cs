using MMP.Core.Domain.Models;
using MMP.Core.Shared;

namespace MMP.Core.Domain.Interfaces;

public interface IOperationRepository
{
    Task<Optional<Operation>> GetByAccountAndOperationId(Guid accountId, Guid operationId);
    Task<Optional<Operation>> InsertOperation(Operation operation);
}