using MMP.Core.Domain.Models;

namespace MMP.Core.Application.Interfaces;

public interface IOperationCommandRepository
{
    Task<Operation> Get(Guid operationId);
    Task Save(Operation operation);
    Task Update(Operation operation);
    Task Commit();
}