namespace GestureCommandEngine.Core.Models;

public enum MouseGestureItem
{
    Up,
    Down,
    Left,
    Right,
    Unrecognized,
}

public static class MouseGestureItemExtensions
{
    public static char GetSymbol(this MouseGestureItem item)
    {
        switch (item)
        {
            case MouseGestureItem.Up:
                return 'U';

            case MouseGestureItem.Down:
                return 'D';

            case MouseGestureItem.Left:
                return 'L';

            case MouseGestureItem.Right:
                return 'R';

            default:
                return '?';
        }
    }

    public static MouseGestureItem ToMouseGestureItem(this char symbol)
    {
        switch (symbol)
        {
            case 'U':
                return MouseGestureItem.Up;

            case 'D':
                return MouseGestureItem.Down;

            case 'L':
                return MouseGestureItem.Left;

            case 'R':
                return MouseGestureItem.Right;

            default:
                return MouseGestureItem.Unrecognized;
        }
    }

    public static List<MouseGestureItem> ToMouseGestureItems(this string gestureString)
    {
        return gestureString.ToArray().Select(c => c.ToMouseGestureItem()).ToList();
    }
}
