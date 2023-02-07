using Microsoft.AspNetCore.Identity;

namespace Products_Catalog.Models;

public class User : IdentityUser
{
    public List<Product>? Products { get; set; }
}
