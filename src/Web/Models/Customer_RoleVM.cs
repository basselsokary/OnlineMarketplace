using Domain.Entities;

namespace Web.Models;

public class Customer_RoleVM
{
    Customer Customer { get; set; }
    List<string> Roles { get; set; }
}
