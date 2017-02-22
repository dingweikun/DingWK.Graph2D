using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public interface IVisualInfo
    {
        Brush Fill { get; }

        Pen Stroke { get; }

        Point Origin { get; }

        double Angle { get; }
    }
}
