using MMP.Core.Domain.Models;
using MMP.Core.Shared;

namespace MMP.Core.Domain.Interfaces;

public interface IOperationRepository
{
    Task<Optional<Operation>> GetByAccountAndOperationId(Guid accountId, Guid operationId);
    Task<Optional<Operation>> Save(Operation operation);
    Task<Optional<Operation>> Update(Operation operation);
}