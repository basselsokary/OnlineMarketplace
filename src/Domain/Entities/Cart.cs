using Domain.Common;
using Domain.Errors;
using SharedKernel.Models;

namespace Domain.Entities;

public class Cart : BaseAuditableEntity<Guid>
{
    private Cart() : base(Guid.Empty) { }
    private Cart(Guid customerId, string userId) : base(Guid.NewGuid())
    {
        CustomerId = customerId;
        UserId = userId;
    }

    public Guid CustomerId { get; private set; }

    public string UserId { get; private set; } = null!;

    private readonly List<CartItem> _cartItems = [];
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public static Cart Create(Guid customerId, string userId)
    {
        return new Cart(customerId, userId);
    }

    public Result AddItem(Guid productId, int quantity)
    {
        if (_cartItems.Any(ci => ci.ProductId == productId))
            return Result.Failure(CartErrors.ItemProductNotFound(productId));

        var item = CartItem.Create(productId, Id, quantity);

        _cartItems.Add(item);

        return Result.Success();
    }

    public Result UpdateItem(Guid itemId, int quantity)
    {
        var item = _cartItems.FirstOrDefault(ci => ci.Id == itemId);
        if (item == null)
            return Result.Failure(CartErrors.ItemNotFound(itemId));

        item.Update(quantity);

        return Result.Success();
        
    }

    public Result RemoveItem(Guid itemId)
    {
        var item = _cartItems.FirstOrDefault(ci => ci.Id == itemId);
        if (item == null)
            return Result.Failure(CartErrors.ItemNotFound(itemId));

        _cartItems.Remove(item);

        return Result.Success();
    }

    public void Clear()
    {
        _cartItems.Clear();
    }
}
