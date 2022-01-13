using Pentago.Services.Authentication.Models;

namespace Pentago.Services.Authentication;

/// <summary>
///     This interface represents an authentication service.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    ///     Verifies the user and creates an API key.
    /// </summary>
    /// <param name="model">User data.</param>
    /// <returns>The user's API key.</returns>
    Task<string?> LoginAsync(LoginModel model);

    /// <summary>
    ///     Registers the user.
    /// </summary>
    /// <param name="model">User data.</param>
    Task RegisterAsync(RegisterModel model);
}