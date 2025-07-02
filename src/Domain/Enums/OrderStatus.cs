namespace Domain.Enums;

public enum OrderStatus : byte
{
    Pending = 1,
    Confirmed,
    Shipped,
    Delivered,
    Cancelled
}
