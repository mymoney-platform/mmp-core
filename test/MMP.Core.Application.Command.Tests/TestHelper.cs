using System;
using MMP.Core.Application.Commands;
using MMP.Core.Shared.Enums;

namespace MMP.Core.Application.Command.Tests;

public static class TestHelper
{
    public static CreateOperationCommand SetupOperation()
    {
        var accountId = Guid.NewGuid();
        var externalId = Guid.NewGuid();
        decimal value = 1000;
        var operationType = OperationType.Deposit;
        var operationCategory = OperationCategory.Home;
        var description = "some description";

        return new CreateOperationCommand
        {
            AccountId = accountId,
            ExternalId = externalId,
            Value = value,
            Description = description,
            OperationCategory = OperationCategory.Clothes,
            OperationType = OperationType.Expense
        };
    }
}