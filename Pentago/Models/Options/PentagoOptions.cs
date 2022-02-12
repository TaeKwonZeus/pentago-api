namespace Pentago.Models.Options;

/// <summary>
/// This record represents pentago and Glicko options.
/// </summary>
public record PentagoOptions
{
    /// <summary>
    /// Default Glicko rating.
    /// </summary>
    public int DefaultGlickoRating { get; set; } = 0;

    /// <summary>
    /// Default Glicko rating deviation.
    /// </summary>
    public double DefaultGlickoRd { get; set; } = 0;
}