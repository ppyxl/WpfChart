namespace LcChart
{
    public static class LcChartTool
    {
        /// <summary>
        /// 小数字符串去除末尾无用的0
        /// </summary>
        /// <param name="vv"></param>
        /// <returns></returns>
        public static string DecimalStringRemoveZero(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Contains('.'))
                {
                    int zeroCount = 0;
                    for (int i = value.Length - 1; i >= 0; i--)
                    {
                        if (value[i] == '0')
                        {
                            zeroCount++;
                        }
                        else if (value[i] == '.')
                        {
                            zeroCount++;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (zeroCount > 0)
                    {
                        value = value.Substring(0, value.Length - zeroCount);
                    }
                }
            }
            return value ?? string.Empty;
        }

        /// <summary>
        /// double转为字符串，去掉末尾无意义的0
        /// </summary>
        /// <returns></returns>
        public static string Double2String(double value)
        {
            return DecimalStringRemoveZero(value.ToString());
        }

        /// <summary>
        /// 最多保留多少位小数
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="maxNc"></param>
        /// <returns></returns>
        public static string Double2String(double dd, int maxNc)
        {
            if (maxNc < 0)
            {
                maxNc = 0;
            }

            string value = dd.ToString();
            int pointIndex = value.IndexOf('.');
            if (pointIndex > 0)
            {
                if (value.Length > pointIndex + 1 + maxNc)
                {
                    if (value[pointIndex + 1 + maxNc] > '4')
                    {
                        value = (dd + Double1(maxNc)).ToString();
                    }
                }
                //小数位长度
                int smallLength = value.Length - pointIndex - 1;
                if (smallLength > maxNc)
                {
                    value = value[..(pointIndex + 1 + maxNc)];
                }
                value = DecimalStringRemoveZero(value);
            }
            return value;
        }

        /// <summary>
        /// 0.0……1
        /// </summary>
        /// <param name="length">小数点后多少位</param>
        /// <returns></returns>
        public static double Double1(int length)
        {
            if (length < 1)
            {
                return 1;
            }
            else
            {
                string ss = "0.";
                for (int i = 0; i < length - 1; i++)
                {
                    ss += "0";
                }
                ss += "1";
                return double.Parse(ss);
            }
        }

        public static bool IsSame(string? v1, string? v2)
        {
            if (v1 != null && v2 != null)
            {
                if (v1.Length != v2.Length)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < v1.Length; i++)
                    {
                        if (v1[i] != v2[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            else
            {
                if (v1 == null && v2 == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
