using Graphic2D.Kernel.Model;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Name = "PART_MoveThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RotateThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_LTThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_CTThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RTThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_LCThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RCThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_LBThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_CBThumb", Type = typeof(Thumb))]
    [TemplatePart(Name = "PART_RBThumb", Type = typeof(Thumb))]
    public class TransformOperator : PageOperator
    {
        private Thumb[] _thumbs;

        private double BarLength { get; set; }
        private double AngleEx { get; set; }
        private DrawingGroup Watermark { get; set; }
        private Pen AccentPen { get; set; }
        public Color AccentColor
        {
            get
            {
                return (AccentPen.Brush as SolidColorBrush).Color;
            }
            set
            {
                AccentPen = new Pen(new SolidColorBrush(value), 0);
                AccentPen.DashStyle = DashStyles.Dash;
            }
        }


        #region MoveOffset
        /// <summary>
        /// 
        /// </summary>
        public Point MoveOffset
        {
            get { return (Point)GetValue(MoveOffsetProperty); }
            set { SetValue(MoveOffsetProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty MoveOffsetProperty =
            DependencyProperty.Register(
                nameof(MoveOffset),
                typeof(Point),
                typeof(TransformOperator),
                new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion


        #region SelectedVisuals
        /// <summary>
        /// 
        /// </summary>
        public VisualSelection SelectedVisuals
        {
            get { return (VisualSelection)GetValue(SelectedVisualsProperty); }
            set { SetValue(SelectedVisualsProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty SelectedVisualsProperty =
            DependencyProperty.Register(
                nameof(SelectedVisuals),
                typeof(VisualSelection),
                typeof(TransformCollection),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        
        static TransformOperator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransformOperator), new FrameworkPropertyMetadata(typeof(TransformOperator)));
        }

        public TransformOperator()
        {
            AccentColor = Colors.Red;
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler(Thumb_DragStarted));
            AddHandler(Thumb.DragDeltaEvent, new DragDeltaEventHandler(Thumb_DragDelta));
            AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(Thumb_DragCompleted));


            // Thumb 编号示意            
            //        9  
            //        |
            // 5------6------7
            // |             |
            // 4      8      0
            // |             |
            // 3------2------1

            _thumbs = new Thumb[]
            {
                GetTemplateChild("PART_RCThumb") as Thumb,     // 0
                GetTemplateChild("PART_RBThumb") as Thumb,     // 1
                GetTemplateChild("PART_CBThumb") as Thumb,     // 2
                GetTemplateChild("PART_LBThumb") as Thumb,     // 3
                GetTemplateChild("PART_LCThumb") as Thumb,     // 4
                GetTemplateChild("PART_LTThumb") as Thumb,     // 5
                GetTemplateChild("PART_CTThumb") as Thumb,     // 6
                GetTemplateChild("PART_RTThumb") as Thumb,     // 7
                GetTemplateChild("PART_MoveThumb") as Thumb,   // 8
                GetTemplateChild("PART_RotateThumb") as Thumb  // 9
            };

            BarLength = -_thumbs[9].Margin.Top;
        }

        internal override void OnScalePropertyChanged()
        {
            SetTransformOperator();
        }

        
        public void SetTransformOperator()
        {
            if (SelectedVisuals != null)
            {
                Rect bounds = SelectedVisuals.SelectionDrawing.Bounds;
                Height = bounds.Height * Scale;
                Width = bounds.Width * Scale;
                TransformGroup tr = new TransformGroup();
                tr.Children.Add(new TranslateTransform(bounds.X * Scale, bounds.Y * Scale));
                tr.Children.Add(new RotateTransform(SelectedVisuals.RefAngle));
                RenderTransform = tr;
            }
        }


        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = e.OriginalSource as Thumb;
            if (thumb != null)
            {
                MoveOffset = new Point(e.HorizontalChange, e.VerticalChange);

               
            }
            e.Handled = true;
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var thumb = e.OriginalSource as Thumb;
            if (thumb != null)
            {
                MoveOffset = new Point(0, 0);

                if (thumb == _thumbs[8])
                {
                    AccentPen.Thickness = 1 / Scale;
                    Watermark = SelectedVisuals?.SelectionDrawing;

                }
                else if (thumb == _thumbs[9])
                {
                    Watermark = SelectedVisuals?.SelectionDrawing;
                }


            }
            e.Handled = true;
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            //MessageBox.Show($"x={MoveOffset.X},y={MoveOffset.Y}");
            MoveOffset = new Point(0, 0);
            AccentPen.Thickness = 0;
            Watermark = null;
            e.Handled = true;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Rect regionRect = SelectedVisuals.RegionRect;


            Transform vs = this.RenderTransform.Inverse as Transform;
            TransformGroup tx = new TransformGroup();
            tx.Children.Add(vs);

            drawingContext.PushTransform(tx);
            drawingContext.PushTransform(new ScaleTransform(Scale, Scale));
            drawingContext.PushTransform(new TranslateTransform(MoveOffset.X, MoveOffset.Y));
            drawingContext.DrawRectangle(Brushes.Yellow, AccentPen, regionRect);
            drawingContext.Pop();
            drawingContext.Pop();
            drawingContext.Pop();








            Rect bounds = SelectedVisuals.SelectionDrawing.Bounds;
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new TranslateTransform(-bounds.X, -bounds.Y));
            tr.Children.Add(new ScaleTransform(Scale, Scale));
            tr.Children.Add(new TranslateTransform(MoveOffset.X, MoveOffset.Y));


            drawingContext.PushTransform(tr);
            drawingContext.DrawRectangle(null, AccentPen, regionRect);

            drawingContext.PushOpacity(0.5);
            drawingContext.DrawDrawing(Watermark);
            drawingContext.Pop();

            drawingContext.Pop();

        }

    }
}
