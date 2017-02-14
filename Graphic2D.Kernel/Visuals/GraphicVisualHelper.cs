using Graphic2D.Kernel.Common;
using Graphic2D.Kernel.Geom;
using System.Windows;
using System.Windows.Media;
using System;

namespace Graphic2D.Kernel.Visuals
{
    public static class GraphicVisualHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static DrawingGroup GetGraphicVisualDrawing(GraphicVisual graphicVisual)
        {
            var gp = graphicVisual as GroupVisual;
            if (gp != null)
            {
                // GroupVisual 自身没有绘制（没有覆写OnRender方法）, 所以 GroupVisual 的 Drawing 为空（null）；
                // 通过递归、遍历获取 GroupVisual 子级（Children）GraphicVisual 的 Drawing, 
                // 集合形成表示 GroupVisual 图形的 DrawingGroup

                DrawingGroup drawingGroup = new DrawingGroup();
                foreach (GraphicVisual gv in gp)
                {
                    drawingGroup.Children.Add(GetGraphicVisualDrawing(gv));
                }
                return drawingGroup;
            }
            else
            {
                return graphicVisual.Drawing;
            }
        }




        public static void MoveGraphicVisual(GraphicVisual graphicVisual, Vector delta)
        {
            graphicVisual.Origin += delta;
        }

        public static void RotateGraphicVisual(GraphicVisual graphicVisual, double angle, Point center)
        {
            RotateTransform tr = new RotateTransform(angle, center.X, center.Y);
            graphicVisual.Origin = tr.Transform(graphicVisual.Origin);
            graphicVisual.Angle += angle;
        }

        //public static void ScaleGraphicVisual(GraphicVisual graphicVisual, Vector scale,  Point refer)
        //{
        //    if (graphicVisual is GroupVisual)
        //    {
        //        throw new NotImplementedException();
        //    }
        //    else if (graphicVisual is GeomVisual)
        //    { 
        //        Transform tr = new ScaleTransform(scale.X, scale.Y, refer.X, refer.Y);
        //        graphicVisual.Origin = tr.Transform(graphicVisual.Origin);
        //        Vector locScales = Func.VectorRotate(graphicVisual.Angle, scale.X, scale.Y);

        //        GeomVisual gv = graphicVisual as GeomVisual;
        //        IGeom geom = gv.Geom;
        //        GeomHelper.ScaleGeom(ref geom, locScales.X, locScales.Y);
        //        gv.Geom = geom;
        //    }
        //}

        public static void ScaleGraphicVisual(GraphicVisual graphicVisual, double scaleX, double scaleY, Point refer)
        {
            //ScaleTransform tr = new ScaleTransform(scaleX, scaleY, refer.X, refer.Y);
            //graphicVisual.Origin = tr.Transform(graphicVisual.Origin);

            if (graphicVisual is GroupVisual)
            {
                throw new NotImplementedException();
            }
            else if (graphicVisual is GeomVisual)
            {
                GeomVisual gv = graphicVisual as GeomVisual;
                IGeom geom = gv.Geom;
                GeomHelper.ScaleGeom(ref geom, scaleX, scaleY, refer);
                gv.Geom = geom;
            }


        }

        //public static void ScaleGraphicVisual(GraphicVisual graphicVisual, double scaleX, double scaleY)
        //{
        //    if (graphicVisual is GroupVisual)
        //    {
        //        throw new NotImplementedException();
        //    }
        //    else if (graphicVisual is GeomVisual)
        //    {
        //        GeomVisual gv = graphicVisual as GeomVisual;
        //        IGeom geom = gv.Geom;
        //        GeomHelper.ScaleGeom(ref geom, scaleX, scaleY);
        //        gv.Geom = geom;
        //    }
        //}


    }
}
