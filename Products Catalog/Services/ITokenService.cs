using Microsoft.AspNetCore.Identity;
using Products_Catalog.Models;

namespace Products_Catalog.Services;

public interface ITokenService
{
    SignInResponse CreateToken(IdentityUser user);
}
