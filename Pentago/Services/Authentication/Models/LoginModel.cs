namespace Pentago.Services.Authentication.Models;

/// <summary>
/// This record represents a login request body.
/// </summary>
/// <param name="Username">The user's username.</param>
/// <param name="Password">The user's password.</param>
public record LoginModel(string Username, string Password);