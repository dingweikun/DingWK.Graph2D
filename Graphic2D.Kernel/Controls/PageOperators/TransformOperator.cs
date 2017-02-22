using Graphic2D.Kernel.Model;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System;
using Graphic2D.Kernel.Common;

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
        private Rect BoundsRect { get; set; }
        private DrawingGroup Watermark { get; set; }
        private Vector MouseDelta { get; set; }
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
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as TransformOperator).SetTransformOperator();
                    }
                });
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

            BarLength = _thumbs[9].Margin.Top;
        }

        internal override void OnScalePropertyChanged()
        {
            SetTransformOperator();
        }

        public void SetTransformOperator()
        {
            if (SelectedVisuals != null)
            {
                Watermark = SelectedVisuals.SelectionDrawing.CloneCurrentValue();
                Watermark.Transform = new RotateTransform(-SelectedVisuals.RefAngle);
                Watermark.Opacity = 0.5;

                //if (!SelectedVisuals.RefRect.IsEmpty)
                //{
                Height = SelectedVisuals.RefRect.Height * Scale;
                    Width = SelectedVisuals.RefRect.Width * Scale;
                    TransformGroup tr = new TransformGroup();
                    tr.Children.Add(new TranslateTransform(SelectedVisuals.RefRect.X * Scale, SelectedVisuals.RefRect.Y * Scale));
                    tr.Children.Add(new RotateTransform(SelectedVisuals.RefAngle));
                    RenderTransform = tr;
                //}
            }
        }



        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = e.OriginalSource as Thumb;
            if (thumb == _thumbs[8])
            {
                MoveOffset = new Point(e.HorizontalChange, e.VerticalChange);
            }
            else if (thumb == _thumbs[9])
            {

                Point cp = new Point(Width / 2, Height / 2);
                Point rp = new Point(Width / 2, BarLength);
                double l = Height / 2 - BarLength;
                double s = e.HorizontalChange;
                double delta = Math.Atan2(e.HorizontalChange, Height / 2 - BarLength) / Math.PI * 180;
                SelectedVisuals.Rotate(delta);
                SetTransformOperator();
            }
            else
            {
                FrameworkElement element = thumb as FrameworkElement;

                Point refer = SelectedVisuals.RefRect.Location;
                //Point refer = RegionRect.Location;

                double dx, dy;

                switch (element.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        dx = -e.HorizontalChange;
                        //refer.X += RegionRect.Width;
                        refer.X += SelectedVisuals.RefRect.Width;
                        break;
                    case HorizontalAlignment.Right:
                        dx = e.HorizontalChange;
                        break;
                    default:
                        dx = 0;
                        break;
                }
                switch (element.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        dy = -e.VerticalChange;
                        //refer.Y += RegionRect.Height;
                        refer.Y += SelectedVisuals.RefRect.Height;
                        break;
                    case VerticalAlignment.Bottom:
                        dy = e.VerticalChange;
                        break;
                    default:
                        dy = 0;
                        break;
                }

                //refer = Watermark.Transform.Inverse.Transform(refer);
                //refer = new RotateTransform(SelectedVisuals.RefAngle).Transform(refer);

                double factorX = Width + dx <= 0 ? 0 : dx / Width;
                double factorY = Height + dy <= 0 ? 0 : dy / Height;

                //factorX = 1;

                SelectedVisuals.Resize(factorX, factorY, refer);
                SetTransformOperator();
            }
            e.Handled = true;
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var thumb = e.OriginalSource as Thumb;
            if (thumb == _thumbs[8])
            {
                MoveOffset = new Point(0, 0);
                BoundsRect = SelectedVisuals.SelectionDrawing.Bounds;
                AccentPen.Thickness = 1 / Scale;
            }
            e.Handled = true;
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            var thumb = e.OriginalSource as Thumb;
            if (thumb == _thumbs[8])
            {
                Vector delta = Func.VectorRotate(-SelectedVisuals.RefAngle, MoveOffset.X / Scale, MoveOffset.Y / Scale);
                SelectedVisuals.Move(delta);
                SetTransformOperator();
            }

            MoveOffset = new Point(0, 0);
            BoundsRect = Rect.Empty;
            AccentPen.Thickness = 0;

            e.Handled = true;
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (!BoundsRect.IsEmpty)
            {
                // 绘制图形边界矩形

                TransformGroup tx = new TransformGroup();
                tx.Children.Add(new ScaleTransform(Scale, Scale));
                tx.Children.Add(RenderTransform.Inverse as Transform);
                tx.Children.Add(new TranslateTransform(MoveOffset.X, MoveOffset.Y));

                drawingContext.PushTransform(tx);
                drawingContext.DrawRectangle(null, AccentPen, BoundsRect);
                drawingContext.Pop();

                // 绘制图形水印

                TransformGroup tr = new TransformGroup();
                tr.Children.Add(new TranslateTransform(-SelectedVisuals.RefRect.X, -SelectedVisuals.RefRect.Y));
                tr.Children.Add(new ScaleTransform(Scale, Scale));
                tr.Children.Add(new TranslateTransform(MoveOffset.X, MoveOffset.Y));

                drawingContext.PushTransform(tr);
                drawingContext.DrawDrawing(Watermark);
                drawingContext.Pop();
            }

        }

    }
}
