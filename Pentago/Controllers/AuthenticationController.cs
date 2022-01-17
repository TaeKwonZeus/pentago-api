using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Pentago.Services.Authentication;
using Pentago.Services.Authentication.Models;

namespace Pentago.Controllers;

/// <summary>
/// This controller handles user authentication requests.
/// </summary>
[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IAuthenticationService authenticationService,
        ILogger<AuthenticationController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    /// <summary>
    /// Verifies the user and sends a JWT token.
    /// </summary>
    /// <param name="model">Request body.</param>
    /// <returns>A JWT token.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var token = await _authenticationService.GetTokenAsync(model);
        if (token == null)
        {
            _logger.LogInformation("Login failed");
            return BadRequest(new { errorText = "Invalid username or password" });
        }

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

        _logger.LogInformation("Successful login");
        return Ok(new { token = encodedToken });
    }

    /// <summary>
    /// Registers the user.
    /// </summary>
    /// <param name="model">Request body.</param>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            await _authenticationService.RegisterAsync(model);
            _logger.LogInformation("User {User} logged in", model.Username);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Login failed");
            return Problem("Internal server error: ", e.Message);
        }
    }
}