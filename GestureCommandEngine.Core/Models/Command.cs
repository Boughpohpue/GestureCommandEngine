namespace GestureCommandEngine.Core.Models;

public class Command
{
    public string CommandId { get; set; }
    public string Description { get; set; }

    public Command(string commandId, string description)
    {
        CommandId = commandId;
        Description = description;
    }

    public override string ToString()
    {
        return $"{CommandId} - {Description}";
    }
}
