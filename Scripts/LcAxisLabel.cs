using System.Windows;
using System.Windows.Controls;

namespace LcChart
{
    /// <summary>
    /// 根据序号生成标签
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public delegate string GetTextByIndex(int index);

    /// <summary>
    /// 坐标轴标签
    /// </summary>
    public class LcAxisLabel
    {
        /// <summary>
        /// 标签生成函数
        /// </summary>
        public GetTextByIndex? CreateLabelText = null;

        /// <summary>
        /// 固定标签枚举
        /// </summary>
        public List<string>? LabelTexts = null;
        /// <summary>
        /// 标签距离轴的距离
        /// </summary>
        public double Margin = 5;
        /// <summary>
        /// 标签当前最大宽度
        /// </summary>
        public double Width = 0;
        /// <summary>
        /// 标签当前最大高度
        /// </summary>
        public double Height = 0;
        /// <summary>
        /// 标签最大宽度
        /// </summary>
        public double MaxWidth = 100;
        /// <summary>
        /// 标签最大高度
        /// </summary>
        public double MaxHeight = 100;
        /// <summary>
        /// 是否为X轴
        /// </summary>
        public bool IsX = true;

        /// <summary>
        /// 标签控件
        /// </summary>
        private List<Label> _labels = new();
        /// <summary>
        /// 起始值
        /// </summary>
        private double _min = 0;
        /// <summary>
        /// 间隔
        /// </summary>
        private double _step = 0;
        /// <summary>
        /// 字体大小
        /// </summary>
        private double _fontSize = 15;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="isX"></param>
        public LcAxisLabel(bool isX = true)
        {
            IsX = isX;
        }

        /// <summary>
        /// 设置初始值和间隔
        /// </summary>
        /// <param name="min"></param>
        /// <param name="step"></param>
        public void Set(double min, double step)
        {
            _min = min;
            _step = step;
        }

        /// <summary>
        /// 根据序号获取标签内容
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetLabelText(int index)
        {
            if (LabelTexts != null && LabelTexts.Count > 0)
            {
                if (index >= 0 && index < LabelTexts.Count)
                {
                    return LabelTexts[index];
                }
                else
                {
                    return string.Empty;
                }
            }
            else if (CreateLabelText != null)
            {
                return CreateLabelText(index);
            }
            else
            {
                return LcChartTool.Double2String(_min + index * _step, 2);
            }
        }

        /// <summary>
        /// 根据文本内容计算标签最大宽度和最大高度
        /// </summary>
        /// <param name="value"></param>
        public void CalculateLabelMax(string value)
        {
            LcFormattedText lft = new(value, CreateLabel());
            Width = lft.Width;
            Height = lft.Height;
        }

        /// <summary>
        /// 根据标签数量计算标签的最大宽度和最大高度
        /// </summary>
        /// <param name="labelCount"></param>
        public void CalculateLabelMax(int labelCount)
        {
            Width = 0;
            Height = 0;

            Label label = CreateLabel();

            for (int i = 0; i < labelCount; i++)
            {
                LcFormattedText lft = new LcFormattedText(GetLabelText(i), label);
                if (Width < lft.Width)
                {
                    Width = lft.Width;
                }
                if (Height < lft.Height)
                {
                    Height = lft.Height;
                }
            }
        }

        /// <summary>
        /// 显示所有标签
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="positions"></param>
        /// <param name="maxX"></param>
        public void ShowLabels(Canvas parent, List<Point> positions, double maxX)
        {
            _labels.Clear();
            Width = 0;
            Height = 0;
            for (int i = 0; i < positions.Count; i++)
            {
                parent.Children.Add(CreateLabel(GetLabelText(i), positions[i], IsX, maxX));
            }
        }

        /// <summary>
        /// 更新所有标签的文本内容
        /// </summary>
        public void UpdateLabelText()
        {
            if (_labels != null && _labels.Count > 0)
            {
                for (int i = 0; i < _labels.Count; i++)
                {
                    _labels[i].Content = GetLabelText(i);
                }
            }
        }

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="isX"></param>
        /// <param name="maxX"></param>
        /// <returns></returns>
        private Grid CreateLabel(string text, Point position, bool isX, double maxX)
        {
            Label label = CreateLabel();
            label.Content = text;

            Grid grid = new Grid();
            if (isX)
            {
                grid.Width = MaxWidth;//最大宽度

                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Top;

                Canvas.SetLeft(grid, position.X - grid.Width * 0.5);
                Canvas.SetTop(grid, position.Y + Margin);
            }
            else
            {
                grid.Height = MaxHeight;//最大高度

                label.HorizontalAlignment = HorizontalAlignment.Right;
                label.VerticalAlignment = VerticalAlignment.Center;

                Canvas.SetRight(grid, maxX - position.X + Margin);
                Canvas.SetTop(grid, position.Y - grid.Height * 0.5);
            }

            _labels.Add(label);
            grid.Children.Add(label);

            return grid;
        }

        /// <summary>
        /// 生成基本标签
        /// </summary>
        /// <returns></returns>
        private Label CreateLabel()
        {
            Label label = new Label();
            label.FontSize = _fontSize;
            return label;
        }
    }
}
