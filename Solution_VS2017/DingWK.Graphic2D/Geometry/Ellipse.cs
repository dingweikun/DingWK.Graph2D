using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Geometry
{
    public class Ellipse : IGeometry
    {
        #region fields & prperties

        Point _center;
        double _radiusX;
        double _radiusY;

        public Point Center
        {
            get => _center;
            set => _center = value;
        }

        public double RadiusX
        {
            get => _radiusX;
            set => _radiusX = value;
        }

        public double RadiusY
        {
            get => _radiusY;
            set => _radiusY = value;
        }

        #endregion


        #region constructors

        public Ellipse(Point center, double radiusX, double radiusY)
        {
            _center = center;
            _radiusX = radiusX;
            _radiusY = radiusY;
        }

        public Ellipse()
                : this(new Point(0, 0), 0, 0)
        { }

        #endregion


        #region IGeometry implementation

        public IGeometry Clone() => new Ellipse(Center, RadiusX, RadiusY);

        public List<Point> GetGeometryTransformPoints() => new List<Point>() { Center, new Point(RadiusX, RadiusY) };

        public bool SetGeometryByTransformPoints(IList<Point> points)
        {
            if (points?.Count >= 2)
            {
                Center = points[0];
                RadiusX = points[1].X;
                RadiusY = points[1].Y;
                return true;
            }
            else
                return false;
        }

        public void Drawing(DrawingContext dc, Brush brush, Pen pen) => dc.DrawEllipse(brush, pen, Center, RadiusX, RadiusY);

        #endregion

    }
}
