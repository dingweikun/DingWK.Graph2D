using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Visuals
{
    public static class VisualHelper
    {

        #region GetDrawing


        public static DrawingGroup GetDrawing(GeomVisual geomVisual)
        {
            return geomVisual.Drawing.CloneCurrentValue();
        }

        #endregion



        #region GetNomalizedDrawing

        public static DrawingGroup GetNomalizedDrawing(GraphicVisual graphicVisual)
        {
            GetDrawing(graphicVisual)
        }


        #endregion




        #region GetResizeRect(...)

        public static Rect GetResizeRect(GeomVisual geomVisual)
        {
            if (geomVisual == null) return Rect.Empty;

            DrawingGroup drawing = geomVisual.Drawing.CloneCurrentValue();
            drawing.Transform = new RotateTransform(-geomVisual.Angle);



        }

        public static Rect GetResizeRect(GroupVisual groupVisual)
        {




        }

        #endregion



    }
}
3