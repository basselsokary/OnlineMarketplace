namespace Domain.Enums;

public enum PaymentMethod : byte
{
    CreditCard = 1,
    PayPal,
    BankTransfer,
    Cash
}