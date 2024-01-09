using System.Windows.Controls;
using System.Windows.Media;

namespace LcChart.Scripts
{
    /// <summary>
    /// 曲线管理
    /// </summary>
    public class LcLineManage
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
        /// 所有曲线
        /// </summary>
        public List<LcLine> Lines = new();
        /// <summary>
        /// 曲线最小X
        /// </summary>
        public double MinX
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].LineMinX;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value > Lines[i].LineMinX)
                        {
                            value = Lines[i].LineMinX;
                        }
                    }
                }

                return value;
            }
        }
        /// <summary>
        /// 曲线最大X
        /// </summary>
        public double MaxX
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].LineMaxX;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value < Lines[i].LineMaxX)
                        {
                            value = Lines[i].LineMaxX;
                        }
                    }
                }

                return value;
            }
        }
        /// <summary>
        /// 曲线最小Y
        /// </summary>
        public double MinY
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].LineMinY;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value > Lines[i].LineMinY)
                        {
                            value = Lines[i].LineMinY;
                        }
                    }
                }

                return value;
            }
        }
        /// <summary>
        /// 曲线最大Y
        /// </summary>
        public double MaxY
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].LineMaxY;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value < Lines[i].LineMaxY)
                        {
                            value = Lines[i].LineMaxY;
                        }
                    }
                }

                return value;
            }
        }
        /// <summary>
        /// Y轴最大值
        /// </summary>
        public double AxisMaxY
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].AxisMaxY;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value < Lines[i].AxisMaxY)
                        {
                            value = Lines[i].AxisMaxY;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// 原始数据最小值
        /// </summary>
        public double DataMin
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].DataRange.Min;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value > Lines[i].DataRange.Min)
                        {
                            value = Lines[i].DataRange.Min;
                        }
                    }
                }

                return value;
            }
        }
        /// <summary>
        /// 原始数据最大值
        /// </summary>
        public double DataMax
        {
            get
            {
                double value = 0;

                if (Lines != null && Lines.Count > 0)
                {
                    value = Lines[0].DataRange.Max;
                    for (int i = 1; i < Lines.Count; i++)
                    {
                        if (value < Lines[i].DataRange.Max)
                        {
                            value = Lines[i].DataRange.Max;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// 曲线名称
        /// </summary>
        public List<string>? LineNames = null;

        /// <summary>
        /// 是否限定最大Y值
        /// </summary>
        private bool _isLimitMaxY = false;
        /// <summary>
        /// 限定的最大Y值
        /// </summary>
        private double _limitMaxY = 100;

        /// <summary>
        /// 构造器
        /// </summary>
        public LcLineManage()
        { }

        /// <summary>
        /// 获取曲线名称
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetName(int index)
        {
            if (LineNames != null && LineNames.Count > 0 && index >= 0 && index < LineNames.Count)
            {
                return LineNames[index];
            }
            else
            {
                return "Line" + (index + 1).ToString();
            }
        }

        /// <summary>
        /// 添加曲线
        /// </summary>
        /// <param name="color"></param>
        public void Add(Color color)
        {
            Add(GetName(Lines.Count), color);
        }

        /// <summary>
        /// 添加曲线
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public void Add(string name, Color color)
        {
            LcLine line = new();
            line.LineName = name;
            line.LineColor = color;
            line.GetLeft = GetLeft;
            line.GetTop = GetTop;
            if (_isLimitMaxY)
            {
                line.LimitMaxY(_limitMaxY);
            }
            Lines.Add(line);
        }

        /// <summary>
        /// 设置最大Y值
        /// </summary>
        /// <param name="max"></param>
        public void LimitMaxY(double max = 100)
        {
            _isLimitMaxY = true;
            _limitMaxY = max;

            if (Lines != null && Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].LimitMaxY(_limitMaxY);
                }
            }
        }

        /// <summary>
        /// 计算数据
        /// </summary>
        public void Process(bool isSub, int subIndex = -1)
        {
            if (Lines != null && Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (isSub)
                    {
                        Lines[i].ProcessWithSub(subIndex);
                    }
                    else
                    {
                        Lines[i].Process();
                    }
                }
            }
        }

        /// <summary>
        /// 光滑曲线数据
        /// </summary>
        /// <param name="times"></param>
        public void Smooth(int times)
        {
            if (Lines != null && Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    Lines[i].Smooth(times);
                }
            }
        }

        /// <summary>
        /// 设置曲线
        /// </summary>
        /// <param name="index"></param>
        /// <param name="values"></param>
        public void Set(int index, List<double>? values)
        {
            if (Lines != null && Lines.Count > 0 && index >= 0 && index < Lines.Count)
            {
                Lines[index].SetData(values);
            }
        }

        /// <summary>
        /// 设置曲线
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        public void Set(string name, List<double>? values)
        {
            if (Lines != null && Lines.Count > 0)
            {
                for (int i = 0; i < Lines.Count; i++)
                {
                    if (LcChartTool.IsSame(Lines[i].LineName, name))
                    {
                        Lines[i].SetData(values);
                    }
                }
            }
        }

        /// <summary>
        /// 显示曲线
        /// </summary>
        /// <param name="parent"></param>
        public void Show(Canvas parent, double lineWidth)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Show(parent, lineWidth);
            }
        }
    }
}
