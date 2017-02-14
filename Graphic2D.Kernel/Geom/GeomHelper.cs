using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Geom
{
    public static class GeomHelper
    {
        public static void ScaleGeom(ref IGeom geom, double scaleX, double scaleY, Point refer)
        {
            Matrix mx = new ScaleTransform(scaleX, scaleY, refer.X, refer.Y).Value;
            Point[] pots = geom.ToPoints();
            mx.Transform(pots);
            geom.SetByPoints(pots);
        }

    }
}
