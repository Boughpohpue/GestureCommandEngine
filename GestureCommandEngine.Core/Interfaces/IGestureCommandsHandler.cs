using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.Core.Interfaces;

public interface IGestureCommandsHandler
{
    event EventHandler<GestureCommandEventArgs> GestureCommandInvoked;
    event EventHandler<MouseGestureEventArgs> MouseGestureNotRecognized;

    void Handle(List<Point2D> points);
    void AddGestureCommand(string commandId, List<Point2D> points);
    void AddGestureCommand(string commandId, string gestureString);
    void UpdateGestureCommand(string commandId, List<Point2D> points, bool overwrite = true, bool force = true);
    void RemoveGestureCommand(string commandId);
    void AddGestureToCommand(string commandId, List<Point2D> points, bool force = true);
    void RemoveGestureFromCommand(string commandId, List<Point2D> points);
    Dictionary<string, string> GetGesturesInfo();
}
