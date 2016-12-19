using System.Windows.Media;

namespace Graphic2D.Kernel.Graphics
{
    public interface IGraphic
    {
        IGraphic DefaultGraphic { get; } 

        void Drawing(DrawingContext dc, Brush fill, Pen stroke);
    }
}
