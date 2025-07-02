namespace Application.DTOs;

public record AddressDto(
    string Street,
    string District,
    string City,
    string? ZipCode
);
