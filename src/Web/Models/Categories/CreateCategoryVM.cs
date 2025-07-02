using System.ComponentModel.DataAnnotations;

namespace Web.Models.Categories;

public class CreateCategoryViewModel
{
    [Required]
    [MaxLength(25)]
    [MinLength(3)]
     public string Name { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }
}
