using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.Core.Interfaces;

public interface IMouseGestureRecognitionService
{
    MouseGesture Recognize(List<Point2D> points, bool removeUnrecognizedItems = true);
}
