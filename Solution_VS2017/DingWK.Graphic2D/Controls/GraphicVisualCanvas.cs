using DingWK.Graphic2D.Graphic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DingWK.Graphic2D.Controls
{


    public class GraphicVisualCanvas : Control
    {

        private Canvas _canvas = null;
        private PageGrid _pageGrid = null;
        private ScrollBar _horScrollBar = null;
        private ScrollBar _verScrollBar = null;



        static GraphicVisualCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicVisualCanvas), new FrameworkPropertyMetadata(typeof(GraphicVisualCanvas)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = GetTemplateChild("PART_Canvas") as Canvas;
            _pageGrid = GetTemplateChild("PART_PageGrid") as PageGrid;
            _horScrollBar = GetTemplateChild("PART_HorScrollBar") as ScrollBar;
            _verScrollBar = GetTemplateChild("PART_VerScrollBar") as ScrollBar;

        }



        #region PageWidth
        /// <summary>
        /// 
        /// </summary>
        public double PageWidth
        {
            get { return (double)GetValue(PageWidthProperty); }
            set { SetValue(PageWidthProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageWidthProperty =
            DependencyProperty.Register(
                nameof(PageWidth),
                typeof(double),
                typeof(GraphicVisualCanvas),
                new PropertyMetadata(0.0));
        #endregion


        #region PageHeight
        /// <summary>
        /// 
        /// </summary>
        public double PageHeight
        {
            get { return (double)GetValue(PageHeightProperty); }
            set { SetValue(PageHeightProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageHeightProperty =
            DependencyProperty.Register(
                nameof(PageHeight),
                typeof(double),
                typeof(GraphicVisualCanvas),
                new PropertyMetadata(0.0));
        #endregion


        #region PageScale
        /// <summary>
        /// 
        /// </summary>
        public double PageScale
        {
            get { return (double)GetValue(PageScaleProperty); }
            set { SetValue(PageScaleProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageScaleProperty =
            DependencyProperty.Register(
                nameof(PageScale),
                typeof(double),
                typeof(GraphicVisualCanvas), 
                new PropertyMetadata(1.0)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        GraphicVisualCanvas gvc = d as GraphicVisualCanvas;
                        double delta = (double)e.OldValue - (double)e.NewValue;
                        gvc.AdjustOffset(
                            delta * gvc._canvas.RenderSize.Width,
                            delta * gvc._canvas.RenderSize.Height);
                    }
                });
        #endregion


        #region PageColor
        /// <summary>
        /// 
        /// </summary>
        public Brush PageColor
        {
            get { return (Brush)GetValue(PageColorProperty); }
            set { SetValue(PageColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageColorProperty =
            DependencyProperty.Register(
                nameof(PageColor),
                typeof(Brush),
                typeof(GraphicVisualCanvas),
                new PropertyMetadata(new SolidColorBrush(Colors.White)));
        #endregion


        //#region PageOffset
        ///// <summary>
        ///// 
        ///// </summary>
        //public Point PageOffset
        //{
        //    get { return (Point)GetValue(PageOffsetProperty); }
        //    set { SetValue(PageOffsetProperty, value); }
        //}
        ////
        //// Dependency property definition
        ////
        //public static readonly DependencyProperty PageOffsetProperty =
        //    DependencyProperty.Register(
        //        nameof(PageOffset),
        //        typeof(Point),
        //        typeof(GraphicVisualCanvas),
        //        new PropertyMetadata(new Point())
        //        {
        //            PropertyChangedCallback = (d, e) =>
        //            {
        //                GraphicVisualCanvas gvc = d as GraphicVisualCanvas;

        //                // set host transform
        //                TransformGroup trx = new TransformGroup();
        //                trx.Children.Add(new ScaleTransform(gvc.PageScale, gvc.PageScale));
        //                trx.Children.Add(new TranslateTransform(gvc.PageOffset.X, gvc.PageOffset.Y));
        //                gvc.GraphicVisualHost.RenderTransform = trx;

        //            }
        //        });
        //#endregion


        #region PageOffsetX
        /// <summary>
        /// 
        /// </summary>
        public double PageOffsetX
        {
            get { return (double)GetValue(PageOffsetXProperty); }
            set { SetValue(PageOffsetXProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageOffsetXProperty =
            DependencyProperty.Register(
                nameof(PageOffsetX),
                typeof(double),
                typeof(GraphicVisualCanvas),
                new FrameworkPropertyMetadata(0.0)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as GraphicVisualCanvas).SetContentTransform();
                    }

                });
        #endregion


        #region PageOffsetY
        /// <summary>
        /// 
        /// </summary>
        public double PageOffsetY
        {
            get { return (double)GetValue(PageOffsetYProperty); }
            set { SetValue(PageOffsetYProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageOffsetYProperty =
            DependencyProperty.Register(
                nameof(PageOffsetY),
                typeof(double),
                typeof(GraphicVisualCanvas),
                new FrameworkPropertyMetadata(0.0)
                {
                    PropertyChangedCallback = (d, e) => (d as GraphicVisualCanvas).SetContentTransform()
                });
        #endregion


        #region GridSize
        /// <summary>
        /// 
        /// </summary>
        public int GridSize
        {
            get { return (int)GetValue(GridSizeProperty); }
            set { SetValue(GridSizeProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register(
                nameof(GridSize),
                typeof(int),
                typeof(GraphicVisualCanvas),
                new PropertyMetadata(10)
                {
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        int intValue = (int)baseValue;
                        return (intValue < PageGrid.GridSizeInterval) ? PageGrid.GridSizeInterval : intValue - intValue % PageGrid.GridSizeInterval;
                    }
                });
        #endregion


        #region GridColor
        /// <summary>
        /// 
        /// </summary>
        public Brush GridColor
        {
            get { return (Brush)GetValue(GridColorProperty); }
            set { SetValue(GridColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(
                nameof(GridColor),
                typeof(Brush),
                typeof(GraphicVisualCanvas),
                new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
        #endregion


        #region GraphicVisualHost

        private GraphicVisualHost _graphicVisualHost = null;

        /// <summary>
        /// 
        /// </summary>
        public GraphicVisualHost GraphicVisualHost
        {
            get
            {
                if (_graphicVisualHost == null)
                {
                    _graphicVisualHost = base.GetTemplateChild("PART_GraphicVisualHost") as GraphicVisualHost;
                }
                return _graphicVisualHost;
            }
        }
        #endregion







        private void AdjustOffset(double widthChange, double heightChange)
        {
            PageOffsetX += widthChange / 2;
            PageOffsetY += heightChange / 2;
        }

        private void UpdateScrollBars()
        {
            if (_canvas == null) return;

            if (_horScrollBar != null)
            {
                _horScrollBar.Minimum = -_canvas.RenderSize.Width / 2;
                _horScrollBar.Maximum = -_canvas.RenderSize.Width / 2 + PageWidth * PageScale;
                _horScrollBar.SmallChange = _canvas.RenderSize.Width / 50;
                _horScrollBar.LargeChange = _canvas.RenderSize.Width;
                _horScrollBar.ViewportSize = _canvas.RenderSize.Width;
            }

            if (_verScrollBar != null)
            {
                _verScrollBar.Minimum = -_canvas.RenderSize.Height / 2;
                _verScrollBar.Maximum = -_canvas.RenderSize.Height / 2 + PageHeight * PageScale;
                _verScrollBar.SmallChange = _canvas.RenderSize.Height / 50;
                _verScrollBar.LargeChange = _canvas.RenderSize.Height;
                _verScrollBar.ViewportSize = _canvas.RenderSize.Height;
            }
        }


        private void SetContentTransform()
        {
            // set grid translate
            _pageGrid.RenderTransform = new TranslateTransform(PageOffsetX, PageOffsetY);

            // set host scale & translate 
            TransformGroup trx = new TransformGroup();
            trx.Children.Add(new ScaleTransform(PageScale, PageScale));
            trx.Children.Add(new TranslateTransform(PageOffsetX, PageOffsetY));
            GraphicVisualHost.RenderTransform = trx;
        }

    }
}
