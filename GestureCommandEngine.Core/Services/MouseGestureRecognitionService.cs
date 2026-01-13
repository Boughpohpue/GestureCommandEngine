using GestureCommandEngine.Core.Interfaces;
using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.Core.Services
{
    public class MouseGestureRecognitionService : IMouseGestureRecognitionService
    {
        public MouseGesture Recognize(List<Point2D> points, bool removeUnrecognizedItems = true)
        {
            if (points == null)
            {
                throw new ArgumentNullException($"The parameter '{nameof(points)}' must not be null!");
            }
            if (points.Count < 2)
            {
                throw new ArgumentException($"The parameter '{nameof(points)}' must contain at least 2 elements!");
            }

            var gestureItems = new List<MouseGestureItem>();

            var filteredPoints = FilterPoints(points);
            for (int x = 0; x < filteredPoints.Count - 2; x++)
            {
                var item = GetGestureItem(filteredPoints[x], filteredPoints[x + 1]);

                if (gestureItems.Count > 0 && gestureItems.Last() == item)
                {
                    continue;
                }

                if (removeUnrecognizedItems && item == MouseGestureItem.Unrecognized)
                {
                    continue;
                }

                gestureItems.Add(item);
            }

            return new MouseGesture(gestureItems);
        }

        private MouseGestureItem GetGestureItem(Point2D start, Point2D stop)
        {
            var deltaX = stop.X - start.X;
            var deltaY = stop.Y - start.Y;
            var deltaXAbs = Math.Abs(deltaX);
            var deltaYAbs = Math.Abs(deltaY);

            if (deltaXAbs == deltaYAbs)
            {
                return MouseGestureItem.Unrecognized;
            }

            if (deltaXAbs > deltaYAbs)
            {
                return deltaX > 0
                    ? MouseGestureItem.Right
                    : MouseGestureItem.Left;
            }
            else
            {
                return deltaY > 0
                    ? MouseGestureItem.Down
                    : MouseGestureItem.Up;
            }
        }

        /// <summary>
        /// Remove small delta points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<Point2D> FilterPoints(List<Point2D> points, int minDelta = 6)
        {
            var filtered = new List<Point2D>() { points[0] };

            for (int i = 1; i < points.Count; i++)
            {
                var deltaX = Math.Abs(points[i].X - filtered.Last().X);
                var deltaY = Math.Abs(points[i].Y - filtered.Last().Y);
                if (deltaX < minDelta && deltaY < minDelta)
                {
                    continue;
                }
                filtered.Add(points[i]);
            }

            return filtered;
        }
    }
}
