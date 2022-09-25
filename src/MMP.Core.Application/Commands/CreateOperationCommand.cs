using MediatR;
using MMP.Core.Shared.Enums;

namespace MMP.Core.Application.Commands;

public class CreateOperationCommand : IRequest<Guid>
{
    public Guid AccountId { get; set; }
    public Guid? ExternalId { get; set; }
    public decimal Value { get; set; }
    public OperationType OperationType { get; set; }
    public OperationCategory OperationCategory { get; set; }
    public string Description { get; set; }
}