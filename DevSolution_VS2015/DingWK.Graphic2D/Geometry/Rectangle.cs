using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Geometry
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
            get => _rect;
            set => _rect = value;
        }

        public Size Size
        {
            get => _rect.Size;
            set => _rect.Size = value;
        }

        public double X
        {
            get => _rect.X;
            set => _rect.X = value;
        }

        public double Y
        {
            get => _rect.Y;
            set => _rect.Y = value;
        }

        public double Width
        {
            get => _rect.Width;
            set => _rect.Width = value;
        }

        public double Height
        {
            get => _rect.Height;
            set => _rect.Height = value;
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

        public IGeometry Clone() => new Rectangle(Rect, RadiusX, RadiusY);

        public List<Point> GetGeometryTransformPoints() => new List<Point>() { Rect.TopLeft, Rect.BottomRight };

        public bool SetGeometryByTransformPoints(IList<Point> points)
        {
            if (points?.Count >= 2)
            {
                Rect = new Rect(points[0], points[1]);
                return true;
            }
            else
                return false;
        }

        public void Drawing(DrawingContext dc, Brush brush, Pen pen) => dc.DrawRoundedRectangle(brush, pen, Rect, RadiusX, RadiusY);

        #endregion

    }
}
