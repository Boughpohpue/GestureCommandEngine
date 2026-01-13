namespace GestureCommandEngine.Core.Models;

public class GestureCommandEventArgs : EventArgs
{
    public string CommandId { get; }
    public string GestureString { get; }

    public GestureCommandEventArgs(string commandId, string gestureString)
    {
        CommandId = commandId;
        GestureString = gestureString;
    }
}
