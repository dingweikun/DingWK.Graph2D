using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Geometry
{
    public class Line : IGeometry
    {
        #region fields & prperties

        Point _start;
        Point _end;

        public Point Start
        {
            get => _start;
            set => _start = value;
        }

        public Point End
        {
            get => _end;
            set => _end = value;
        }

        public Vector Direct => End - Start;

        public double Length => Direct.Length;

        #endregion


        #region constructors

        public Line(Point start, Point end)
        {
            _start = start;
            _end = end;
        }

        public Line(Point start, Vector direct)
            : this(start, start + direct)
        { }

        public Line()
                : this(new Point(0, 0), new Point(1, 0))
        { }

        #endregion


        #region IGeometry implementation

        public IGeometry Clone() => new Line(Start, End);

        public List<Point> GetGeometryTransformPoints() => new List<Point>() { Start, End };

        public bool SetGeometryByTransformPoints(IList<Point> points)
        {
            if (points?.Count == 2)
            {
                Start = points[0];
                End = points[1];
                return true;
            }
            else
                return false;
        }

        public void Drawing(DrawingContext dc, Brush brush, Pen pen) => dc.DrawLine(pen, Start, End);

        #endregion


    }
}
