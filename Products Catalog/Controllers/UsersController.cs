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
    private readonly ILogger<UsersController> _logger;

    public UsersController(UserManager<User> userManager, ITokenService tokenService, ILogger<UsersController> logger)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _logger = logger;
    }

    [HttpPost("signin")]
    public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest signIn)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogDebug(SIGNIN_ERROR_MESSAGE);
            return Error(SIGNIN_ERROR_MESSAGE);
        }

        var user = await _userManager.FindByEmailAsync(signIn.Email!);
        if (user == null)
        {
            _logger.LogDebug("User {Email} not found", signIn.Email);
            return Error(SIGNIN_ERROR_MESSAGE);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, signIn.Password!);
        if (!isPasswordValid)
        {
            _logger.LogDebug("Invalid password for user {Email}", signIn.Email);
            return Error(SIGNIN_ERROR_MESSAGE);
        }

        var token = _tokenService.CreateToken(user);
        _logger.LogDebug("User {Email} successully signed in", signIn.Email);
        return Ok(token);
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUp(SignUpRequest signUp)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogDebug(SIGNUP_ERROR_MESSAGE);
            return Error(SIGNUP_ERROR_MESSAGE);
        }

        var newUser = new User { Email = signUp.Email, UserName = signUp.Email };
        var result = await _userManager.CreateAsync(newUser, signUp.Password!);
        if (!result.Succeeded)
        {
            _logger.LogDebug("Failed to create user {Email}", signUp.Email);
            return BadRequest(result.Errors);
        }

        _logger.LogInformation("User {Email} created successfully!", signUp.Email);
        return Ok();
    }

    private ActionResult Error(string message) => BadRequest(new { Error = message });
}
