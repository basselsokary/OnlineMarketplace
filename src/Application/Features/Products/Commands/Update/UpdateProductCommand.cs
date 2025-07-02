using System;
using Application.DTOs;

namespace Application.Features.Products.Commands.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    MoneyDto Price,
    int StockQuantity,
    List<Guid> CategoryIds,
    string? ImageUrl
) : ICommand;
