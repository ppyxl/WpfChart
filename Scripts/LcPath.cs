using System.Windows.Media;
using System.Windows.Shapes;

namespace LcChart.Scripts
{
    /// <summary>
    /// 路径
    /// </summary>
    public class LcPath
    {
        /// <summary>
        /// 线颜色
        /// </summary>
        public Color PathColor = Colors.Black;
        /// <summary>
        /// 线条宽度（粗细）
        /// </summary>
        public double PathWidth = 0;

        /// <summary>
        /// 是否填充
        /// </summary>
        public bool IsFill = false;
        /// <summary>
        /// 填充颜色
        /// </summary>
        public Color FillColor = Colors.White;

        /// <summary>
        /// 填充颜色
        /// </summary>
        /// <param name="cc"></param>
        public void Fill(Color cc)
        {
            IsFill = true;
            FillColor = cc;
        }

        /// <summary>
        /// 生成指定格式的路径
        /// </summary>
        /// <returns></returns>
        public Path CreatePath()
        {
            Path path = new Path();
            path.StrokeThickness = PathWidth;
            path.Stroke = new SolidColorBrush(PathColor);
            if (IsFill)
            {
                path.Fill = new SolidColorBrush(FillColor);
            }
            return path;
        }
    }
}
