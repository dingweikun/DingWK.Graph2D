using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace dev
{

    public abstract class geom
    {

    }

    public class Ageom : geom
    {

    }

    public class Bgeom : geom
    {

    }

    public static class Extent
    {
        public static void Do(this Ageom a)
        {
            Console.WriteLine("Ageom");
        }
        public static void Do(this Bgeom b)
        {
            Console.WriteLine("Bgeom");
        }
        public static void Do(this geom geom)
        {
            Console.WriteLine("AbstractGeom");
        }


        public static void Xo(Ageom a)
        {
            Console.WriteLine("Ageom");
        }
        public static void Xo(Bgeom b)
        {
            Console.WriteLine("Bgeom");
        }
        public static void Xo(geom geom)
        {
            Console.WriteLine("AbstractGeom");
        }

    }




    public abstract class Graph
    {

    }

    public class GeomGraph<T> : Graph
        where T : geom
    {
        public T geom;

        public GeomGraph(T g)
        {
            geom = g;
        }

    }





    public abstract class VVVV
    {
        public Graph graph;
    }

    public class GV<T> : VVVV
        where T : geom
    {
        public GV(GeomGraph<T> geomGraph)
        {
            graph = geomGraph;
            d = Extent.Xo;
        }

        public GeomGraph<T> GeomGraph => graph as GeomGraph<T>;

        public Action<geom> d;

        public void Done()
        {
            //(graph as GeomGraph<T>).geom.Do();

            var a = GeomGraph.geom;
            a.Do();
           // d((graph as GeomGraph<T>).geom);
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Ageom a = new Ageom();

            GeomGraph<Ageom> geom_a = new GeomGraph<Ageom>(a);
            
            GV<Ageom> gv = new GV<Ageom>(geom_a);

            (gv.graph as GeomGraph<Ageom>).geom.Do();

            gv.GeomGraph.geom.Do();

            gv.Done();



            TransformGroup t1 = new TransformGroup();
            t1.Children.Add(new RotateTransform(45));
            t1.Children.Add(new TranslateTransform(100, 100));

            var x = t1.Inverse;

            string s = x.GetType().ToString();
            string v=t1.Value.GetType().ToString();

            TransformGroup t2 = new TransformGroup();
            t2.Children.Add(new TranslateTransform(-100, -100));
            t2.Children.Add(new RotateTransform(-45));

        }
    }
}
