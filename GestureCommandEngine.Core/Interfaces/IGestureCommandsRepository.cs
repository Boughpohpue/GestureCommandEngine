using GestureCommandEngine.Core.Models;
using System.Windows.Input;

namespace GestureCommandEngine.Core.Interfaces;

public interface IGestureCommandsRepository
{
    GestureCommand? GetCommandById(string commandId);
    GestureCommand? GetCommandByGesture(MouseGesture gesture);
    GestureCommand? GetCommandByGestureString(string gestureString);
    void Add(GestureCommand command);
    void Add(Command cmd, MouseGesture gesture);
    void Add(string commandId, string gestureString);
    void Update(string commandId, string gestureString, bool overwrite = true, bool force = false);
    void Update(string commandId, MouseGesture gesture, bool overwrite = true, bool force = false);
    void Remove(string commandId);
    void Clear();
    void AddGestureToCommand(string commandId, string gestureString, bool force = false);
    void RemoveGestureFromCommand(string commandId, string gestureString);
    Dictionary<string, string> GetGesturesInfo();
    Dictionary<string, List<string>> GetGestureCommandsInfo();
}
