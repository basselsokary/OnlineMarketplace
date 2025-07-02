using Application.DTOs;

namespace Application.Features.Products.Commands.Create;

public record CreateProductCommand(
    string Name,
    string? Description,
    MoneyDto Price,
    int StockQuantity,
    List<Guid> SelectedCategoryIds,
    string? ImageUrl
) : ICommand<Guid>;
