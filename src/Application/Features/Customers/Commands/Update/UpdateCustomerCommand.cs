using Application.DTOs;

namespace Application.Features.Customers.Commands.Update;

public record UpdateCustomerCommand(
    string FullName,
    AddressDto Address,
    int? Age
) : ICommand;
