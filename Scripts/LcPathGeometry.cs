using System.Windows.Media;
using System.Windows.Shapes;

namespace LcChart.Scripts
{
    /// <summary>
    /// 复合几何图形
    /// </summary>
    public class LcPathGeometry : LcPath
    {
        /// <summary>
        /// 图形数据
        /// </summary>
        public PathGeometry Geometry = new PathGeometry();

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public LcPathGeometry(Color color, double width)
        {
            PathColor = color;
            PathWidth = width;
        }

        /// <summary>
        /// 添加几何数据
        /// </summary>
        /// <param name="figure"></param>
        public void AddFigure(PathFigure figure)
        {
            Geometry.Figures.Add(figure);
        }

        /// <summary>
        /// 生成路径
        /// </summary>
        /// <returns></returns>
        public Path ToPath()
        {
            Path path = CreatePath();
            path.Data = Geometry;
            //path.IsHitTestVisible = false;
            return path;
        }
    }
}
