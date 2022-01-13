namespace Pentago.Services.Engine;

public class Engine : IEngine
{
    private readonly string _connectionString;

    public Engine(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Evaluation Evaluate(Board position)
    {
        throw new NotImplementedException();
    }

    public Move BestMove(Board position)
    {
        throw new NotImplementedException();
    }
}