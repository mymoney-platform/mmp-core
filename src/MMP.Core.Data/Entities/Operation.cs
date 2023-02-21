using MMP.Core.Domain.Enums;

namespace MMP.Core.Data.Entities;

public class Operation : Entity
{ 
    public Guid OperationId { get; set; }
    public Guid AccountId { get; set; }
    public Guid? ExternalId { get; set; }
    public decimal Value { get; set; }
    public OperationType OperationType { get; set; }
    public OperationCategory OperationCategory { get; set; }
    public string Description { get; set; }
}