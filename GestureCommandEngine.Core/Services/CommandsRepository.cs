using GestureCommandEngine.Core.Interfaces;
using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.Core.Services;

public class CommandsRepository : ICommandsRepository
{
    private readonly List<Command> _commands;

    public CommandsRepository()
    {
        _commands = new List<Command>();
    }

    public bool Has(string commandId)
    {
        return _commands.Any(c => c.CommandId == commandId);
    }

    public bool Has(Command command)
    {
        return Has(command.CommandId);
    }

    public void Add(Command command)
    {
        if (command == null)
            throw new ArgumentNullException("Provided command must not be null!");
        if (_commands.Any(c => c.CommandId == command.CommandId))
            throw new ArgumentException($"Command '{command.CommandId}' already exists!");

        _commands.Add(command);
    }

    public void Add(string commandId, string description)
    {
        Add(new Command(commandId, description));
    }

    public Command? Get(string commandId)
    {
        return _commands.FirstOrDefault(c => c.CommandId == commandId);
    }

    public void Remove(string commandId)
    {
        var cmd = Get(commandId);
        if (cmd != null)
        {
            _commands.Remove(cmd);
        }
    }

    public void Remove(Command command)
    {
        if (Has(command))
        {
            _commands.Remove(command);
        }
    }

    public void Update(string commandId, string description)
    {
        if (Has(commandId))
        {
            _commands.First(c => c.CommandId == commandId).Description = description;
        }
    }

    public List<string> GetCommandsInfo()
    {
        return _commands.Select(c => c.ToString()).ToList();
    }
}
