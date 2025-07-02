using Domain.Common;
using Domain.Errors;
using Domain.ValueObjects;
using SharedKernel.Models;
using static Domain.Constants.DomainConstants.Product;

namespace Domain.Entities;

public class Product : BaseAuditableEntity<Guid>
{
    private Product() : base(Guid.Empty) { }
    private Product(
        string name,
        string? description,
        int unitsInStock,
        Money price,
        string? imageUrl,
        List<CategoryProduct> categories) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        UnitsInStock = unitsInStock;
        Price = price;
        ImageURL = imageUrl;

        _categoryProducts = categories;
    }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public int UnitsInStock { get; private set; }

    public Money Price { get; private set; } = null!;

    public string? ImageURL { get; private set; }

    private readonly List<CategoryProduct> _categoryProducts = [];
    public ICollection<CategoryProduct> CategoryProducts => _categoryProducts.AsReadOnly();

    public IReadOnlyCollection<Review> Reviews { get; private set; } = null!;

    public static Product Create(
        string name,
        string? description,
        int unitsInStock,
        Money price,
        string? imageUrl,
        List<(Guid Id, string Name)> categories)
    {
        var categoryProductItems = CategoryProduct.CreateMany(categories);
        return new Product(name, description, unitsInStock, price, imageUrl, categoryProductItems);
    }

    public void Update(
        string name,
        string? description,
        int unitsInStock,
        Money price,
        string? imageUrl,
        List<(Guid Id, string Name)> categories)
    {
        Name = name;
        Description = description;
        UnitsInStock = unitsInStock;
        Price = price;
        ImageURL = imageUrl;

        var categoryProductItems = CategoryProduct.CreateMany(categories);
        _categoryProducts.AddRange(categoryProductItems);
    }

    public Result Purchsased(int quantity)
    {
        if (quantity <= 0)
            return Result.Failure(ProductErrors.InvalidQuantity);

        if (UnitsInStock < quantity)
            return Result.Failure(ProductErrors.InsufficientStock(Id, UnitsInStock));

        UnitsInStock -= quantity;

        return Result.Success();
    }

    public Result AddCategory(Guid categoryId, string categoryName)
    {
        if (_categoryProducts.Count >= MaxCategoriesPerProduct)
            return Result.Failure(ProductErrors.TooManyCategories(MaxCategoriesPerProduct));

        if (_categoryProducts.Any(c => c.CategoryId == categoryId))
            return Result.Failure(ProductErrors.CategoryAlreadyExists(categoryId));

        var categoryProductItem = CategoryProduct.Create(categoryId, categoryName, Id);
        _categoryProducts.Add(categoryProductItem);

        return Result.Success();
    }

    public Result RemoveCategory(Guid categoryId)
    {
        var categoryProductItem = _categoryProducts.FirstOrDefault(c => c.CategoryId == categoryId);
        if (categoryProductItem == null)
            return Result.Failure(CategoryErrors.NotFound(categoryId));

        _categoryProducts.Remove(categoryProductItem);

        return Result.Success();
    }

    public void ClearCategories()
    {
        _categoryProducts.Clear();
    }
}