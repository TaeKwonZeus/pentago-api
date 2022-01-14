namespace Pentago.Services.Engine;

public class Evaluation
{
    private double? _eval;
    private int? _winIn;

    public double? Eval
    {
        get => _eval;
        set
        {
            if (value == null) return;

            _eval = value;
            _winIn = null;
        }
    }

    public int? WinIn
    {
        get => _winIn;
        set
        {
            if (value == null) return;

            _winIn = value;
            _eval = null;
        }
    }

    public override string ToString()
    {
        if (_eval != null) return _eval.ToString()!;
        return _winIn != null ? $"#{_winIn}" : "-";
    }
}