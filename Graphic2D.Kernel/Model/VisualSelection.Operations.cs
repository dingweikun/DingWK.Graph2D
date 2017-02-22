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

            // 移动 RefRect
            delta = (Vector)(new RotateTransform(-RefAngle).Transform((Point)delta));
            RefRect = new Rect(RefRect.Location + delta, RefRect.Size);

            UpdateSelection();
        }

        public void Rotate(double delta)
        {
            Point center = new Point(RefRect.X + RefRect.Width / 2, RefRect.Y + RefRect.Height / 2);
            center = new RotateTransform(RefAngle).Transform(center);

            double angle = Math.Round(delta, 2);
            foreach (GraphicVisual gv in this)
            {
                GraphicVisualHelper.RotateGraphicVisual(gv, angle, center);
            }

            Point loc = new RotateTransform(RefAngle).Transform(RefRect.Location);
            loc = new RotateTransform(angle, center.X, center.Y).Transform(loc);
            RefAngle += angle;
            loc = new RotateTransform(-RefAngle).Transform(loc);
            RefRect = new Rect(loc, RefRect.Size);

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


                //Point origin = new RotateTransform(RefAngle).Transform(gv.Origin);
                //origin = new ScaleTransform(1 + factorX, 1 + factorY, refer.X, refer.Y).Transform(origin);
                //gv.Origin = new RotateTransform(-RefAngle).Transform(origin);


                //Point localRefer = gv.Transform.Inverse.Transform(refer);
                Point localRefer = gv.Transform.Inverse.Transform(refer);
                Point outRefer = new RotateTransform(RefAngle).Transform(refer);

                GraphicVisualHelper.ScaleGraphicVisual(gv, 1 + fx, 1 + fy, outRefer);
                //GraphicVisualHelper.ScaleGraphicVisual(gv, 1 + fx, 1 + fy);
            }

            Point loc = new ScaleTransform(1 + factorX, 1 + factorY, refer.X, refer.Y).Transform(RefRect.Location);
            RefRect = new Rect(loc.X, loc.Y, RefRect.Width * (1 + factorX), RefRect.Height * (1 + factorY));

            UpdateSelection();
        }

    }
}
