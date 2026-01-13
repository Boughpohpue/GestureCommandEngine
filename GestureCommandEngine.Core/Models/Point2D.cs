namespace GestureCommandEngine.Core.Models;

public readonly struct Point2D
{
    public int X { get; }
    public int Y { get; }

    public Point2D(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Point2D(double x, double y)
    {
        X = (int)x;
        Y = (int)y;
    }
}
