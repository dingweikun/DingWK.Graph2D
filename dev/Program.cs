using Graphic2D.Kernel.Geom;
using Graphic2D.Kernel.Visuals;
using System;
using System.Windows;
using System.Windows.Media;

namespace dev
{
    class Program
    {
        static void Main(string[] args)
        {
            //GeomVisual<Rectangle> g1 = new GeomVisual<Rectangle>(
            //    new Rectangle(new Rect(100.5, 100.5, 199, 99)));
            //g1.Origin = new Point(100, 100);
            //g1.Angle = 90;

            //DrawingGroup d = g1.Drawing;
            //d.Transform = g1.Transform;


            //GeomVisual<Rectangle> g2 = new GeomVisual<Rectangle>(
            //    new Rectangle(new Rect(0.5, 0.5, 99, 99)));
            //g2.Origin = new Point(200, 0);

            //DrawingGroup c = g2.Drawing;
            //c.Transform = g2.Transform;



            //DrawingGroup gg = new DrawingGroup();
            //gg.Children.Add(d);
            //gg.Children.Add(c);
            //gg.Transform = new RotateTransform(-90);

            //Console.WriteLine();

            GraphicVisual gv = new GeomVisual<Rectangle>(
                new Rectangle(new Rect(0.5, 0.5, 99, 99)));

            GeomVisual<IGeom> cc = gv as GeomVisual<IGeom>;


        }
    }
}
