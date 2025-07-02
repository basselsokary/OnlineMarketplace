namespace Domain.Common;

public abstract class BaseAuditableEntity<T> : BaseEntity<T>
{
    protected BaseAuditableEntity(T Id) : base(Id)
    {
        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public DateTime LastModifiedAt { get; protected set; }
    public string? LastModifiedBy { get; protected set; }

    public virtual void UpdateCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }

    public virtual void UpdateLastModifiedBy(string modifiedBy)
    {
        LastModifiedBy = modifiedBy;
    }
}
