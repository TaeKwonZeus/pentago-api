﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Pentago.Models.Options;
using Pentago.Services.Authentication.Models;
using Pentago.Services.Database;

namespace Pentago.Services.Authentication;

/// <summary>
/// This class is a default implementation of the <see cref="IAuthenticationService" /> interface.
/// </summary>
/// <inheritdoc cref="IAuthenticationService" />
public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationOptions _authenticationOptions = new();
    private readonly IDbService _dbService;
    private readonly PentagoOptions _pentagoOptions = new();

    public AuthenticationService(IConfiguration configuration, IDbService dbService)
    {
        _dbService = dbService;
        configuration.GetSection("AuthenticationOptions").Bind(_authenticationOptions);
        configuration.GetSection("PentagoOptions").Bind(_pentagoOptions);
    }

    public async Task<JwtSecurityToken?> GetTokenAsync(LoginModel model)
    {
        var (username, password) = model;

        await using var connection = _dbService.GetDbConnection();

        var user = await connection.QueryFirstOrDefaultAsync<string>(@"SELECT id
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
        var (username, email, password) = model;

        await using var connection = _dbService.GetDbConnection();

        var user = await connection.QueryFirstOrDefaultAsync<string>(@"SELECT id
            FROM users
            WHERE username = @Username
              OR email = @Email", new
        {
            Username = username,
            Email = email
        });

        if (user != null) throw new InvalidDataException("User already exists");

        await connection.ExecuteAsync(@"INSERT INTO users (id, username, email, password_hash, glicko_rating, glicko_rd)
            VALUES (@Id, @Username, @Email, @PasswordHash, @GlickoRating, @GlickoRd);", new
        {
            Id = Guid.NewGuid().ToString(),
            Username = username,
            Email = email,
            PasswordHash = Sha256HashOf(password),
            GlickoRating = _pentagoOptions.DefaultGlickoRating,
            GlickoRd = _pentagoOptions.DefaultGlickoRd
        });
    }

    private static string Sha256HashOf(string str)
    {
        using var hash = SHA256.Create();

        return string.Concat(hash
            .ComputeHash(Encoding.UTF8.GetBytes(str))
            .Select(item => item.ToString("x2")));
    }
}