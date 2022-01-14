using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Pentago.Models;
using Pentago.Services.Authentication.Models;

namespace Pentago.Services.Authentication;

/// <summary>
///     This class is a default implementation of the <see cref="IAuthenticationService" /> interface.
/// </summary>
/// <inheritdoc cref="IAuthenticationService" />
public class AuthenticationService : IAuthenticationService
{
    private readonly string _connectionString;

    public AuthenticationService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<JwtSecurityToken?> GetTokenAsync(LoginModel model)
    {
        await using var connection = new SQLiteConnection(_connectionString);

        var identity = connection.QueryFirst<User>("");
        
        return null;
    }

    public async Task RegisterAsync(RegisterModel model)
    {
        // TODO Write this shit
        await Task.Run(() => { }).ConfigureAwait(false);

        throw new NotImplementedException();
    }

    private static string ToStandard(string str)
    {
        return str.Trim().Normalize().ToLower();
    }

    private static string Sha256HashOf(string str)
    {
        using var hash = SHA256.Create();

        return string.Concat(hash
            .ComputeHash(Encoding.UTF8.GetBytes(str))
            .Select(item => item.ToString("x2")));
    }

    private static string GenerateApiKey()
    {
        var key = new byte[32];

        using var generator = RandomNumberGenerator.Create();

        generator.GetBytes(key);

        return Convert.ToBase64String(key);
    }
}