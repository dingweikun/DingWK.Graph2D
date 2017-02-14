using Graphic2D.Kernel.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Model
{
    public sealed partial class VisualSelection
    {
        public void Move(Vector delta)
        {
            // 图形移动距离保留两位小数
            delta.X = Math.Round(delta.X, 2);
            delta.Y = Math.Round(delta.Y, 2);

            // 移动图形
            foreach (GraphicVisual gv in this)
            {
                GraphicVisualHelper.MoveGraphicVisual(gv, delta);
            }
            UpdateSelection();
        }

        public void Rotate(double delta)
        {
            Rect rect = SelectionDrawing.Bounds;
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            double angle = Math.Round(delta, 2);
            foreach (GraphicVisual gv in this)
            {
                GraphicVisualHelper.RotateGraphicVisual(gv, angle, center);
            }
            RefAngle += angle;
            UpdateSelection();
        }

        public void Resize(double factorX, double factorY, Point refer)
        {
            foreach (GraphicVisual gv in this)
            {
                double alfa = (RefAngle - gv.Angle) * Math.PI / 180;
                double sin2 = Math.Pow(Math.Sin(alfa), 2);
                double cos2 = 1 - sin2;

                double fx = cos2 * factorX + sin2 * factorY;
                double fy = sin2 * factorX + cos2 * factorY;

                //ScaleTransform tr = new ScaleTransform(1 + factorX, 1 + factorY, refer.X, refer.Y);
                //gv.Origin = tr.Transform(gv.Origin);

                Point localRefer = gv.Transform.Inverse.Transform(refer);
                GraphicVisualHelper.ScaleGraphicVisual(gv, 1 + fx, 1 + fy, localRefer);
                //GraphicVisualHelper.ScaleGraphicVisual(gv, 1 + fx, 1 + fy);
            }
            UpdateSelection();
        }

    }
}
