using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DingWK.Graphic2D.Controls.Adorners
{
    public class RectSelectorAdorner : Adorner
    {
        const double FillBrushOpacity = 0.2;

        public Brush Fill { get; protected set; } 
        public Pen Stroke { get; protected set; } 
        public Rect SelectedRect => new Rect(Start, End);


        #region Color
        /// <summary>
        /// 
        /// </summary>
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                nameof(Color),
                typeof(Color),
                typeof(RectSelectorAdorner),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        var adr = d as RectSelectorAdorner;
                        adr.Fill = new SolidColorBrush((Color)e.NewValue);
                        adr.Stroke = new Pen(adr.Fill.CloneCurrentValue(), 1) { DashStyle = DashStyles.Dash };
                        adr.Fill.Opacity = FillBrushOpacity;

                        if (adr.Stroke.CanFreeze) adr.Stroke.Freeze();
                        if (adr.Fill.CanFreeze) adr.Fill.Freeze();
                    }
                });
        #endregion


        #region Start
        /// <summary>
        /// 
        /// </summary>
        public Point Start
        {
            get { return (Point)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register(
                nameof(Start),
                typeof(Point),
                typeof(RectSelectorAdorner),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        #region End
        /// <summary>
        /// 
        /// </summary>
        public Point End
        {
            get { return (Point)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register(
                nameof(End),
                typeof(Point),
                typeof(RectSelectorAdorner),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        public RectSelectorAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
            Color = Colors.Gray;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Matrix mtx = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
            double dpiFactor = 1 / mtx.M11;
            double delt = dpiFactor / 2;

            GuidelineSet guidelineSet = new GuidelineSet();
            guidelineSet.GuidelinesX.Add(SelectedRect.Left - delt);
            guidelineSet.GuidelinesX.Add(SelectedRect.Right - delt);
            guidelineSet.GuidelinesY.Add(SelectedRect.Top - delt);
            guidelineSet.GuidelinesY.Add(SelectedRect.Bottom - delt);

            drawingContext.PushGuidelineSet(guidelineSet);
            drawingContext.DrawRectangle(Fill, Stroke, SelectedRect);
            drawingContext.Pop();
        }

    }
}
