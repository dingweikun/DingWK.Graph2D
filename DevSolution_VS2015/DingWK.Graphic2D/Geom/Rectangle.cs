using System.Collections.Generic;
using System.Windows;

namespace DingWK.Graphic2D.Geom
{
    /// <summary>
    /// Rounded rectangle.
    /// </summary>
    public class Rectangle : IGeometry
    {

        #region fields & prperties

        Rect _rect;
        double _radiusX;
        double _radiusY;

        public Rect Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public Size Size
        {
            get { return _rect.Size; }
            set { _rect.Size = value; }
        }

        public double X
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }

        public double Y
        {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }

        public double Width
        {
            get { return _rect.Width; }
            set { _rect.Width = value; }
        }

        public double Height
        {
            get { return _rect.Height; }
            set { _rect.Height = value; }
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

        #endregion


        #region constructors

        public Rectangle(Rect rect, double radiusX, double radiusY)
        {
            _rect = rect;
            _radiusX = radiusX;
            _radiusY = radiusY;
        }

        public Rectangle(double x, double y, double width, double height)
            : this(new Rect(x, y, width, height), 0, 0)
        {
        }

        public Rectangle()
            : this(0, 0, 0, 0)
        { }

        #endregion


        #region IGeometry implementation

        public IGeometry Clone() => new Rectangle(_rect, _radiusX, _radiusY);

        public List<Point> GetGeometryPoints() => new List<Point>() { _rect.TopLeft, _rect.BottomRight };

        public bool SetGeometryByPoints(IList<Point> points)
        {
            if (points?.Count >= 2)
            {
                _rect = new Rect(points[0], points[1]);
                return true;
            }
            else
                return false;
        }

        #endregion

    }
}
