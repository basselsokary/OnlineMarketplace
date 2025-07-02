using System.ComponentModel.DataAnnotations;

namespace Web.Models.Categories;

public class EditeCategoryViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(25)]
    [MinLength(3)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }
}
