namespace Pentago.Services.Engine;

public class Move
{
    public Move(Color color, Square placedPiece, int rotateIndex, bool clockwise)
    {
        Color = color;
        PlacedPiece = placedPiece;
        Clockwise = clockwise;
        RotateIndex = rotateIndex;
    }

    public Color Color { get; }
    public Square PlacedPiece { get; }
    public int RotateIndex { get; }
    public bool Clockwise { get; }
}