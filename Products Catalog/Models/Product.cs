using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Products_Catalog.Models;

public class Product
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }
    public decimal UnitPrice { get; set; } = decimal.Zero;
    public uint Amount { get; set; } = 0;

    public User? User { get; set; }
    public string? UserId { get; set; }
}
