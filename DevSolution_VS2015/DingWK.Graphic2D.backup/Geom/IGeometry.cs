using System.Collections.Generic;
using System.Windows;

namespace DingWK.Graphic2D.Geom
{

    public interface IGeometry
    {
        IGeometry Clone();

        List<Point>  GetGeometryPoints();

        bool SetGeometryByPoints(IList<Point> points);
    }
}
