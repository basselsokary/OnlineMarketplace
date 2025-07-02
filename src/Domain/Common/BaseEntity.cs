namespace Domain.Common;

public abstract class BaseEntity<T>
{
    public T Id { get; protected set; }

    protected BaseEntity(T id)
    {
        Id = id;
    }
}
