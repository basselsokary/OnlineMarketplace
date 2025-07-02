namespace Web.Models.Products;

public class EditProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<Guid> SelectedCategoryIds { get; set; } = null!;
    public string? ImageUrl { get; set; } = string.Empty;
}

