using LcChart.Scripts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LcChart
{
    /// <summary>
    /// LcLineChartView.xaml 的交互逻辑
    /// </summary>
    public partial class LcLineChartView : UserControl
    {
        /// <summary>
        /// 所有曲线
        /// </summary>
        public LcLineManage Lines = new();
        /// <summary>
        /// X轴
        /// </summary>
        public LcAxisX AxisX = new();
        /// <summary>
        /// Y轴
        /// </summary>
        public LcAxisY AxisY = new();
        /// <summary>
        /// 光标追踪
        /// </summary>
        public LcCursorMark CursorMark = new();

        /// <summary>
        /// X轴标签高度
        /// </summary>
        private double _xLabelHeight = 50;
        /// <summary>
        /// Y轴标签宽度
        /// </summary>
        private double _yLabelWidth = 100;
        /// <summary>
        /// 绘图区域
        /// </summary>
        private LcRect _chartArea = new();
        /// <summary>
        /// 曲线宽度
        /// </summary>
        private double _lineWidth = 1;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="parent"></param>
        public LcLineChartView(Grid parent,double lineWidth)
        {
            _lineWidth = lineWidth;
            if (_lineWidth <= 0)
            {
                _lineWidth = 1;
            }

            InitializeComponent();

            Lines.GetLeft = GetLeft;
            Lines.GetTop = GetTop;

            AxisX.XLabelHeight = _xLabelHeight;
            AxisX.YLabelWidth = _yLabelWidth;
            AxisX.StepCount = 10;

            AxisY.XLabelHeight = _xLabelHeight;
            AxisY.YLabelWidth = _yLabelWidth;
            AxisY.StepCount = 10;

            CursorMark.SetParent(MarkPanel);

            SizeChanged += (sender, e) =>
            {
                Show();
            };

            parent.Children.Add(this);
        }

        /// <summary>
        /// 获取横坐标
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double GetLeft(double value)
        {
            return AxisX.GetCoordinateValue(value);
        }

        /// <summary>
        /// 获取纵坐标
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double GetTop(double value)
        {
            return AxisY.GetCoordinateValue(value);
        }

        /// <summary>
        /// 移除所有曲线
        /// </summary>
        public void ClearAllLine()
        {
            LinePanel.Children.Clear();
            Lines.Lines.Clear();
        }

        /// <summary>
        /// 设定最大Y值
        /// </summary>
        /// <param name="max"></param>
        public void LimitMaxY(double max = 100)
        {
            Lines.LimitMaxY(max);
            AxisY.Max = max;
        }

        /// <summary>
        /// 减去指定值，并展示
        /// </summary>
        /// <param name="value"></param>
        public void ShowWithProcess(bool isSub = false, int subIndex = -1)
        {
            Process(isSub, subIndex);
            Show();
        }

        /// <summary>
        /// 显示图形
        /// </summary>
        public void Show()
        {
            double maxX = Lines.MaxX;
            if (maxX <= 0)
            {
                maxX = 600;
            }
            AxisX.SetRange(maxX);
            double maxY = Lines.AxisMaxY;
            if (maxY <= 0)
            {
                maxY = 100;
            }
            AxisY.SetRange(maxY);
            UpdateCoordinates();
            Lines.Show(LinePanel, _lineWidth);
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        public void Process(bool isSub, int subIndex = -1)
        {
            Lines.Process(isSub, subIndex);
        }

        /// <summary>
        /// 设置X轴间隔
        /// </summary>
        /// <param name="step"></param>
        public void SetStepX(double step)
        {
            AxisX.Step = step;
        }

        /// <summary>
        /// 显示边框
        /// </summary>
        public void ShowBorder()
        {
            MainBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
            MainBorder.BorderThickness = new Thickness(1);
            MainBorder.CornerRadius = new CornerRadius(5);
        }

        /// <summary>
        /// 更新坐标轴
        /// </summary>
        public void UpdateCoordinates()
        {
            double ww = CoordinatePanel.ActualWidth;
            double hh = CoordinatePanel.ActualHeight;
            if (ww > 200 && hh > 200)
            {
                CoordinatePanel.Children.Clear();

                AxisX.AutoLabelSize();
                AxisY.AutoLabelSize();
                AxisX.SetLabelSize(AxisY.YLabelWidth, AxisX.XLabelHeight);
                AxisY.SetLabelSize(AxisY.YLabelWidth, AxisX.XLabelHeight);

                AxisX.SetArea(ww, hh);
                AxisX.Show(CoordinatePanel);

                AxisY.SetArea(ww, hh);
                AxisY.Show(CoordinatePanel);

                _yLabelWidth = AxisY.YLabelWidth;
                _xLabelHeight = AxisX.XLabelHeight;

                _chartArea = AxisX.ChartArea;

                CursorMark.SetArea(_chartArea);
            }
        }

        /// <summary>
        /// 当鼠标移动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPanel_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition(CoordinatePanel);
            if (_chartArea.IsInRange(p.X, p.Y))
            {
                Log(LcChartTool.Double2String(AxisX.GetValueByRate(_chartArea.GetXrate(p.X)), 2) + "，" + LcChartTool.Double2String(AxisY.GetValueByRate(_chartArea.GetYrate(p.Y)), 2));
                CursorMark.Show(p.X, p.Y);
            }
            else
            {
                Log(null);
                CursorMark.Hide();
            }
        }

        /// <summary>
        /// 当鼠标进入时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        /// <summary>
        /// 当鼠标离开时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CursorMark.Hide();
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="msg"></param>
        private void Log(string? msg)
        {
            MainLabel.Content = msg;
        }
    }
}
