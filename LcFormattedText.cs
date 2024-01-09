using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LcChart
{
    /// <summary>
    /// 用于计算文本长度
    /// </summary>
    public class LcFormattedText
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get
            {
                if (_ft != null)
                {
                    return _ft.Width;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get
            {
                if (_ft != null)
                {
                    return _ft.Height;
                }
                else
                {
                    return 0;
                }
            }
        }

        private FormattedText? _ft = null;

        public LcFormattedText(string? text, Label label, double pixelsPerDip = 1)
        {
            if (!string.IsNullOrEmpty(text))
            {
                _ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(label.FontFamily, label.FontStyle, label.FontWeight, label.FontStretch), label.FontSize, label.Foreground, pixelsPerDip);
            }
        }

        public LcFormattedText(string? text, double fontSize, FontFamily ff, FontStyle fsty, FontWeight fw, FontStretch fstr, Brush fontBrush, double pixelsPerDip = 1)
        {
            if (!string.IsNullOrEmpty(text))
            {
                _ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(ff, fsty, fw, fstr), fontSize, fontBrush, pixelsPerDip);
            }
        }
    }
}
