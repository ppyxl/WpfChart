using LcChart.Scripts;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LcChart
{
    /// <summary>
    /// 获取值
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public delegate double GetDouble(double value);

    /// <summary>
    /// 折线
    /// </summary>
    public class LcLine
    {
        /// <summary>
        /// 根据数值计算X轴坐标值
        /// </summary>
        public GetDouble? GetLeft = null;
        /// <summary>
        /// 根据数值计算Y轴坐标值
        /// </summary>
        public GetDouble? GetTop = null;

        /// <summary>
        /// 曲线名称
        /// </summary>
        public string LineName = string.Empty;
        /// <summary>
        /// 曲线颜色
        /// </summary>
        public Color LineColor = Colors.CornflowerBlue;
        /// <summary>
        /// 曲线数据
        /// </summary>
        public List<double>? LineData = null;
        /// <summary>
        /// 当前曲线最小X值
        /// </summary>
        public double LineMinX = 0;
        /// <summary>
        /// 当前曲线最大X值
        /// </summary>
        public double LineMaxX = 0;
        /// <summary>
        /// 当前曲线最小Y值
        /// </summary>
        public double LineMinY = 0;
        /// <summary>
        /// 当前曲线最大Y值
        /// </summary>
        public double LineMaxY = 0;

        /// <summary>
        /// 轴最大比率
        /// </summary>
        private const double AxisMaxYRate = 1.05;
        /// <summary>
        /// Y轴最大值
        /// </summary>
        public double AxisMaxY
        {
            get
            {
                if (_isLimitMax)
                {
                    return _limitMax;
                }
                else
                {
                    return LineMaxY * AxisMaxYRate;
                }
            }
        }

        /// <summary>
        /// 原始数据
        /// </summary>
        public List<double>? Data = null;
        /// <summary>
        /// 原始数据取值范围
        /// </summary>
        public LcDoubleRange DataRange = new();

        /// <summary>
        /// 生成点
        /// </summary>
        private List<LcPoint>? _points = null;
        /// <summary>
        /// 曲线的轨迹
        /// </summary>
        private Path? _path = null;
        /// <summary>
        /// 是否指定最大值
        /// </summary>
        private bool _isLimitMax = false;
        /// <summary>
        /// 指定的最大值
        /// </summary>
        private double _limitMax = 100;

        /// <summary>
        /// 构造器
        /// </summary>
        public LcLine()
        { }

        /// <summary>
        /// 设置最大值
        /// </summary>
        /// <param name="value"></param>
        public void LimitMaxY(double value = 100)
        {
            _isLimitMax = true;
            _limitMax = value;
            if (_limitMax <= 0)
            {
                _limitMax = 100;
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="values"></param>
        public void SetData(List<double>? values)
        {
            Data = values;
            DataRange = LcDoubleList.GetRange(Data);
            LineData = values;
            GetLineDataRange();
        }

        /// <summary>
        /// 不减去，原始值处理
        /// </summary>
        public void Process()
        {
            if (_isLimitMax)
            {
                if (Data != null && Data.Count > 1)
                {
                    double dax = DataRange.Max;
                    if (dax != 0)
                    {
                        List<double> rates = new();
                        for (int i = 0; i < Data.Count; i++)
                        {
                            rates.Add(Data[i] / dax);
                        }
                        LcDoubleRange rateRange = LcDoubleList.GetRange(rates);
                        double rateRangeLength = rateRange.Max * AxisMaxYRate;
                        double rateStep = _limitMax / rateRangeLength;
                        LineData = new List<double>();
                        for (int i = 0; i < rates.Count; i++)
                        {
                            LineData.Add(rates[i] * rateStep);
                        }
                    }
                }
            }
            GetLineDataRange();
        }

        /// <summary>
        /// 整体减去指定值，默认减去最小值
        /// </summary>
        /// <param name="subIndex"></param>
        public void ProcessWithSub(int subIndex = -1)
        {
            if (_isLimitMax)
            {
                if (Data != null && Data.Count > 1)
                {
                    double dax = DataRange.Max;
                    if (dax != 0)
                    {
                        List<double> rates = new();
                        for (int i = 0; i < Data.Count; i++)
                        {
                            rates.Add(Data[i] / dax);
                        }
                        LcDoubleRange rateRange = LcDoubleList.GetRange(rates);
                        double subValue = rateRange.Min;//默认为最小值
                        if (subIndex >= 0 && subIndex < rates.Count)
                        {
                            //如果可以，指定值
                            subValue = rates[subIndex];
                        }
                        double rateRangeLength = (rateRange.Max - subValue) * AxisMaxYRate;
                        double rateStep = _limitMax / rateRangeLength;
                        LineData = new List<double>();
                        for (int i = 0; i < rates.Count; i++)
                        {
                            double value = (rates[i] - subValue) * rateStep;
                            if (value < 0)
                            {
                                value = 0;
                            }
                            LineData.Add(value);
                        }
                    }
                }
            }
            GetLineDataRange();
        }

        /// <summary>
        /// 光滑曲线
        /// </summary>
        /// <param name="times"></param>
        public void Smooth(int times = 1)
        {
            if (LineData != null && LineData.Count > 7 && times > 0)
            {
                for (int i = 0; i < times; i++)
                {
                    LineData = LcDoubleList.CubicSmooth7(LineData);
                }
                GetLineDataRange();
            }
        }

        /// <summary>
        /// 获取当前曲线数据的范围
        /// </summary>
        private void GetLineDataRange()
        {
            var rect = LcDoubleList.GetRect(LineData);
            LineMinX = rect.Left;
            LineMaxX = rect.Right;
            LineMinY = rect.Bottom;
            LineMaxY = rect.Top;
        }

        /// <summary>
        /// 获取当前曲线数据的范围
        /// </summary>
        private void GetRange(double maxY)
        {
            var rect = LcDoubleList.GetRect(LineData);
            LineMinX = rect.Left;
            LineMaxX = rect.Right;
            LineMinY = maxY;
            LineMaxY = rect.Top;
        }

        /// <summary>
        /// 创建曲线的点
        /// </summary>
        private void CreatePoints()
        {
            _points = new List<LcPoint>();

            if (LineData != null && LineData.Count > 0)
            {
                if (GetLeft != null && GetTop != null)
                {
                    for (int i = 0; i < LineData.Count; i++)
                    {
                        double left = GetLeft(i);
                        double top = GetTop(LineData[i]);
                        _points.Add(new LcPoint(left, top));
                    }
                }
            }
        }

        /// <summary>
        /// 展示曲线
        /// </summary>
        /// <param name="parent"></param>
        public void Show(Canvas parent, double lineWidth)
        {
            if (lineWidth <= 0)
            {
                lineWidth = 1;
            }

            CreatePoints();

            if (_path != null)
            {
                parent.Children.Remove(_path);
                _path = null;
            }

            if (_points != null && _points.Count > 1)
            {
                LcPathFigure lpf = new(_points[0].X, _points[0].Y, false);
                for (int i = 1; i < _points.Count; i++)
                {
                    lpf.AddLine(_points[i].X, _points[i].Y);
                }
                LcPathGeometry lpg = new(LineColor, lineWidth);
                lpg.AddFigure(lpf.Figure);
                _path = lpg.ToPath();
            }

            if (_path != null)
            {
                parent.Children.Add(_path);
            }
        }
    }
}
