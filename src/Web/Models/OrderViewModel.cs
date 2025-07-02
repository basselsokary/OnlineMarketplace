using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.Models;

public class OrderViewModel
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [StringLength(100, ErrorMessage = "Street can't be longer than 100 characters")]
    public string Street { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City can't be longer than 100 characters")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "District is required")]
    [StringLength(100, ErrorMessage = "District can't be longer than 100 characters")]
    public string District { get; set; } = null!;

    [DisplayName("Postal Code (Optionally)")]
    [Range(10000, int.MaxValue, ErrorMessage = "Zip Code must be at least 5 digits")]
    public int? ZipCode { get; set; }

    [Required(ErrorMessage = "Payment Method is required")]
    public PaymentMethod PaymentMethod { get; set; }

    // +
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must at least be equal 1")]
    public int Quantity { get; set; }
}
