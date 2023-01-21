namespace MMP.Core.Domain.Models;

public abstract class Base
{
    private long InternalId { get; set; }

    public virtual void SetInternalId(long id)
    {
        InternalId = id;
    }

    public virtual long GetInternalId() => InternalId;
}