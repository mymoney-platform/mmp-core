using MediatR;

namespace MMP.Core.Application.Commands;

public class DeleteOperationCommand : IRequest
{
    public Guid OperationId { get; set; }
}