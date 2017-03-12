using DingWK.Graphic2D.Graphics;
using DingWK.Graphic2D.Wpf.Helper;
using System.Windows.Media;

namespace DingWK.Graphic2D.Wpf
{
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
