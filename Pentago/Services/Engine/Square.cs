namespace Pentago.Services.Engine;

public struct Square
{
    private int _x;

    private int _y;

    public int X
    {
        get => _x;
        set
        {
            if (value > 5)
                throw new IndexOutOfRangeException("X must be between 0 and 5 inclusive.");

            _x = value;
        }
    }

    public int Y
    {
        get => _y;
        set
        {
            if (value > 5)
                throw new IndexOutOfRangeException("Y must be between 0 and 5 inclusive.");

            _y = value;
        }
    }

    public Color? State { get; set; }

    public void Rotate(bool clockwise)
    {
        var x = _x;
        var y = _y;
        if (clockwise)
        {
            _x += x % 3 - 1 + (x + -y) * -1;
            _y += y % 3 - 1 + (x + y) * -1;
        }
        else
        {
            _x += x % 3 - 1 + (x + y) * -1;
            _y += y % 3 - 1 + (-x + y) * -1;
        }
    }

    public Square(int x, int y, Color? state = null)
    {
        if (x > 5 || y > 5)
            throw new IndexOutOfRangeException("Arguments must be between 0 and 5 inclusive.");
        _x = x;
        _y = y;

        State = state;
    }
}