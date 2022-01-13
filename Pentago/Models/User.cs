namespace Pentago.Api.Models;

/// <summary>
///     This record represents a row in the users table.
/// </summary>
public record User
{
    /// <summary>
    ///     A user's GUID.
    /// </summary>
    private string Id { get; init; }

    /// <summary>
    ///     A user's username.
    /// </summary>
    private string Username { get; init; }

    /// <summary>
    ///     A user's normalized username.
    /// </summary>
    private string NormalizedUsername { get; init; }

    /// <summary>
    ///     A user's email.
    /// </summary>
    private string Email { get; init; }

    /// <summary>
    ///     A user's password hash.
    /// </summary>
    private string PasswordHash { get; init; }

    /// <summary>
    ///     A user's API key hash.
    /// </summary>
    private string ApiKeyHash { get; init; }

    /// <summary>
    ///     A user's Glicko rating.
    /// </summary>
    private int GlickoRating { get; init; }

    /// <summary>
    ///     A user's Glicko rating deviation.
    /// </summary>
    private double GlickoRd { get; init; }
}