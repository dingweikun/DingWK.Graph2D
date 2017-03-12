using DingWK.Graphic2D.Geom;
using DingWK.Graphic2D.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DingWK.Graphic2D.Wpf.Helper
{
    public static class GeometryDrawingHelper
    {
        public static void DrawGeometry(DrawingContext dc, Geom.Geometry geometry, Brush fill, Pen stroke)
        {
            Type geomType = geometry?.GetType();
            if (geomType == typeof(Geom.Rectangle))
            {
                DrawingRectangle(dc, geometry as Geom.Rectangle, fill, stroke);
            }
            else if (geomType == typeof(Geom.Ellipse))
            {
                DrawingEllipse(dc, geometry as Geom.Ellipse, fill, stroke);
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        private static void DrawingEllipse(DrawingContext dc, Ellipse ellipse, Brush fill, Pen stroke)
        {
            dc.DrawEllipse(fill, stroke, ellipse.Center, ellipse.RadiusX, ellipse.RadiusY);
        }

        public static void DrawingRectangle(DrawingContext dc, Rectangle rectangle, Brush fill, Pen stroke)
        {
            dc.DrawRoundedRectangle(fill, stroke, rectangle.Rect, rectangle.RadiusX, rectangle.RadiusY);
        }


    }
}
