using Graphic2D.Kernel.Visuals;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
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


        #region SelectedVisuals
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<GraphicVisual> SelectedVisuals
        {
            get { return (ReadOnlyCollection<GraphicVisual>)GetValue(SelectedVisualsProperty); }
            set { SetValue(SelectedVisualsProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty SelectedVisualsProperty =
            DependencyProperty.Register(
                nameof(SelectedVisuals),
                typeof(ReadOnlyCollection<GraphicVisual>),
                typeof(TransformOperator),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as TransformOperator).SetTransformOperator();
                    }
                });
        #endregion







        internal override void OnScalePropertyChanged()
        {
            SetTransformOperator();
        }


        public void SetTransformOperator()
        {
            //if (SelectedVisuals == null || SelectedVisuals.Count == 0)
            //{
            //    Height = Width = 0;
            //    RenderTransform = null;
            //}
            //else
            //{
            //    if (SelectedVisuals.Count==1)
            //    {
                    
            //    }
            //    else
            //    {

            //    }
            //}



            //if (SelectedVisuals != null)
            //{
            //    Rect rect = Rect.Empty;

            //    foreach (GraphicVisual gv in SelectedVisuals)
            //    {
            //        rect = Rect.Union(rect, gv.DescendantBounds);
            //    }

            //    Width = rect.Width * Scale;
            //    Height = rect.Height * Scale;

            //    TransformGroup tr = new TransformGroup();
            //    tr.Children.Add(new RotateTransform(Angle));
            //    tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
            //    RenderTransform = tr;


            //    //BoundVisual.BoundTransform.Transform(pos);
            //    //pos.X *= Scale;
            //    //pos.Y *= Scale;

            //    //var tr = BoundVisual.BoundTransform.CloneCurrentValue() as TransformGroup;
            //    //tr.Children.Add(new TranslateTransform(
            //    //    pos.X * Scale - pos.X,
            //    //    pos.Y * Scale - pos.Y
            //    //    ));

            //    //this.RenderTransform = tr;
            //}
















        }


        static TransformOperator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransformOperator), new FrameworkPropertyMetadata(typeof(TransformOperator)));
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
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            e.Handled = true;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //var thumb = e.OriginalSource as Thumb;
            //if (thumb != null)
            //{
            //    if (thumb == _thumbs[8])
            //    {
            //        this.RenderX += e.HorizontalChange;
            //        this.RenderY += e.VerticalChange;
            //    }
            //    else if (thumb == _thumbs[9])
            //    {
            //        Vector vc = new Vector(Width / 2, Height / 2);
            //        Vector vn = new Vector(Width / 2, -25);
            //        Vector va = vn - vc;

            //        Point pd = new Point(e.HorizontalChange, e.VerticalChange);
            //        this.RenderTransform.Inverse.Transform(pd);
            //        Vector vb = new Vector(pd.X, pd.Y);


            //        double dd = Math.Acos((vb * va) / va.Length / vb.Length);
            //        if (vb.Y < 0) dd = -dd;

            //        this.RenderAngle += dd;


            //    }
            //    else
            //    {
            //    }
            //}
            e.Handled = true;
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            //throw new NotImplementedException();
            e.Handled = true;
        }

    }
}
