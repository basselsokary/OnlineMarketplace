
namespace Domain.Entities;

public class CategoryProduct
{
    private CategoryProduct() { }
    private CategoryProduct(Guid categoryId, string categoryName, Guid  productId)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;

        ProductId = productId;
    }

    public Guid CategoryId { get; private set; }
    public Guid ProductId { get; private set; }

    public string CategoryName { get; private set; } = null!;

    internal static CategoryProduct Create(Guid categoryId, string CategoryName, Guid productId = default)
    {
        return new CategoryProduct(categoryId, CategoryName, productId);
    }

    internal static List<CategoryProduct> CreateMany(List<(Guid Id, string Name)> categories)
    {
        return categories.Select(category => Create(category.Id, category.Name)).ToList();
    }
}
