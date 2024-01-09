using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LcChart.Scripts
{
    /// <summary>
    /// Wpf线
    /// </summary>
    public class LcLineGeometry : LcPath
    {
        /// <summary>
        /// 线段数据
        /// </summary>
        private List<LcLineData> Datas = new List<LcLineData>();

        /// <summary>
        /// 构造器
        /// </summary>
        public LcLineGeometry()
        { }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public LcLineGeometry(Color color, double width)
        {
            PathColor = color;
            PathWidth = width;
        }

        /// <summary>
        /// 清除所有线段
        /// </summary>
        public void Clear()
        {
            Datas.Clear();
        }

        /// <summary>
        /// 添加线段
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void AddLine(Point p1, Point p2)
        {
            Datas.Add(new LcLineData(p1, p2));
        }

        /// <summary>
        /// 添加线段
        /// </summary>
        /// <param name="points"></param>
        public void AddLine(List<Point> points)
        {
            var list = LcPointList.ToLineDatas(points);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Datas.Add(list[i]);
                }
            }
        }

        /// <summary>
        /// 生成路径
        /// </summary>
        /// <returns></returns>
        public Path ToPath()
        {
            Path path = CreatePath();

            if (Datas.Count > 0)
            {
                GeometryGroup gg = new GeometryGroup();

                for (int i = 0; i < Datas.Count; i++)
                {
                    gg.Children.Add(new LineGeometry(Datas[i].Start, Datas[i].End));
                }

                path.Data = gg;
            }
            //path.IsHitTestVisible = false;

            return path;
        }
    }
}
