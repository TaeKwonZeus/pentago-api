namespace Pentago.Services.Engine;

/// <summary>
/// This class is a default implementation of the <see cref="IEngine"/> interface.
/// </summary>
/// <inheritdoc cref="IEngine"/>
public class Engine : IEngine
{
    private readonly string _connectionString;

    public Engine(string connectionString)
    {
        _connectionString = connectionString;
    }
}