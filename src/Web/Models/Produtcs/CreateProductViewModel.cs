using System.ComponentModel.DataAnnotations;

namespace Web.Models.Products;

public class CreateProductViewModel
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal PriceAmount { get; set; }

    [Required]
    public string PriceCurrency { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public List<Guid> SelectedCategoryIds { get; set; } = [];
}

