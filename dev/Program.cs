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
            GeomVisual<Rectangle> g1 = new GeomVisual<Rectangle>(
                new Rectangle(new Rect(100, 100, 200, 100)));
            g1.Origin = new Point(100, 100);
            g1.Angle = 45;

            DrawingGroup d = g1.Drawing;

            d.Transform = g1.Transform;

            //DrawingGroup c = new DrawingGroup();
            //c.Children.Add(d);
            //c.Transform = new RotateTransform(-g1.Angle);



            //GeomVisual<Rectangle> g1 = new GeomVisual<Rectangle>(
            //    new Rectangle(new Rect(100, 100, 100, 100)));
            //DrawingGroup d = g1.Drawing;

            //d.Transform = new RotateTransform(45);



            Console.WriteLine();



        }
    }
}
