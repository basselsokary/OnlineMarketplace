using Domain.Common;
using Domain.ValueObjects;
using SharedKernel.Models;

namespace Domain.Entities;

public class Customer : BaseAuditableEntity<Guid>
{
    private Customer() : base(Guid.Empty) { }
    private Customer(string fullName, Address address, string userId, int? age)
        : base(Guid.NewGuid())
    {
        FullName = fullName;
        Address = address;
        Age = age;
        UserId = userId;
    }

    public string FullName { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public int? Age { get; private set; }

    public string UserId { get; private set; } = null!;

    public static Customer Create(string fullName, Address address, string userId, int? age = null)
    {
        return new Customer(fullName, address, userId, age);
    }

    public Result Update(string fullName, Address address, int? age)
    {
        FullName = fullName;
        Address = address;
        if (age != null)
            Age = age;

        return Result.Success();
    }
}
