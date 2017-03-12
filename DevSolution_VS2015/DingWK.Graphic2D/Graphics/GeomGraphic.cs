using DingWK.Graphic2D.Geom;
using System;

namespace DingWK.Graphic2D.Graphics
{

    public interface IGeomGraphic : IGraphic
    {
        IGeometry Geometry { get; set; }
    }


    public sealed class GeomGraphic : Graphic, IGeomGraphic
    {
        // field

        private IGeometry _geometry;


        // properties

        public IGeometry Geometry
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


        // constructors

        public GeomGraphic(IGeometry geometry)
        {
            Geometry = geometry;
        }
    }


}
