namespace Products_Catalog.Models;

public class JwtSettings
{
    public string? Issuer { get; set; }
    public string? Subject { get; set; }
    public string? Key { get; set; }
    public string? Audience { get; set; }
    public int ExpirationHours { get; set; }
}
