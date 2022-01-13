namespace Pentago.Services.Authentication.Models;

/// <summary>
///     This record represents a registration request body.
/// </summary>
/// <param name="Username">The user's username.</param>
/// <param name="Email">The user's email.</param>
/// <param name="Password">The user's password.</param>
public record RegisterModel(string Username, string Email, string Password);