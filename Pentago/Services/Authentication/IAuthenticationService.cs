using System.IdentityModel.Tokens.Jwt;
using Pentago.Services.Authentication.Models;

namespace Pentago.Services.Authentication;

/// <summary>
/// This interface represents an authentication service.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Verifies the user and returns a JWT token.
    /// </summary>
    /// <param name="model">User data.</param>
    /// <returns>A JWT token.</returns>
    Task<JwtSecurityToken?> GetTokenAsync(LoginModel model);

    /// <summary>
    /// Registers the user.
    /// </summary>
    /// <param name="model">User data.</param>
    Task RegisterAsync(RegisterModel model);
}