using DingWK.Graphic2D.Geom;
using System;
using System.Windows.Media;

namespace DingWK.Graphic2D.Wpf
{

    //public static class GeomRenderHelper
    //{


    //    //public static void Render(DrawingContext dc, Rectangle rectangle , Brush fill, Pen stroke)
    //    //{
    //    //    dc.DrawRoundedRectangle(fill, stroke, rectangle.Rect, rectangle.RadiusX, rectangle.RadiusY);
    //    //}

    //    public static void Render(DrawingContext dc, Geometry goemetry, Brush fill, Pen stroke)
    //    {
    //        switch (typeof(Geom.Geometry))
    //        {
    //            case GetTypr(Rectangle):


    //        }


    //        dc.DrawRoundedRectangle(fill, stroke, rectangle.Rect, rectangle.RadiusX, rectangle.RadiusY);
    //    }

    //}


    public static class GeometryDrawingExtension
    {

        public static void Drawing(this Geom.Geometry geometry, DrawingContext dc, Brush fill, Pen stroke)
        {
            throw new NotImplementedException();
        }

        public static void Drawing(this Rectangle rectangle, DrawingContext dc, Brush fill, Pen stroke)
        {
            dc.DrawRoundedRectangle(fill, stroke, rectangle.Rect, rectangle.RadiusX, rectangle.RadiusY);
        }


        //public static void Drawing1(Rectangle rectangle, DrawingContext dc, Brush fill, Pen stroke)
        //{
        //    dc.DrawRoundedRectangle(fill, stroke, rectangle.Rect, rectangle.RadiusX, rectangle.RadiusY);
        //}
    }


}
