using Application.DTOs;

namespace Application.Features.Customers.Queries.Profile;

public record GetProfileQueryResponse(
    string FullName,
    int? Age,
    AddressDto Address,
    DateTime DateJoined
);