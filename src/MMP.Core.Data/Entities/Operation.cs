using Dapper.Contrib.Extensions;
using MMP.Core.Domain.Enums;

namespace MMP.Core.Data.Entities;
public class Operation : Entity
{ 
    public string OperationId { get; set; }
    public string AccountId { get; set; }
    public string? ExternalId { get; set; }
    public decimal Value { get; set; }
    public OperationType OperationType { get; set; }
    public OperationCategory OperationCategory { get; set; }
    public string Description { get; set; }
}