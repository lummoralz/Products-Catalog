namespace Products_Catalog.Models;

public class SignInResponse
{
    public string? Token { get; init; }
    public DateTime Expiration { get; init; }
}
