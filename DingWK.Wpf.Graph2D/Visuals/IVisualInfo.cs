using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Visuals
{
    public interface IVisualInfo
    {
        Point Origin { get; }

        double Angle { get; }

        Brush Fill { get; }

        Pen Stroke { get; }
    }
}
