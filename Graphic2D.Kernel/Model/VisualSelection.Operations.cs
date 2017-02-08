using Graphic2D.Kernel.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Graphic2D.Kernel.Model
{
    public sealed partial class VisualSelection
    {
        public void Move(Vector delta)
        {
            if (Count > 0)
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
        }

        public void Rotate(double delta)
        {
            Rect rect = SelectionDrawing.Bounds;
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            double angle = Math.Round(delta, 2);
            foreach (GraphicVisual gv in this)
            {
                GraphicVisualHelper.RotateGraphicVisual(gv, angle,center);
            }
            RefAngle += angle;
            UpdateSelection();
        }

        public void Resize(Rect rect)
        {

        }

    }
}
