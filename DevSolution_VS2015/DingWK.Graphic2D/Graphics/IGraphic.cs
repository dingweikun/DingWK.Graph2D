using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphics
{
    public interface IGraphic
    {
        Point Origin { get; }

        double Angle { get; }

        Brush Fill { get; }

        Pen Stroke { get; }

    }
}
