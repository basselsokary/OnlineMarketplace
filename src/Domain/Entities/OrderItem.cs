using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class OrderItem : BaseEntity<Guid>
{
    private OrderItem() : base(Guid.Empty) { }
    private OrderItem(Guid productId, int quantity, Money price, Guid orderId)
        : base(Guid.NewGuid())
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = price;
        OrderId = orderId;
    }

    public int Quantity { get; private set; }

    public Money UnitPrice { get; private set; } = null!;

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }
    
    internal static OrderItem Create(
        Guid productId,
        int quantity,
        Money price,
        Guid orderId = default)
    {
        return new OrderItem(productId, quantity, price, orderId);
    }
}