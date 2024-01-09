namespace LcChart
{
    /// <summary>
    /// 二维点
    /// </summary>
    public struct LcPoint
    {
        /// <summary>
        /// X坐标值
        /// </summary>
        public double X = 0;
        /// <summary>
        /// Y坐标值
        /// </summary>
        public double Y = 0;

        /// <summary>
        /// 构造器
        /// </summary>
        public LcPoint()
        { }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public LcPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="p"></param>
        public LcPoint(LcPoint p)
        {
            X = p.X;
            Y = p.Y;
        }

        /// <summary>
        /// 获取两点之间的距离
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(LcPoint start, LcPoint end)
        {
            return GetDistance(start.X, start.Y, end.X, end.Y);
        }

        /// <summary>
        /// 获取两点之间的距离
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            if (dx < 0)
            {
                dx = -dx;
            }
            double dy = y2 - y1;
            if (dy < 0)
            {
                dy = -dy;
            }
            if (dx == 0 && dy == 0)
            {
                return 0;
            }
            else if (dx == 0)
            {
                return dy;
            }
            else if (dx == 0)
            {
                return dx;
            }
            else
            {
                //三角函数 a^2+b^2=c^2
                return Math.Sqrt(dx * dx + dy * dy);
            }
        }

        /// <summary>
        /// 获取中点
        /// </summary>
        /// <returns></returns>
        public static LcPoint GetMidPoint(LcPoint start, LcPoint end)
        {
            return GetMidPoint(start.X, start.Y, end.X, end.Y);
        }

        /// <summary>
        /// 获取中点
        /// </summary>
        /// <returns></returns>
        public static LcPoint GetMidPoint(double x1, double y1, double x2, double y2)
        {
            return GetPointByRate(x1, y1, x2, y2, 0.5);
        }

        /// <summary>
        /// 获取点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static LcPoint GetPointByRate(LcPoint start, LcPoint end, double rate)
        {
            return GetPointByRate(start.X, start.Y, end.X, end.Y, rate);
        }

        /// <summary>
        /// 获取点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static LcPoint GetPointByRate(double x1, double y1, double x2, double y2, double rate)
        {
            return new LcPoint(x1 + (x2 - x1) * rate, y1 + (y2 - y1) * rate);
        }

        /// <summary>
        /// 获取距离终点或起点多少距离的点
        /// </summary>
        /// <returns></returns>
        public static LcPoint GetPointByDistance(LcPoint start, LcPoint end, double distance, bool isFromEnd = true)
        {
            return GetPointByDistance(start.X, start.Y, end.X, end.Y, distance, isFromEnd);
        }

        /// <summary>
        /// 获取距离终点或起点多少距离的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static LcPoint GetPointByDistance(double x1, double y1, double x2, double y2, double distance, bool isFromEnd = true)
        {
            double rate = 0;
            double distance0 = GetDistance(x1, y1, x2, y2);
            if (distance0 > 0)
            {
                if (isFromEnd)
                {
                    rate = (distance0 - distance) / distance0;
                }
                else
                {
                    rate = distance / distance0;
                }
            }
            return GetPointByRate(x1, y1, x2, y2, rate);
        }
    }
}
