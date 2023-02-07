using System.ComponentModel.DataAnnotations;

namespace Products_Catalog.Models;

public class ProductRequest
{
    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }
    public decimal UnitPrice { get; set; } = decimal.Zero;
    public uint Amount { get; set; } = 0;
}
