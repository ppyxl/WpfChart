namespace LcChart
{
    /// <summary>
    /// Double集合处理工具
    /// </summary>
    public static class LcDoubleList
    {
        /// <summary>
        /// 获取范围
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static LcRect GetRect(List<double>? values)
        {
            LcRect rect = new();

            if (values != null && values.Count > 0)
            {
                rect.Left = 0;
                rect.Right = values.Count;

                var range = GetRange(values);
                rect.Top = range.Max;
                rect.Bottom = range.Min;
            }

            return rect;
        }

        /// <summary>
        /// 获取范围
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static LcDoubleRange GetRange(List<double>? values)
        {
            LcDoubleRange range = new();

            if (values != null && values.Count > 0)
            {
                range.Min = values[0];
                range.Max = values[0];

                for (int i = 1; i < values.Count; i++)
                {
                    if (values[i] > range.Max)
                    {
                        range.Max = values[i];
                    }
                    if (values[i] < range.Min)
                    {
                        range.Min = values[i];
                    }
                }
            }

            return range;
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double GetMin(List<double>? values)
        {
            if (values != null && values.Count > 0)
            {
                double vv = values[0];
                for (int i = 1; i < values.Count; i++)
                {
                    if (values[i] < vv)
                    {
                        vv = values[i];
                    }
                }
                return vv;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double GetMax(List<double>? values)
        {
            if (values != null && values.Count > 0)
            {
                double vv = values[0];
                for (int i = 1; i < values.Count; i++)
                {
                    if (values[i] > vv)
                    {
                        vv = values[i];
                    }
                }
                return vv;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double GetMin(List<double>? values, int startIndex, int endIndex)
        {
            if (values != null && values.Count > 0 && startIndex >= 0 && startIndex < values.Count && endIndex >= 0 && endIndex < values.Count)
            {
                double vv = 0;
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i == startIndex)
                    {
                        vv = values[i];
                    }
                    else
                    {
                        if (values[i] < vv)
                        {
                            vv = values[i];
                        }
                    }
                }
                return vv;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double GetMax(List<double>? values, int startIndex, int endIndex)
        {
            if (values != null && values.Count > 0 && startIndex >= 0 && startIndex < values.Count && endIndex >= 0 && endIndex < values.Count)
            {
                double vv = 0;
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i == startIndex)
                    {
                        vv = values[i];
                    }
                    else
                    {
                        if (values[i] > vv)
                        {
                            vv = values[i];
                        }
                    }
                }
                return vv;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 滤波处理七点三次函数拟合平滑,次数越多越光滑
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<double> CubicSmooth7(List<double> values)
        {
            if (values.Count < 7)
            {
                return values;
            }

            int N = values.Count;

            List<double> b = new List<double>();
            for (int i = 0; i < N; i++)
            {
                b.Add(0);
            }

            b[0] = (39.0 * values[0] + 8.0 * values[1] - 4.0 * values[2] - 4.0 * values[3] + 1.0 * values[4] + 4.0 * values[5] - 2.0 * values[6]) / 42.0;
            b[1] = (8.0 * values[0] + 19.0 * values[1] + 16.0 * values[2] + 6.0 * values[3] - 4.0 * values[4] - 7.0 * values[5] + 4.0 * values[6]) / 42.0;
            b[2] = (-4.0 * values[0] + 16.0 * values[1] + 19.0 * values[2] + 12.0 * values[3] + 2.0 * values[4] - 4.0 * values[5] + 1.0 * values[6]) / 42.0;
            for (int i = 3; i <= N - 4; i++)
            {
                b[i] = (-2.0 * (values[i - 3] + values[i + 3]) + 3.0 * (values[i - 2] + values[i + 2]) + 6.0 * (values[i - 1] + values[i + 1]) + 7.0 * values[i]) / 21.0;
            }
            b[N - 3] = (-4.0 * values[N - 1] + 16.0 * values[N - 2] + 19.0 * values[N - 3] + 12.0 * values[N - 4] + 2.0 * values[N - 5] - 4.0 * values[N - 6] + 1.0 * values[N - 7]) / 42.0;
            b[N - 2] = (8.0 * values[N - 1] + 19.0 * values[N - 2] + 16.0 * values[N - 3] + 6.0 * values[N - 4] - 4.0 * values[N - 5] - 7.0 * values[N - 6] + 4.0 * values[N - 7]) / 42.0;
            b[N - 1] = (39.0 * values[N - 1] + 8.0 * values[N - 2] - 4.0 * values[N - 3] - 4.0 * values[N - 4] + 1.0 * values[N - 5] + 4.0 * values[N - 6] - 2.0 * values[N - 7]) / 42.0;

            return b;
        }
    }
}
