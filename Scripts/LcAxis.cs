using LcChart.Scripts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LcChart
{
    /// <summary>
    /// 坐标轴
    /// </summary>
    public class LcAxis
    {
        /// <summary>
        /// 起点
        /// </summary>
        public Point Start = new(0, 0);
        /// <summary>
        /// 终点
        /// </summary>
        public Point End = new(0, 0);

        /// <summary>
        /// 区域宽度
        /// </summary>
        public double AreaWidth = 0;
        /// <summary>
        /// 区域高度
        /// </summary>
        public double AreaHeight = 0;

        /// <summary>
        /// 绘图区域宽度
        /// </summary>
        public double ChartAreaWidth
        {
            get
            {
                return AreaWidth - YLabelWidth - Margin - AxisExtra;
            }
        }

        /// <summary>
        /// 绘图区域高度
        /// </summary>
        public double ChartAreaHeight
        {
            get
            {
                return AreaHeight - XLabelHeight - Margin - AxisExtra;
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public double Min = 0;
        /// <summary>
        /// 最大值
        /// </summary>
        public double Max = 0;

        private double _step = 0;
        /// <summary>
        /// 间隔
        /// </summary>
        public double Step
        {
            get
            {
                return _step;
            }
            set
            {
                _stepCount = 0;
                _step = value;
            }
        }
        private int _stepCount = 0;
        /// <summary>
        /// 间隔数，小于2的情况下，不作处理，大于1的情况下，自动计算间距
        /// </summary>
        public int StepCount
        {
            get
            {
                return _stepCount;
            }
            set
            {
                _stepCount = value;
            }
        }

        /// <summary>
        /// 每间隔的画面长度
        /// </summary>
        public double StepLength = 0;
        /// <summary>
        /// X轴标签高度
        /// </summary>
        public double XLabelHeight = 100;
        /// <summary>
        /// Y轴标签宽度
        /// </summary>
        public double YLabelWidth = 100;
        /// <summary>
        /// 距离边界空白区的宽度
        /// </summary>
        public double Margin = 20;
        /// <summary>
        /// 轴额外长度
        /// </summary>
        public double AxisExtra = 30;
        /// <summary>
        /// 标签位置
        /// </summary>
        public List<Point> LabelPositions = new List<Point>();

        /// <summary>
        /// 箭头宽度
        /// </summary>
        public double ArrowWidth = 10;
        /// <summary>
        /// 箭头高度
        /// </summary>
        public double ArrowHeight = 15;
        /// <summary>
        /// 箭头圆角半径
        /// </summary>
        public double ArrowRadius = 3;

        /// <summary>
        /// 坐标轴标签
        /// </summary>
        public LcAxisLabel AxisLabel = new LcAxisLabel();

        /// <summary>
        /// 绘图区域
        /// </summary>
        public LcRect ChartArea
        {
            get
            {
                LcRect rect = new();
                rect.Left = YLabelWidth;
                rect.Right = rect.Left + ChartAreaWidth;
                rect.Top = Margin + AxisExtra;
                rect.Bottom = rect.Top + ChartAreaHeight;
                return rect;
            }
        }

        /// <summary>
        /// 获取比例
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetRate(double value)
        {
            if (AxisLabel.IsX && value >= 900)
            {

            }

            double rate = 0;
            if (Max > Min)
            {
                rate = (value - Min) / (Max - Min);
            }
            return rate;
        }

        /// <summary>
        /// 设置区域
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetArea(double width, double height)
        {
            AreaWidth = width;
            AreaHeight = height;
            AutoStartAndEnd();
        }

        public void AutoCalculateStepLength()
        {
            if (StepCount > 1)
            {
                int sv = (int)((Max - Min) / StepCount);
                if (AxisLabel.IsX)
                {
                    if (Min + sv * StepCount < Max)
                    {
                        sv++;
                        Max = Min + sv * StepCount;
                    }
                }

                //确定间隔数
                _step = (Max - Min) / StepCount;
                if (AxisLabel.IsX)
                {
                    StepLength = ChartAreaWidth / StepCount;
                }
                else
                {
                    StepLength = ChartAreaHeight / StepCount;
                }
            }
            else
            {
                //确定间隔值
                if (Step != 0)
                {
                    int count = (int)((Max - Min) / Step);
                    if (Min + count * Step < Max)
                    {
                        count++;
                        Max = Min + count * Step;
                    }

                    if (AxisLabel.IsX)
                    {
                        StepLength = ChartAreaWidth * Step / (Max - Min);
                    }
                    else
                    {
                        StepLength = ChartAreaHeight * Step / (Max - Min);
                    }
                }
                else
                {
                    StepLength = 0;
                }
            }
        }

        public void SetRange(double max, double min = 0)
        {
            Min = min;
            Max = max;
            AutoCalculateStepLength();
        }

        public void SetLabelSize(double width, double height)
        {
            YLabelWidth = width;
            XLabelHeight = height;
        }

        public void AutoLabelSize()
        {
            AxisLabel.CalculateLabelMax(LcChartTool.Double2String(Max, 2));
            YLabelWidth = AxisLabel.Width + AxisLabel.Margin + Margin;
            XLabelHeight = AxisLabel.Height + AxisLabel.Margin + Margin;
            AutoStartAndEnd();
        }

        public double GetValueByRate(double rate)
        {
            return (Max - Min) * rate + Min;
        }

        /// <summary>
        /// 自动计算终点
        /// </summary>
        public void AutoStartAndEnd()
        {
            if (AxisLabel.IsX)
            {
                Start.X = YLabelWidth;
                Start.Y = AreaHeight - XLabelHeight;
                End.X = AreaWidth - ArrowHeight - Margin;
                End.Y = Start.Y;
            }
            else
            {
                Start.X = YLabelWidth;
                Start.Y = AreaHeight - XLabelHeight;
                End.X = Start.X;
                End.Y = Margin + ArrowHeight;
            }
        }

        /// <summary>
        /// 根据数值获取坐标值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetCoordinateValue(double value)
        {
            double rate = GetRate(value);
            if (AxisLabel.IsX)
            {
                return Start.X + ChartAreaWidth * rate;
            }
            else
            {
                return Start.Y - ChartAreaHeight * rate;
            }
        }

        /// <summary>
        /// 更新标签文本
        /// </summary>
        public void UpdateLabelText()
        {
            AxisLabel.Set(Min, Step);
            AxisLabel.UpdateLabelText();
        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public void Show(Canvas parent)
        {
            if (Max == Min)
            {
                return;
            }

            AutoCalculateStepLength();
            if (StepLength > 0)
            {
                LabelPositions.Clear();
                LabelPositions.Add(new Point(Start.X, Start.Y));
                double max = 0;
                if (AxisLabel.IsX)
                {
                    max = ChartAreaWidth + StepLength * 0.5;
                }
                else
                {
                    max = ChartAreaHeight + StepLength * 0.5;
                }
                double currentStepPosition = StepLength;
                while (currentStepPosition < max)
                {
                    if (AxisLabel.IsX)
                    {
                        LabelPositions.Add(new Point(Start.X + currentStepPosition, Start.Y));
                    }
                    else
                    {
                        LabelPositions.Add(new Point(Start.X, Start.Y - currentStepPosition));
                    }
                    currentStepPosition += StepLength;
                }
            }

            AxisLabel.Set(Min, Step);
            AxisLabel.ShowLabels(parent, LabelPositions, AreaWidth);

            if (LabelPositions.Count > 1)
            {
                LcLineGeometry lineStep = new LcLineGeometry(Colors.LightGray, 1);
                for (int i = 1; i < LabelPositions.Count; i++)
                {
                    if (AxisLabel.IsX)
                    {
                        lineStep.AddLine(LabelPositions[i], new Point(LabelPositions[i].X, LabelPositions[i].Y - ChartAreaHeight));
                    }
                    else
                    {
                        lineStep.AddLine(LabelPositions[i], new Point(LabelPositions[i].X + ChartAreaWidth, LabelPositions[i].Y));
                    }
                }
                parent.Children.Add(lineStep.ToPath());
            }

            LcLineGeometry line = new LcLineGeometry(Colors.Black, 2);
            line.AddLine(Start, End);
            parent.Children.Add(line.ToPath());

            if (AxisLabel.IsX)
            {
                LcTriangle lct = new LcTriangle(new Point(AreaWidth - Margin - ArrowHeight, Start.Y), ArrowWidth, ArrowHeight);
                LcPathFigure lpf = new(lct.Get(LcTriangleDirection.Right, ArrowRadius), true);
                LcPathGeometry arrow = new LcPathGeometry(Colors.Black, 0);
                arrow.Fill(Colors.Black);
                arrow.AddFigure(lpf.Figure);
                parent.Children.Add(arrow.ToPath());
            }
            else
            {
                LcTriangle lct2 = new LcTriangle(new Point(Start.X, ArrowHeight + Margin), ArrowWidth, ArrowHeight);
                LcPathFigure lpf2 = new(lct2.Get(LcTriangleDirection.Up, ArrowRadius), true);
                LcPathGeometry arrow2 = new LcPathGeometry(Colors.Black, 0);
                arrow2.Fill(Colors.Black);
                arrow2.AddFigure(lpf2.Figure);
                parent.Children.Add(arrow2.ToPath());
            }
        }
    }
}
