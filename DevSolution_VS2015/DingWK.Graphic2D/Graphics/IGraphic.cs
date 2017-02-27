using System.Windows;
using System;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphics
{
    public interface IGraphic
    {
        Guid ID { get; }
        
        Point Origin { get; set; }

        double Angle { get; set; }

        Brush Fill { get; set; }

        Pen Stroke { get; set; }

    }
}
