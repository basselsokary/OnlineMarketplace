using Domain.Common;
using Domain.Errors;
using SharedKernel.Models;
using static Domain.Constants.DomainConstants.Item;

namespace Domain.Entities;

public class CartItem : BaseAuditableEntity<Guid>
{
    private CartItem() : base(Guid.Empty) { }
    private CartItem(Guid productId, Guid cartId, int quantity)
        : base(Guid.NewGuid())
    {
        ProductId = productId;
        CartId = cartId;
        Quantity = quantity;
    }

    public Guid ProductId { get; private set; }
    public Guid CartId { get; private set; }

    public int Quantity { get; private set; } // Quantity of the product added to the cart

    internal static CartItem Create(Guid productId, Guid cartId, int quantity)
    {
        return new CartItem(productId, cartId, quantity);
    }

    internal Result Update(int quantity)
    {
        if (quantity > 0 && quantity <= MaxItemQuantity)
            return Result.Failure(CartErrors.NotValidQuantity(quantity));

        Quantity = quantity;

        return Result.Success();
    }
}
