using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Infrustructure;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<CartItem> CartItems { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Category> Categories { get;  }
    DbSet<CategoryProduct> CategoryProducts { get; set; }
    DbSet<Product> Products { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Review> Reviews { get; }
    DbSet<Order> Orders { get; }
    DbSet<Cart> Carts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
