using System.Resources;
using MediatR;

namespace MMP.Core.Application.Commands;

public class ReverseOperationCommand : IRequest
{
    public Guid OperationId { get; set; }
}