using MMP.Core.Domain.Models;

namespace MMP.Core.Application.Interfaces;

public interface IOperationRepository
{
    Task Save(Operation operation);
    Task Delete(Guid operationId);
}