using Domain.Common;

namespace Domain.ValueObjects;

// Owned Table to Order
public class Address : ValueObject
{
    private Address() { }
    private Address(string street, string city, string district, string? zipCode)
    {
        Street = street;
        City = city;
        District = district;
        ZipCode = zipCode;
    }
    
    // [Required(ErrorMessage = "Street is required")]
    // [StringLength(100, ErrorMessage = "Street can't be longer than 100 characters")]
    public string Street { get; private set; } = null!;

    // [Required(ErrorMessage = "City is required")]
    // [StringLength(100, ErrorMessage = "City can't be longer than 100 characters")]
    public string City { get; private set; } = null!;

    // [Required(ErrorMessage = "District is required")]
    // [StringLength(100, ErrorMessage = "District can't be longer than 100 characters")]
    public string District { get; private set; } = null!;

    // [DisplayName("Postal Code (Optionally)")]
    // [Range(10000, int.MaxValue, ErrorMessage = "Postal Code must is wrong")]
    public string? ZipCode { get; private set; }

    public static Address Create(
        string street,
        string district,
        string city,
        string? zipCode = null)
    {
        return new Address(street, city, district, zipCode);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return District;
    }
}
