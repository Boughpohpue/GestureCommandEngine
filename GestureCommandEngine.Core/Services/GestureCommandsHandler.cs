using GestureCommandEngine.Core.Interfaces;
using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.Core.Services;

public class GestureCommandsHandler : IGestureCommandsHandler
{
    private readonly IGestureCommandsRepository _gestureCommandsRepository;
    private readonly IMouseGestureRecognitionService _gestureRecognitionService;
    

    public event EventHandler<GestureCommandEventArgs> GestureCommandInvoked = delegate { };
    public event EventHandler<MouseGestureEventArgs> MouseGestureNotRecognized = delegate { };

    public GestureCommandsHandler(IGestureCommandsRepository gestureCommandsRepository, IMouseGestureRecognitionService gestureRecognitionService)
    {
        _gestureCommandsRepository = gestureCommandsRepository;
        _gestureRecognitionService = gestureRecognitionService;
    }

    public void Handle(List<Point2D> points)
    {
        var gesture = _gestureRecognitionService.Recognize(points);
        var command = _gestureCommandsRepository.GetCommandByGesture(gesture);
        if (command != null)
        {
            if (GestureCommandInvoked != null)
                GestureCommandInvoked.Invoke(this, new GestureCommandEventArgs(command.CommandId, gesture.GestureString));
        }
        else
        {
            if (MouseGestureNotRecognized != null)
                MouseGestureNotRecognized.Invoke(this, new MouseGestureEventArgs(gesture.GestureString));
        }
    }

    public void AddGestureCommand(string commandId, List<Point2D> points)
    {
        var gesture = _gestureRecognitionService.Recognize(points);
        _gestureCommandsRepository.Add(commandId, gesture.GestureString);
    }

    public void AddGestureCommand(string commandId, string gestureString)
    {
        _gestureCommandsRepository.Add(commandId, gestureString);
    }


    public void UpdateGestureCommand(string commandId, List<Point2D> points, bool overwrite = true, bool force = true)
    {
        var gesture = _gestureRecognitionService.Recognize(points);
        _gestureCommandsRepository.Update(commandId, gesture, overwrite, force);
    }

    public void RemoveGestureCommand(string commandId)
    {
        _gestureCommandsRepository.Remove(commandId);
    }

    public void AddGestureToCommand(string commandId, List<Point2D> points, bool force = true)
    {
        var gesture = _gestureRecognitionService.Recognize(points);
        _gestureCommandsRepository.AddGestureToCommand(commandId, gesture.GestureString, force);
    }

    public void RemoveGestureFromCommand(string commandId, List<Point2D> points)
    {
        var gesture = _gestureRecognitionService.Recognize(points);
        _gestureCommandsRepository.RemoveGestureFromCommand(commandId, gesture.GestureString);
    }

    public Dictionary<string, string> GetGesturesInfo()
    {
        return _gestureCommandsRepository.GetGesturesInfo();
    }
}
