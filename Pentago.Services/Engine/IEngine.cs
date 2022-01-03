namespace Pentago.Services.Engine;

public interface IEngine
{
    public Evaluation Evaluate(Board position);

    public Move BestMove(Board position);

    public static IEngine Instance(string connectionString)
    {
        return new Engine(connectionString);
    }
}