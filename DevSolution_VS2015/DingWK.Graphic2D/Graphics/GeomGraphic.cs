using DingWK.Graphic2D.Geom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWK.Graphic2D.Graphics
{
    public sealed class GeomGraphic : Graphic
    {
        // field

        private Geometry _geometry;


        // properties
        
        public static GeomGraphic Empty => new GeomGraphic();

        public Geometry Geometry
        {
            get { return _geometry; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Geometry));
                }
                else
                {
                    _geometry = value;
                }
            }
        }

        public bool IsEmpty => _geometry == null;


        // constructors

        private GeomGraphic()
        {
        }

        public GeomGraphic(Geometry geometry)
        {
            Geometry = geometry;
        }

    }




    public sealed class GeomGraphic<T> : Graphic
        where T : Geometry
    {
        // field

        private T _geometry;


        // properties

        public static GeomGraphic<T> Empty => new GeomGraphic<T>();

        public T Geometry
        {
            get { return _geometry; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Geometry));
                }
                else
                {
                    _geometry = value;
                }
            }
        }

        public bool IsEmpty => _geometry == null;


        // constructors

        private GeomGraphic()
        {
        }

        public GeomGraphic(T _geometry)
        {
        }


    }
}
