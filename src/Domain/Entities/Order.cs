using Domain.Common;
using Domain.Enums;
using Domain.Errors;
using Domain.ValueObjects;
using SharedKernel.Models;

namespace Domain.Entities;

public class Order : BaseAuditableEntity<Guid>
{
    private Order() : base(Guid.Empty) { }
    private Order(Guid customerId, string userId, Address address) : base(Guid.NewGuid())
    {
        CustomerId = customerId;
        Address = address;
        UserId = userId;

        Status = OrderStatus.Pending;
        Fulfilled = null;
    }

    public DateTime? Fulfilled { get; private set; }

    public DateTime? ShippedAt { get; private set; }

    public OrderStatus Status { get; private set; }

    public Guid CustomerId { get; private set; }

    public string UserId { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public Money TotalAmount { get; private set; } = Money.Empty;

    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public static Order Create(
        Guid customerId,
        string userId,
        Address address)
    {
        return new Order(customerId, userId, address);
    }

    public Result AddOrderItem(Guid productId, int quantity, Money price)
    {
        if (_orderItems.Any(x => x.ProductId == productId))
        {
            return Result.Failure(OrderErrors.ItemAlreadyExists(productId));
        }

        var orderItem = OrderItem.Create(productId, quantity, price);
        _orderItems.Add(orderItem);

        UpdateTotalAmount(orderItem.UnitPrice);

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(["Order cannot be cancelled unless it is pending."]);
        }

        Status = OrderStatus.Cancelled;
        Fulfilled = DateTime.UtcNow;

        return Result.Success();
    }

    public Result Fulfill(DateTime orderFulfilled)
    {
        if (Status == OrderStatus.Delivered)
        {
            return Result.Failure(OrderErrors.AlreadyFulfilled(Id));
        }

        Status = OrderStatus.Delivered;
        if (orderFulfilled == default)
            Fulfilled = DateTime.UtcNow;
        else
            Fulfilled = orderFulfilled;

        return Result.Success();
    }

    private void UpdateTotalAmount(Money totalPrice)
    {
        if (TotalAmount == Money.Empty)
        {
            TotalAmount = totalPrice;
            return;
        }

        TotalAmount.Add(totalPrice);
    }
}
