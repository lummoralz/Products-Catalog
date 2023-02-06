using System.ComponentModel.DataAnnotations;

namespace Products_Catalog.Models;

public class SignUpRequest
{
    [Required, DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required, DataType(DataType.Password), Compare("Password")]
    public string? ConfirmPassword { get; set; }
}
