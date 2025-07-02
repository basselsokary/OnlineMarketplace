using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Payment : BaseAuditableEntity<Guid>
{
    private Payment() : base(Guid.Empty) { }
    private Payment(
        Money amount,
        DateTime? paymentDate,
        PaymentMethod method,
        PaymentStatus status,
        Guid orderId,
        Guid customerId) : base(Guid.NewGuid())
    {
        Amount = amount;
        PaymentDate = paymentDate;
        Method = method;
        Status = status;
        OrderId = orderId;
        CustomerId = customerId;
    }
    
    public Money Amount { get; private set; } = null!;

    public DateTime? PaymentDate { get; private set; }

    public PaymentMethod Method { get; private set; }

    public PaymentStatus Status { get; private set; }

    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = null!;

    public Guid CustomerId { get; private set; }

    public static Payment Create(
        Money amount,
        DateTime? paymentDate,
        PaymentMethod method,
        PaymentStatus status,
        Guid orderId,
        Guid customerId)
    {
        return new Payment(amount, paymentDate, method, status, orderId, customerId);
    }
}