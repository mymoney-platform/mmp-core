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

    public static OperationCategory ToDomain(this MMP.Core.Shared.Enums.OperationCategory operationCategory)
    {
        return operationCategory switch
        {
            Shared.Enums.OperationCategory.Clothes => OperationCategory.Clothes,
            Shared.Enums.OperationCategory.None => OperationCategory.None,
            Shared.Enums.OperationCategory.Meal => OperationCategory.Meal,
            Shared.Enums.OperationCategory.Supermarket => OperationCategory.Supermarket,
            Shared.Enums.OperationCategory.Home => OperationCategory.Home,
            Shared.Enums.OperationCategory.Health => OperationCategory.Health,
            Shared.Enums.OperationCategory.Investment => OperationCategory.Investment,
            Shared.Enums.OperationCategory.Travel => OperationCategory.Travel,
            Shared.Enums.OperationCategory.Internet => OperationCategory.Internet,
            Shared.Enums.OperationCategory.Pet => OperationCategory.Pet,
            _ => throw new ArgumentOutOfRangeException(nameof(operationCategory), operationCategory, null)
        };
    }
    public static OperationType ToDomain(this MMP.Core.Shared.Enums.OperationType operationType)
    {
        return operationType switch
        {
            Shared.Enums.OperationType.Deposit => OperationType.Deposit,
            Shared.Enums.OperationType.Expense => OperationType.Expense,
            Shared.Enums.OperationType.Investment => OperationType.Investment,
            Shared.Enums.OperationType.Withdraw => OperationType.Withdraw,
            Shared.Enums.OperationType.Reverse => OperationType.Reverse,
            
            _ => throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null)
        };
    }
}