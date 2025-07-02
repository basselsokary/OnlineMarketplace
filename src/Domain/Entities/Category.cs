using Domain.Common;

namespace Domain.Entities;

public class Category : BaseAuditableEntity<Guid>
{
    private Category() : base(Guid.Empty) { }
    private Category(string name, Guid productId, string? description = null)
        : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public static Category Create(string name, string? description = null, Guid productId = default)
    {
        return new Category(name, productId, description);
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }
}