namespace GestureCommandEngine.Core.Models;

public class MouseGesture
{
    public List<MouseGestureItem> GestureItems { get; set; }

    public string GestureString => string.Join("", GestureItems.Select(i => i.GetSymbol()));

    public MouseGesture(List<MouseGestureItem> items)
    {
        GestureItems = items;
    }

    public MouseGesture(string gestureString)
    {
        GestureItems = gestureString.ToMouseGestureItems();
    }

    public override string ToString()
    {
        return GestureString;
    }
}
