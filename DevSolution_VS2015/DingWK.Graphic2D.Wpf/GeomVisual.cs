using DingWK.Graphic2D.Graphics;
using System.Windows.Media;

namespace DingWK.Graphic2D.Wpf
{
    //public sealed class GeomVisual : GraphicVisual
    //{



    //    public GeomVisual(GeomGraphic geomGraphicg)
    //    {

    //    }


    //    protected override void UpdateFill()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override void UpdateStroke()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateVisualRender()
    //    {
    //        using (DrawingContext dc = Visual.RenderOpen())
    //        {
    //            GeomRenderHelper.Render(dc, (Graphic as GeomGraphic).Geometry, Fill, Stroke);
    //        }
    //    }
    //}

    //public interface IGeomVisual<in T>
    //    where T : Geom.Geometry
    //{
    //    GeomVisual<T> GeomVisual { get; }

    //}

    public sealed class GeomVisual<T> : GraphicVisual
        where T : Geom.Geometry
    {

        public GeomGraphic<T> GeomGraphic => Graphic as GeomGraphic<T>;


        public GeomVisual(GeomGraphic<T> geomGraphicg)
            : base(geomGraphicg)
        {
        }

        protected override void UpdateGraphic()
        {
            UpdateVisualRender();
            UpdateVisualTransform();
        }
        protected override void UpdateFill() => UpdateVisualRender();
        protected override void UpdateStroke() => UpdateVisualRender();

        public void UpdateVisualRender()
        {
            using (DrawingContext dc = Visual.RenderOpen())
            {
                GeometryDrawingHelper.DrawGeometry(dc, GeomGraphic.Geometry, Fill, Stroke);
                dc.Close();
            }
        }
    }
}
