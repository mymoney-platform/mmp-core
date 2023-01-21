using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;
using Operation = MMP.Core.Data.Entities.Operation;

namespace MMP.Core.Data.Extensions;
    
public static class OperationExtension
{
    public static MMP.Core.Domain.Models.Operation ToDomain(this Operation entity)
    {
        var model = new MMP.Core.Domain.Models.Operation(
            entity.OperationId, 
            entity.AccountId, 
            entity.Value, 
            (OperationType)entity.OperationType, 
            (OperationCategory)entity.OperationCategory, 
            entity.Description, 
            entity.ExternalId);
        
        model.SetInternalId(entity.Id);
        return model;
    }
}