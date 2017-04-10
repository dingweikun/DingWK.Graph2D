using System.Windows;

namespace DingWK.Graphic2D.Graphic
{
    public interface IPlacement
    {
        Point Origin { get; }

        double Angle { get; }
    }
}
