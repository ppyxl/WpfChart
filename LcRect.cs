namespace LcChart
{
    /// <summary>
    /// 定位，最左为0，最上为0，左上角最小，右下角最大；
    /// 范围，X，最小Left，最大Right；Y，最小Bottom，最大Top；
    /// </summary>
    public struct LcRect
    {
        public double Left = 0;
        public double Top = 0;
        public double Right = 0;
        public double Bottom = 0;

        public LcRect()
        { }

        /// <summary>
        /// 是否在矩形的范围内
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public bool IsInRange(double left, double top)
        {
            if (left >= Left && left <= Right && top >= Top && top <= Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取X轴长度值
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public double GetX(double left)
        {
            return left - Left;
        }

        /// <summary>
        /// 获取Y轴长度值
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public double GetY(double top)
        {
            return Bottom - top;
        }

        public double GetXrate(double left)
        {
            if (Right > Left)
            {
                return (left - Left) / (Right - Left);
            }
            else
            {
                return 0;
            }
        }

        public double GetYrate(double top)
        {
            if (Bottom > Top)
            {
                return (Bottom - top) / (Bottom - Top);
            }
            else
            {
                return 0;
            }
        }
    }
}
