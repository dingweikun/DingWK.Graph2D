using DingWK.Graphic2D.Geometry;
using System;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphic
{
    public class GeomGraphicVisual : GraphicVisual
    {

        #region Geometry property

        public IGeometry Geometry
        {
            get { return (IGeometry)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }

        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register(
                nameof(Geometry),
                typeof(IGeometry),
                typeof(GeomGraphicVisual),
                new FrameworkPropertyMetadata(new Rectangle(0, 0, 1, 1), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = (d, baseValue) => baseValue ?? (d as GeomGraphicVisual).Geometry,
                    PropertyChangedCallback = (d, e) => (d as GeomGraphicVisual).RenderVisual()
                });

        #endregion


        public GeomGraphicVisual(IGeometry geometry, PlaceInfo info = default(PlaceInfo))
        {
            Geometry = geometry ?? throw new ArgumentNullException(nameof(geometry));
            PlaceInfo = info;
        }


        public void RenderVisual()
        {
            using (DrawingContext dc = RenderOpen())
            {
                Geometry.Drawing(dc, Fill, Stroke);
                dc.Close();
            }
        }

        protected override void UpdateVisualFill() => RenderVisual();
        protected override void UpdateVisualStroke() => RenderVisual();

    }


    public sealed class GeomGraphicVisual<T> : GeomGraphicVisual where T : IGeometry
    {
        public GeomGraphicVisual(T geometry, PlaceInfo info = default(PlaceInfo))
            : base(geometry, info)
        {
        }
    }

}
