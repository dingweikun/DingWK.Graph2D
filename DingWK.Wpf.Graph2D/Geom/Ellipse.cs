using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Geom
{
    public class Ellipse : Geometry
    {
        #region fields & properties

        Point _center;
        double _radiusX;
        double _radiusY;

        public Point Center
        {
            get { return _center; }
            set { _center = value; }
        }

        public double RadiusX
        {
            get { return _radiusX; }
            set { _radiusX = value; }
        }

        public double RadiusY
        {
            get { return _radiusY; }
            set { _radiusY = value; }
        }

        public double Width
        {
            get { return _radiusX * 2; }
            set { _radiusX = value / 2; }
        }

        public double Height
        {
            get { return _radiusY * 2; }
            set { _radiusY = value / 2; }
        }

        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        #endregion


        #region constructors

        public Ellipse(Point center, double radiusX, double radiusY)
        {
            _center = center;
            _radiusX = radiusX;
            _radiusY = radiusY;
        }

        public Ellipse(double centerX, double centerY, double radiusX, double radiusY)
            : this(new Point(centerX, centerY), radiusX, radiusY)
        {
        }

        public Ellipse(Point center, Size size)
            : this(center, size.Width / 2, size.Height / 2)
        {
        }

        public Ellipse()
            : this(new Point(0, 0), 0, 0)
        {
        }

        #endregion


        // override metheds

        public override void Draw(DrawingContext drawingContext, Brush fill, Pen stroke)
        {
            drawingContext?.DrawEllipse(fill, stroke, _center, _radiusX, _radiusY);
        }

        public override List<Point> GetGeometryPoints()
        {
            return new List<Point>() { _center, new Point(_radiusX, _radiusY) };
        }

        public override bool SetGeometryByPoints(IList<Point> points)
        {
            if (points?.Count >= 2)
            {
                _center = points[0];
                _radiusX = points[1].X;
                _radiusY = points[1].Y;
                return true;
            }
            else
                return false;
        }

    }
}
