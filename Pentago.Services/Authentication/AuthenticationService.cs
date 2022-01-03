using Pentago.Services.Authentication.Models;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace Pentago.Services.Authentication;

/// <summary>
/// This class is a default implementation of the <see cref="IAuthenticationService"/> interface.
/// </summary>
/// <inheritdoc cref="IAuthenticationService"/>
public class AuthenticationService : IAuthenticationService
{
    private readonly string _connectionString;

    public AuthenticationService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<string?> LoginAsync(LoginModel model)
    {
        // TODO Rewrite this shit

        var (usernameOrEmail, password) = model;

        await using var connection = new SQLiteConnection(_connectionString);
        await connection.OpenAsync();

        var command =
            new SQLiteCommand(
                @"SELECT id, api_key_hash
                    FROM users
                    WHERE (normalized_username = @usernameOrEmail OR email = @usernameOrEmail)
                      AND password_hash = @passwordHash
                    LIMIT 1;",
                connection);
        command.Parameters.AddWithValue("@usernameOrEmail", ToStandard(usernameOrEmail));
        command.Parameters.AddWithValue("@passwordHash", Sha256HashOf(password));

        int id;

        await using (var reader = await command.ExecuteReaderAsync())
        {
            if (!await reader.ReadAsync()) return null;

            var idOrdinal = reader.GetOrdinal("id");

            id = reader.GetInt32(idOrdinal);
        }

        var apiKey = GenerateApiKey();

        command.CommandText = @"UPDATE users SET api_key_hash = @apiKeyHash WHERE id = @id;";
        command.Parameters.Clear();
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@apiKeyHash", Sha256HashOf(apiKey));

        await command.ExecuteNonQueryAsync();

        return apiKey;
    }

    public async Task RegisterAsync(RegisterModel model)
    {
        // TODO Write this shit
        await Task.Run(() => { }).ConfigureAwait(false);

        throw new NotImplementedException();
    }

    private static string ToStandard(string str) => str.Trim().Normalize().ToLower();

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