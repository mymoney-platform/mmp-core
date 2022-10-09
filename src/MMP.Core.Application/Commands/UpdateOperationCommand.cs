using MediatR;
using MMP.Core.Shared.Enums;

namespace MMP.Core.Application.Commands;

public class UpdateOperationCommand : IRequest
{
    public Guid OperationId { get; set; }
    public decimal Value { get; set; }
    public OperationType OperationType { get; set; }
    public OperationCategory OperationCategory { get; set; }
    public string Description { get; set; }
}