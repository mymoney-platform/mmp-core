using MMP.Core.Domain.Models;

namespace MMP.Core.Application.Interfaces;

public interface IOperationRepository
{
    Task<Operation> Get(Guid operationId);
    Task Save(Operation operation);
    Task Commit();
}