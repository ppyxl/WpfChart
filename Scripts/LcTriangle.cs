using System.Windows;

namespace LcChart.Scripts
{
    public enum LcTriangleDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    //三角形
    public class LcTriangle
    {
        public Point StartPoint = new(0, 0);

        public double Width = 0;
        public double Height = 0;

        public LcTriangle(Point startPoint, double width, double height)
        {
            StartPoint = startPoint;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 返回三角形的3个点
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<Point> Get(LcTriangleDirection direction)
        {
            List<Point> points = new List<Point>();

            switch (direction)
            {
                case LcTriangleDirection.Left:
                    points.Add(new Point(StartPoint.X, StartPoint.Y + 0.5 * Width));
                    points.Add(new Point(StartPoint.X, StartPoint.Y - 0.5 * Width));
                    points.Add(new Point(StartPoint.X - Height, StartPoint.Y));
                    break;
                case LcTriangleDirection.Right:
                    points.Add(new Point(StartPoint.X, StartPoint.Y + 0.5 * Width));
                    points.Add(new Point(StartPoint.X, StartPoint.Y - 0.5 * Width));
                    points.Add(new Point(StartPoint.X + Height, StartPoint.Y));
                    break;
                case LcTriangleDirection.Up:
                    points.Add(new Point(StartPoint.X + 0.5 * Width, StartPoint.Y));
                    points.Add(new Point(StartPoint.X - 0.5 * Width, StartPoint.Y));
                    points.Add(new Point(StartPoint.X, StartPoint.Y - Height));
                    break;
                default://Down
                    points.Add(new Point(StartPoint.X + 0.5 * Width, StartPoint.Y));
                    points.Add(new Point(StartPoint.X - 0.5 * Width, StartPoint.Y));
                    points.Add(new Point(StartPoint.X, StartPoint.Y + Height));
                    break;
            }

            return points;
        }

        public List<Point> Get(LcTriangleDirection direction, double radius)
        {
            var temp = Get(direction);
            if (radius > 0)
            {
                List<Point> points = new List<Point>();

                points.Add(GetPointByDistance(temp[0], temp[1], radius, false));
                points.Add(GetPointByDistance(temp[0], temp[1], radius, true));
                points.Add(GetPointByDistance(temp[1], temp[2], radius, false));
                points.Add(GetPointByDistance(temp[1], temp[2], radius, true));
                points.Add(GetPointByDistance(temp[2], temp[0], radius, false));
                points.Add(GetPointByDistance(temp[2], temp[0], radius, true));

                return points;
            }
            else
            {
                return temp;
            }
        }

        public static Point GetPointByDistance(Point start, Point end, double distance, bool isFromEnd = true)
        {
            LcPoint pp = LcPoint.GetPointByDistance(start.X, start.Y, end.X, end.Y, distance, isFromEnd);
            return new Point(pp.X, pp.Y);
        }
    }
}
