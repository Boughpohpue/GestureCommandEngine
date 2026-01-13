namespace GestureCommandEngine.Core.Models;

public class GestureCommand
{
    public Command Command { get; set; }
    public List<MouseGesture> Gestures { get; set; }

    public string CommandId => Command.CommandId;
    public string Description => Command.Description;

    public GestureCommand(Command command, MouseGesture gesture)
    {
        Command = command;
        Gestures = new List<MouseGesture> { gesture };
    }
    public GestureCommand(Command command, List<MouseGesture> gestures)
    {
        Command = command;
        Gestures = gestures;
    }
    public GestureCommand(string gesture, string commandId, string description)
    {
        Command = new Command(commandId, description);
        Gestures = new List<MouseGesture> { new MouseGesture(gesture) };
    }
}
