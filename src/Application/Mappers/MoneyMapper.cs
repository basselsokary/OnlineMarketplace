using Application.DTOs;
using Domain.ValueObjects;

namespace Application.Mappers;

public static class MoneyMapper
{
    public static MoneyDto Map(this Money money)
    {
        return new(money.Amount, money.Currency);
    }

    public static Money Map(this MoneyDto dto)
    {
        return Money.Create(dto.Amount, dto.Currency);
    }
}
