using System.Linq.Expressions;
using Application.DTOs;
using Domain.ValueObjects;

namespace Application.Mappers;

public static class AddressMapper
{
    public static AddressDto Map(this Address address)
    {
        return new(address.Street, address.District, address.City, address.ZipCode);
    }

    public static Address Map(this AddressDto dto)
    {
        return Address.Create(dto.Street, dto.District, dto.City, dto.ZipCode);
    }

    public static Expression<Func<Address, AddressDto>> Map()
    {
        return address => new AddressDto(
            address.Street,
            address.District,
            address.City,
            address.ZipCode);
    }

}
