using LcChart.Scripts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LcChart
{
    public class LcCursorMark
    {
        public Color LineColor
        {
            get
            {
                return _lineGeometry.PathColor;
            }
            set
            {
                _lineGeometry.PathColor = value;
            }
        }

        public double LineWidth
        {
            get
            {
                return _lineGeometry.PathWidth;
            }
            set
            {
                _lineGeometry.PathWidth = value;
            }
        }

        private Canvas? _parent = null;
        private Path? _path = null;
        private LcLineGeometry _lineGeometry = new();
        private double MinX = 0;
        private double MaxX = 0;
        private double MinY = 0;
        private double MaxY = 0;

        public LcCursorMark()
        {
            LineColor = Colors.Red;
            LineWidth = 1;
        }

        public void SetParent(Canvas parent)
        {
            _parent = parent;
        }

        public void SetArea(LcRect rect)
        {
            MinX = rect.Left;
            MaxX = rect.Right;
            MinY = rect.Top;
            MaxY = rect.Bottom;
        }

        public void SetArea(double minX, double maxX, double minY, double maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public void Hide()
        {
            if (_parent != null && _path != null)
            {
                _parent.Children.Remove(_path);
                _path = null;
            }
        }

        public void Show(double x, double y)
        {
            if (_parent == null)
            {
                return;
            }

            if (_path != null)
            {
                _parent.Children.Remove(_path);
                _path = null;
                _lineGeometry.Clear();
            }

            if (MaxX > MinX && MaxY > MinY)
            {
                //添加竖线
                _lineGeometry.AddLine(new Point(x, MinY), new Point(x, MaxY));

                //添加横线
                _lineGeometry.AddLine(new Point(MinX, y), new Point(MaxX, y));

                _path = _lineGeometry.ToPath();
            }

            if (_path != null)
            {
                _parent.Children.Add(_path);
            }
        }
    }
}
