using System.ComponentModel.DataAnnotations;

namespace Products_Catalog.Models;

public class SignInRequest
{
    [Required, DataType(DataType.EmailAddress)] 
    public string? Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }
}
