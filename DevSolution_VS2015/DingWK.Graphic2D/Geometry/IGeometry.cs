using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Geometry
{
    public interface IGeometry
    {
        IGeometry Clone();

        List<Point> GetGeometryTransformPoints();

        bool SetGeometryByTransformPoints(IList<Point> points);

        void Drawing(DrawingContext dc, Brush brush, Pen pen);

    }
}