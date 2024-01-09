using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace LcChart.Scripts
{
    /// <summary>
    /// 几何图形
    /// </summary>
    public class LcPathFigure
    {
        /// <summary>
        /// 连线数据
        /// </summary>
        public PathFigure Figure = new PathFigure();

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="isClosed">是否闭合</param>
        public LcPathFigure(double startX, double startY, bool isClosed = true)
        {
            Figure.StartPoint = new Point(startX, startY);
            Figure.IsClosed = isClosed;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="startPoint">起点</param>
        /// <param name="isClosed">是否闭合</param>
        public LcPathFigure(Point startPoint, bool isClosed = true)
        {
            Figure.StartPoint = startPoint;
            Figure.IsClosed = isClosed;
        }

        public LcPathFigure(List<Point> points, bool isClosed = true)
        {
            if (points.Count > 0)
            {
                Figure.StartPoint = points[0];
                for (int i = 1; i < points.Count; i++)
                {
                    AddLine(points[i]);
                }
            }
            Figure.IsClosed = isClosed;
        }

        /// <summary>
        /// 添加直线
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddLine(Point endPoint)
        {
            Figure.Segments.Add(new LineSegment(endPoint, true));
        }

        /// <summary>
        /// 添加直线
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddLine(double x, double y)
        {
            AddLine(new Point(x, y));
        }

        /// <summary>
        /// 添加弧线
        /// </summary>
        public void AddArc(Point endPoint, Size arcRadius, bool isClockWise, bool isLargeArc)
        {
            ArcSegment arc = new ArcSegment();
            arc.Point = endPoint;
            arc.Size = arcRadius;
            arc.SweepDirection = isClockWise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
            arc.IsLargeArc = isLargeArc;
            Figure.Segments.Add(arc);
        }

        /// <summary>
        /// 添加两次贝塞尔曲线
        /// </summary>
        public void AddBezier(Point endPoint, Point controlPoint1, Point controlPoint2)
        {
            BezierSegment bezier = new BezierSegment();
            bezier.Point1 = controlPoint1;
            bezier.Point2 = controlPoint2;
            bezier.Point3 = endPoint;
            Figure.Segments.Add(bezier);
        }

        /// <summary>
        /// 添加一次贝塞尔曲线
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="controlPoint"></param>
        public void AddBezier(Point endPoint, Point controlPoint)
        {
            QuadraticBezierSegment QuadraticBezier = new();
            QuadraticBezier.Point1 = controlPoint;
            QuadraticBezier.Point2 = endPoint;
            Figure.Segments.Add(QuadraticBezier);
        }
    }
}
