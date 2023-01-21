namespace MMP.Core.Data.Entities;

public class Operation : Entity
{ 
    public Guid OperationId { get; set; }
    public Guid AccountId { get; set; }
    public Guid? ExternalId { get; }
    public decimal Value { get; set; }
    public int OperationType { get; set; }
    public int OperationCategory { get; set; }
    public string Description { get; set; }
}