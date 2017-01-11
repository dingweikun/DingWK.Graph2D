using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public interface IBoundVisual
    {
        Rect Bound { get; }
        Transform BoundTransform { get; }
    }
}
