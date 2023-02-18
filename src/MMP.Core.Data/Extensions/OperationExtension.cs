using MMP.Core.Domain.Enums;
using MMP.Core.Domain.Models;
using Operation = MMP.Core.Data.Entities.Operation;

namespace MMP.Core.Data.Extensions;
    
public static class OperationExtension
{
    public static MMP.Core.Domain.Models.Operation ToDomain(this Operation? entity)
    {
        if (entity is null)
            return default!;
        
        var model = new MMP.Core.Domain.Models.Operation(
            entity.OperationId, 
            entity.AccountId, 
            entity.Value, 
            entity.OperationType, 
            entity.OperationCategory, 
            entity.Description, 
            entity.ExternalId);
        
        model.SetInternalId(entity.Id);
        return model;
    }

    public static Operation ToEntity(this Domain.Models.Operation? model)
    {
        if (model is null)
            return default!;

        var entity = new Operation
        {
            OperationId = model.OperationId,
            AccountId = model.AccountId,
            Value = model.Value,
            OperationType = model.OperationType,
            OperationCategory = model.OperationCategory,
            Description = model.Description,
            ExternalId = model.ExternalId,
            Id = model.GetInternalId()
        };
        
        return entity;
    }
}