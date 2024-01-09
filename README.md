# WpfChart\r\n
A free, pure C # developed WPF drawing plugin，It can directly draw a Double List as a curve.\r\n
#How to Use\r\n
1, Declare an object\r\n
private LcLineChartView _chart = null;\r\n
2, Initialization
_chart = new LcLineChartView(ChartGrid, 2);
_chart.ShowBorder();
_chart.LimitMaxY();
3，Define some functions
public void SetLine(List<double> values, int index = 0)
{
    _chart.Lines.Add(GetColor(index));
    _chart.Lines.Set(index, values);
}

public void ShowLine()
{
    _chart.Lines.Process(true);
    _chart.Lines.Smooth(100);
    _chart.Show();
}

private Color GetColor(int index)
{
    switch (index)
    {
        case 0:
            return Colors.CornflowerBlue;
        case 1:
            return Colors.Green;
        case 2:
            return Colors.Orange;
        default:
            return Colors.Red;
    }
}
4，Draw Lines
List<double> vs=new List<double>(){……};
SetLine(vs);
ShowLine();
