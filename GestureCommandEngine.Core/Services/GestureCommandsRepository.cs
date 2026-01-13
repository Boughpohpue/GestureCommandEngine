using GestureCommandEngine.Core.Interfaces;
using GestureCommandEngine.Core.Models;
using System.ComponentModel.Design;

namespace GestureCommandEngine.Core.Services;

public class GestureCommandsRepository : IGestureCommandsRepository
{
    private readonly ICommandsRepository _commandsRepository;
    private readonly List<GestureCommand> _commands;

    public GestureCommandsRepository(ICommandsRepository commandsRepository)
    {
        _commandsRepository = commandsRepository;
        _commands = new List<GestureCommand>();
    }

    public GestureCommand? GetCommandById(string commandId)
    {
        return _commands.FirstOrDefault(c => c.CommandId == commandId);
    }
    public GestureCommand? GetCommandByGesture(MouseGesture gesture)
    {
        return _commands.FirstOrDefault(c => c.Gestures.Any(g => g.GestureString == gesture.GestureString));
    }
    public GestureCommand? GetCommandByGestureString(string gestureString)
    {
        return _commands.FirstOrDefault(c => c.Gestures.Any(g => g.GestureString == gestureString));
    }

    public void Add(GestureCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        if (!_commandsRepository.Has(command.Command))
        {
            throw new KeyNotFoundException($"Unknown '{command.CommandId}' command!");
        }
        if (command.Gestures.Any(g => !IsGestureAvailable(g)))
        {
            throw new ArgumentException("Provided command contains a gesture that is already in use!");
        }

        _commands.Add(command);
    }

    public void Add(Command cmd, MouseGesture gesture)
    {
        if (cmd == null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }
        if (gesture == null)
        {
            throw new ArgumentNullException(nameof(gesture));
        }
        if (!_commandsRepository.Has(cmd))
        {
            throw new KeyNotFoundException($"Unknown '{cmd.CommandId}' command!");
        }
        if (!IsGestureAvailable(gesture))
        {
            throw new ArgumentException($"Provided gesture '{gesture.GestureString}' is already in use!");
        }

        if (GetCommandById(cmd.CommandId) != null)
        {
            throw new ArgumentException($"Command '{cmd.CommandId}' already exists!");
        }

        Add(new GestureCommand(cmd, gesture));
    }

    public void Add(string commandId, string gestureString)
    {
        if (string.IsNullOrWhiteSpace(gestureString))
        {
            throw new ArgumentNullException(nameof(gestureString));
        }
        if (string.IsNullOrWhiteSpace(commandId))
        {
            throw new ArgumentNullException(nameof(commandId));
        }

        var cmd = _commandsRepository.Get(commandId);
        if (cmd == null)
        {
            throw new KeyNotFoundException($"Unknown '{commandId}' command!");
        }

        Add(new GestureCommand(cmd, new MouseGesture(gestureString)));
    }

    /// <summary>
    /// Adds new gesture to command
    /// </summary>
    /// <param name="commandId">Command ID</param>
    /// <param name="gestureString">Gesture string</param>
    /// <param name="force">Remove gesture from any command using that gesture</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public void AddGestureToCommand(string commandId, string gestureString, bool force = false)
    {
        if (!IsGestureAvailable(gestureString) && !force)
        {
            throw new ArgumentException($"Provided gesture '{gestureString}' is already in use!");
        }

        var cmd = GetCommandById(commandId);
        if (cmd == null)
        {
            throw new KeyNotFoundException($"Command '{commandId}' not found!");
        }

        cmd.Gestures.Add(new MouseGesture(gestureString));
    }

    public void RemoveGestureFromCommand(string commandId, string gestureString)
    {
        var cmd = GetCommandById(commandId);
        if (cmd == null)
        {
            throw new KeyNotFoundException($"Command '{commandId}' not found!");
        }

        var gesture = cmd.Gestures.First(g => g.GestureString == gestureString);
        if (gesture != null)
        {
            cmd.Gestures.Remove(gesture);
        }
    }

    /// <summary>
    /// Updates command with new gesture
    /// </summary>
    /// <param name="commandId">Command ID</param>
    /// <param name="gestureString">Gesture string</param>
    /// <param name="overwrite">If set to true overwrites existing gestures, otherwise adds new gesture</param>
    /// <param name="force">If set to true, removes gesture from any command using that gesture</param>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void Update(string commandId, string gestureString, bool overwrite = true, bool force = false)
    {
        var command = GetCommandById(commandId);
        if (command == null)
        {
            throw new KeyNotFoundException($"Command '{commandId}' not found!");
        }

        var gestureCommand = GetCommandByGestureString(gestureString);
        if (gestureCommand != null && gestureCommand.CommandId != commandId)
        {
            if (!force)
            {
                throw new ArgumentException($"Provided gesture '{gestureString}' is already in use!");
            }
            var gesture = gestureCommand.Gestures.First(g => g.GestureString == gestureString);
            gestureCommand.Gestures.Remove(gesture);
        }

        if (overwrite)
            command.Gestures = new List<MouseGesture> { new MouseGesture(gestureString) };
        else
            command.Gestures.Add(new MouseGesture(gestureString));
    }

    public void Update(string commandId, MouseGesture gesture, bool overwrite = true, bool force = false)
    {
        Update(commandId, gesture.GestureString, overwrite, force);
    }

    public void Remove(string commandId)
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            throw new ArgumentNullException(nameof(commandId));
        }

        var cmd = GetCommandById(commandId);
        if (cmd == null)
        {
            throw new KeyNotFoundException($"Command '{commandId}' not found!");
        }

        _commands.Remove(cmd);
    }

    public void Clear()
    {
        _commands.Clear();
    }

    public Dictionary<string, string> GetGesturesInfo()
    {
        var retval = new Dictionary<string, string>();

        foreach (var cmd in _commands)
        {
            foreach (var gesture in cmd.Gestures)
            {
                retval.Add(gesture.GestureString, cmd.Description);
            }    
        }

        return retval;
    }

    public Dictionary<string, List<string>> GetGestureCommandsInfo()
    {
        var retval = new Dictionary<string, List<string>>();

        foreach (var cmd in _commands)
        {
            retval.Add(cmd.CommandId, cmd.Gestures.Select(g => g.GestureString).ToList());
        }

        return retval;
    }

    private bool IsGestureAvailable(MouseGesture gesture)
    {
        return _commands.Count(cmd => cmd.Gestures.Contains(gesture)) == 0;
    }

    private bool IsGestureAvailable(string gestureString)
    {
        return _commands.Count(cmd => cmd.Gestures.Any(g => g.GestureString == gestureString)) == 0;
    }
}
