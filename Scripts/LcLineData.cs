using System.Windows;

namespace LcChart.Scripts
{
    /// <summary>
    /// 二维线段
    /// </summary>
    public class LcLineData
    {
        /// <summary>
        /// 起点
        /// </summary>
        public Point Start;

        /// <summary>
        /// 终点
        /// </summary>
        public Point End;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        public LcLineData(Point s, Point e)
        {
            Start = s;
            End = e;
        }
    }
}
