using System;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphic
{
    public interface IGraphicVisual
    {
        Guid Guid { get; }
        IPlacement Placement { get; set; }
        Pen Stroke { get; set; }
        Brush Fill { get; set; }
    }
}
