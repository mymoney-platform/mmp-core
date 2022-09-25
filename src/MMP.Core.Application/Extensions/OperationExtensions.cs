using MMP.Core.Application.Commands;
using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;

namespace MMP.Core.Application.Extensions;

public static class OperationExtensions
{
    public static Operation ToDomain(this CreateOperationCommand command)
    {
        Operation op = new(command.AccountId, command.ExternalId, command.Value, (OperationType)command.OperationType,
            (OperationCategory)command.OperationCategory, command.Description);

        return op;
    }
}