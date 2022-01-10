using Microsoft.AspNetCore.Mvc;
using Pentago.Services.Authentication;
using Pentago.Services.Authentication.Models;

namespace Pentago.Api.Controllers.Authentication;

/// <summary>
///     This controller represents a register endpoint.
/// </summary>
[Route("api/auth/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(IAuthenticationService authenticationService, ILogger<RegisterController> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    /// <summary>
    ///     Registers the user.
    /// </summary>
    /// <param name="model">Request body.</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegisterModel model)
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