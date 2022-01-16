using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Pentago.Models.Options;

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

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     The secret key used for encryption.
    /// </summary>
    public string Key { get; set; } = "";

    /// <summary>
    ///     The JWT token lifetime.
    /// </summary>
    public int Lifetime { get; set; } = 1;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}