using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pentago.Services.Authentication.Models;

/// <summary>
///     This record represents JWT authentication options.
/// </summary>
public record AuthenticationOptions
{
    /// <summary>
    ///     The valid JWT issuer.
    /// </summary>
    public string Issuer { get; set; } = "";

    /// <summary>
    ///     The valid JWT audience.
    /// </summary>
    public string Audience { get; set; } = "";

    /// <summary>
    ///     The secret key used for encryption.
    /// </summary>
    private string Key { get; } = "";

    /// <summary>
    ///     The JWT token lifetime.
    /// </summary>
    public int Lifetime { get; set; } = 1;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}