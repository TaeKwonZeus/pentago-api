using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Pentago.Models.Options;
using Pentago.Services.Authentication.Models;

namespace Pentago.Services.Authentication;

/// <summary>
/// This class is a default implementation of the <see cref="IAuthenticationService" /> interface.
/// </summary>
/// <inheritdoc cref="IAuthenticationService" />
public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationOptions _authenticationOptions = new();
    private readonly string _connectionString;

    public AuthenticationService(IConfiguration configuration)
    {
        configuration.GetSection("AuthenticationOptions").Bind(_authenticationOptions);

        _connectionString = configuration.GetConnectionString("App");
    }

    public async Task<JwtSecurityToken?> GetTokenAsync(LoginModel model)
    {
        var (username, password) = model;

        await using var connection = new SQLiteConnection(_connectionString);

        var user = connection.QueryFirstOrDefault<string>(@"SELECT id
            FROM users
            WHERE username = @Username
              AND password_hash = @PasswordHash", new
        {
            Username = username,
            PasswordHash = Sha256HashOf(password)
        });
        if (user == null) return null;

        return new JwtSecurityToken(
            _authenticationOptions.Issuer,
            _authenticationOptions.Audience,
            new List<Claim> { new(ClaimsIdentity.DefaultNameClaimType, model.Username) },
            DateTime.UtcNow,
            DateTime.UtcNow.Add(TimeSpan.FromDays(_authenticationOptions.Lifetime)),
            new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    }

    public async Task RegisterAsync(RegisterModel model)
    {
        // TODO Write this shit
        await Task.Run(() => { }).ConfigureAwait(false);

        throw new NotImplementedException();
    }

    private static string Sha256HashOf(string str)
    {
        using var hash = SHA256.Create();

        return string.Concat(hash
            .ComputeHash(Encoding.UTF8.GetBytes(str))
            .Select(item => item.ToString("x2")));
    }
}