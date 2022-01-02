namespace Pentago.Services.Authentication.Models;

/// <summary>
/// This record represents a login request body.
/// </summary>
/// <param name="UsernameOrEmail">The user's username or email.</param>
/// <param name="Password">The user's password.</param>
public record LoginModel(string UsernameOrEmail, string Password);