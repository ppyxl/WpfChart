using System.Collections.Generic;
using System.Windows;

namespace LcChart.Scripts
{
    /// <summary>
    /// 二维点管理
    /// </summary>
    public class LcPointList
    {
        /// <summary>
        /// 所有点
        /// </summary>
        public List<Point> Data = new();

        /// <summary>
        /// 构造器
        /// </summary>
        public LcPointList() { }

        /// <summary>
        /// 添加新点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Add(double x, double y)
        {
            Data.Add(new Point(x, y));
        }

        /// <summary>
        /// 将一组点转为线段集合
        /// </summary>
        /// <returns></returns>
        public List<LcLineData> ToLineDatas()
        {
            return ToLineDatas(Data);
        }

        /// <summary>
        /// 将一组点转为线段集合
        /// </summary>
        /// <returns></returns>
        public static List<LcLineData> ToLineDatas(List<Point> points)
        {
            List<LcLineData> list = new();

            if (points != null && points.Count > 1)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    list.Add(new LcLineData(points[i - 1], points[i]));
                }
            }

            return list;
        }
    }
}
