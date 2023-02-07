using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products_Catalog.Models;
using Products_Catalog.Services;

namespace Products_Catalog.Controllers;

[ApiController, Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private const string SIGNIN_ERROR_MESSAGE = "Invalid credentials!";
    public const string SIGNUP_ERROR_MESSAGE = "Not valid user data!";

    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public UsersController(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest signIn)
    {
        if (!ModelState.IsValid)
            return Error(SIGNIN_ERROR_MESSAGE);

        var user = await _userManager.FindByEmailAsync(signIn.Email!);
        if (user == null)
            return Error(SIGNIN_ERROR_MESSAGE);

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, signIn.Password!);
        if (!isPasswordValid)
            return Error(SIGNIN_ERROR_MESSAGE);

        var token = _tokenService.CreateToken(user);
        return Ok(token);
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUp(SignUpRequest signUp)
    {
        if (!ModelState.IsValid)
            return Error(SIGNUP_ERROR_MESSAGE);

        var newUser = new User { Email = signUp.Email, UserName = signUp.Email };
        var result = await _userManager.CreateAsync(newUser, signUp.Password!);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }

    private ActionResult Error(string message) => BadRequest(new { Error = message });
}
