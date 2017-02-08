using System.Windows;
using System.Windows.Media;

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

        public static void ScaleGraphicVisual(GraphicVisual graphicVisual, double Herfactor, double Verfactor, Point refer)
        {
            GroupVisual gv = graphicVisual as GroupVisual;
            if (gv != null)
            {

            }
            else
            {
                Transform tr = new ScaleTransform(Herfactor, Verfactor, refer.X, refer.Y);
                graphicVisual.Origin = tr.Transform(graphicVisual.Origin);
                
            }
        }
    }
}
