using MMP.Core.Domain.Enums;

namespace MMP.Core.Domain.Models;

public class Operation
{
    public Operation(Guid operationId, Guid accountId, decimal value, OperationType operationType,
        OperationCategory operationCategory, string description, Guid? externalId = null)
    {
        OperationId = operationId;
        AccountId = accountId;
        Value = value;
        OperationType = operationType;
        OperationCategory = operationCategory;
        ExternalId = externalId;
        Description = (string.IsNullOrWhiteSpace(description) && OperationCategory == OperationCategory.None)
            ? throw new ArgumentNullException(nameof(description))
            : description;
    }

    public Guid OperationId { get; }
    public Guid AccountId { get; }
    public Guid? ExternalId { get; }
    public decimal Value { get; private set; }
    public OperationType OperationType { get; private set; }
    public OperationCategory OperationCategory { get; private set; }
    public string Description { get; private set; }

    public void ChangeOperationValue(decimal value)
    {
        if (OperationType != OperationType.Deposit)
        {
            Value = value * -1;
        }
        else
        {
            Value = value;
        }
    }

    public void ChangeDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description) && OperationCategory == OperationCategory.None)
            throw new Exception("Description can not be empty when category is none");

        Description = description;
    }

    public void ChangeOperationCategory(OperationCategory operationCategory)
    {
        if (string.IsNullOrWhiteSpace(Description) && operationCategory == OperationCategory.None)
            throw new Exception("Operation category can not be none when description is empty");
            

        OperationCategory = operationCategory;
    }

    public void ChangeOperationType(OperationType operationType)
    {
        if (operationType == OperationType.Investment && OperationCategory != OperationCategory.Investment)
            throw new Exception("Operation category must be a Investment");

        if (operationType != OperationType.Deposit)
            Value = Value * -1;

        OperationType = operationType;
    }
}