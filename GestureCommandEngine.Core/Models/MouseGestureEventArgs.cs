namespace GestureCommandEngine.Core.Models;

public class MouseGestureEventArgs : EventArgs
{
    public string GestureString { get; }

    public MouseGestureEventArgs(string gesture)
    {
        GestureString = gesture;
    }
}
