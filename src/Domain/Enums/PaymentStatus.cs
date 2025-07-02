namespace Domain.Enums;

public enum PaymentStatus : byte
{
    Pending = 1,
    Completed,
    Failed,
    Refunded
}