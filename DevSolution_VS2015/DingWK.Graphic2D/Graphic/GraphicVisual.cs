using System;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphic
{
    public abstract class GraphicVisual : DrawingVisual, IGraphicVisual
    {

        public Guid Guid { get; private set; } = Guid.NewGuid();

        

        #region PlaceInfo property

        public IPlaceInfo PlaceInfo
        {
            get { return (IPlaceInfo)GetValue(PlaceInfoProperty); }
            set { SetValue(PlaceInfoProperty, value); }
        }

        public static readonly DependencyProperty PlaceInfoProperty =
            DependencyProperty.Register(
                nameof(PlaceInfo),
                typeof(IPlaceInfo),
                typeof(GraphicVisual),
                new FrameworkPropertyMetadata(default(PlaceInfo), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) => (d as GraphicVisual).UpdateVisualTransform()
                });

        #endregion



        #region Stroke property

        public Pen Stroke
        {
            get { return (Pen)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(
                nameof(Stroke),
                typeof(Pen),
                typeof(GraphicVisual),
                new FrameworkPropertyMetadata(new Pen(Brushes.Black, 1), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) => (d as GraphicVisual).UpdateVisualStroke(),
                    SubPropertiesDoNotAffectRender = false
                });

        #endregion



        #region Fill property

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(
                nameof(Fill),
                typeof(Brush),
                typeof(GraphicVisual),
                new FrameworkPropertyMetadata(new SolidColorBrush(Colors.LightBlue), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) => (d as GraphicVisual).UpdateVisualFill(),
                    SubPropertiesDoNotAffectRender = false
                });

        #endregion



        public void UpdateVisualTransform()
        {
            TransformGroup trans = new TransformGroup();
            trans.Children.Add(new RotateTransform(PlaceInfo.Angle));
            trans.Children.Add(new TranslateTransform(PlaceInfo.Origin.X, PlaceInfo.Origin.Y));
            this.Transform = trans;
        }

        protected abstract void UpdateVisualFill();
        protected abstract void UpdateVisualStroke();

    }
}
