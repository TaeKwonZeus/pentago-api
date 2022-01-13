using Microsoft.AspNetCore.Mvc;
using Pentago.Services.Authentication;
using Pentago.Services.Authentication.Models;

namespace Pentago.Api.Controllers;

/// <summary>
///     This controller handles user authentication requests.
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
    ///     Verifies the user and sends an API key as a response.
    /// </summary>
    /// <param name="model">Request body.</param>
    /// <returns>The user's API key.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var res = await _authenticationService.LoginAsync(model);

            if (res == null) return NotFound("User not found");

            _logger.LogInformation("Successfully logged in user {User}", model.UsernameOrEmail);
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Login failed");

            return Problem("Internal server error: ", e.Message);
        }
    }

    /// <summary>
    ///     Registers the user.
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