using DingWK.Graphic2D.Controls.Adorners;
using DingWK.Graphic2D.Controls.Rulers;
using DingWK.Graphic2D.Converters;
using DingWK.Graphic2D.Graphic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DingWK.Graphic2D.Controls
{

    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Name = "PART_Canvas", Type = typeof(Canvas))]
    [TemplatePart(Name = "PART_PageGrid", Type = typeof(PageGrid))]
    [TemplatePart(Name = "PART_HorRuler", Type = typeof(HorRuler))]
    [TemplatePart(Name = "PART_VerRuler", Type = typeof(VerRuler))]
    [TemplatePart(Name = "PART_HorScrollBar", Type = typeof(ScrollBar))]
    [TemplatePart(Name = "PART_VerScrollBar", Type = typeof(ScrollBar))]
    [TemplatePart(Name = "PART_AdornerDecorator", Type = typeof(AdornerDecorator))]
    [TemplatePart(Name = "PART_GraphicVisualHost", Type = typeof(GraphicVisualHost))]
    public class GraphicVisualCanvas : Control
    {

        struct CanvasState
        {
            public const int StandBy = 0;
            public const int Selecting = 1;
        }


        #region fields & properties

        internal const double MaxPageScale = 100;
        internal const double MinPageScale = 0.01;
        internal const double MaxPageSize = 10000;
        internal const double MinPageSize = 100;
        internal const double Delta = 2;


        public int State { get; protected set; } = CanvasState.StandBy;

        private RectSelectorAdorner _rectSelector = null;
        public RectSelectorAdorner RectSelector => _rectSelector;

        private Canvas _canvas = null;
        private PageGrid _pageGrid = null;
        private ScrollBar _horScrollBar = null;
        private ScrollBar _verScrollBar = null;
        private AdornerDecorator _adornerDecorator = null;
        private GraphicVisualHost _graphicVisualHost = null;

        public PageGrid PageGrid => _pageGrid;
        public AdornerDecorator AdornerDecorator => _adornerDecorator;
        public GraphicVisualHost GraphicVisualHost => _graphicVisualHost;

        #endregion


        #region Dependency properties


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
                new PropertyMetadata(0.0)
                {
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        double width = (double)baseValue;
                        return width < MinPageSize ? MinPageSize : (width > MaxPageSize ? MaxPageSize : width);
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as GraphicVisualCanvas).SetCanvasFitPageWidth();
                    }
                });
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
                new PropertyMetadata(0.0)
                {
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        double height = (double)baseValue;
                        return height < MinPageSize ? MinPageSize : (height > MaxPageSize ? MaxPageSize : height);
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as GraphicVisualCanvas).SetCanvasFitPageHeight();
                    }
                });
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
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        double scale = (double)baseValue;
                        return scale < MinPageScale ? MinPageScale : (scale > MaxPageScale ? MaxPageScale : scale);
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        GraphicVisualCanvas gvc = d as GraphicVisualCanvas;
                        double factor = (double)e.NewValue / (double)e.OldValue;
                        gvc.PageOffsetX = factor * gvc.PageOffsetX + (1 - factor) * gvc._canvas.RenderSize.Width / 2;
                        gvc.PageOffsetY = factor * gvc.PageOffsetY + (1 - factor) * gvc._canvas.RenderSize.Height / 2;
                        gvc.UpdateScrollBars();
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
                    PropertyChangedCallback = (d, e) => (d as GraphicVisualCanvas).SetContentTransform()
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


        #region GridVisible
        /// <summary>
        /// 
        /// </summary>
        public bool GridVisible
        {
            get { return (bool)GetValue(GridVisibleProperty); }
            set { SetValue(GridVisibleProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GridVisibleProperty =
            DependencyProperty.Register(
                nameof(GridVisible),
                typeof(bool),
                typeof(GraphicVisualCanvas),
                new FrameworkPropertyMetadata(true));
        #endregion
        

        #region Operator
        /// <summary>
        /// 
        /// </summary>
        public FrameworkElement Operator
        {
            get { return (FrameworkElement)GetValue(OperatorProperty); }
            set { SetValue(OperatorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty OperatorProperty =
            DependencyProperty.Register(
                nameof(Operator),
                typeof(FrameworkElement),
                typeof(GraphicVisualCanvas),
                new FrameworkPropertyMetadata(null)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        GraphicVisualCanvas gvc = d as GraphicVisualCanvas;
                        if (e.OldValue != null)
                            gvc._canvas.Children.Remove((FrameworkElement)e.OldValue);
                        if (e.NewValue != null)
                            gvc._canvas.Children.Add((FrameworkElement)e.NewValue);
                    }
                });
        #endregion


        #endregion



        static GraphicVisualCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicVisualCanvas), new FrameworkPropertyMetadata(typeof(GraphicVisualCanvas)));
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = GetTemplateChild("PART_Canvas") as Canvas;
            _pageGrid = GetTemplateChild("PART_PageGrid") as PageGrid;
            _horScrollBar = GetTemplateChild("PART_HorScrollBar") as ScrollBar;
            _verScrollBar = GetTemplateChild("PART_VerScrollBar") as ScrollBar;
            _adornerDecorator = GetTemplateChild("PART_AdornerDecorator") as AdornerDecorator;
            _graphicVisualHost = GetTemplateChild("PART_GraphicVisualHost") as GraphicVisualHost;
            

            _rectSelector = new RectSelectorAdorner(_canvas);
            _rectSelector.SetBinding(
                RectSelectorAdorner.ColorProperty,
                new Binding("GridColor")
                {
                    Source = this,
                    Mode = BindingMode.OneWay,
                    Converter = new SolidColorBrushToColorConverter(),
                });
            _adornerDecorator.AdornerLayer.Add(_rectSelector);

            SetInternalCanvasMouseOperation();
        }


        /// <summary>
        /// 
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            PageOffsetX += (sizeInfo.NewSize.Width - sizeInfo.PreviousSize.Width) / 2;
            PageOffsetY += (sizeInfo.NewSize.Height - sizeInfo.PreviousSize.Height) / 2;
            UpdateScrollBars();
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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



        #region Viewport operations

        /// <summary>
        /// 
        /// </summary>
        public void SetCanvasFitPageWidth()
        {
            if (_canvas != null)
            {
                PageScale = (_canvas.RenderSize.Width - Delta) / PageWidth;
                PageOffsetX = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetCanvasFitPageHeight()
        {
            if (_canvas != null)
            {
                PageScale = (_canvas.RenderSize.Height - Delta) / PageHeight;
                PageOffsetY = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetCanvasFitFullPage()
        {
            if (_canvas != null)
            {
                if (_canvas.RenderSize.Width * PageHeight < _canvas.RenderSize.Height * PageWidth)
                {
                    PageScale = (_canvas.RenderSize.Width - Delta) / PageWidth;
                    PageOffsetX = 0;
                    PageOffsetY = (_canvas.RenderSize.Height - PageScale * PageHeight) / 2;
                }
                else
                {
                    PageScale = (_canvas.RenderSize.Height - Delta) / PageHeight;
                    PageOffsetX = (_canvas.RenderSize.Width - PageScale * PageWidth) / 2;
                    PageOffsetY = 0;
                }
            }
        }

        #endregion


        #region Internal canvas mouse operations

        private void SetInternalCanvasMouseOperation()
        {
            _canvas.MouseUp += _canvas_MouseUp;
            _canvas.MouseDown += _canvas_MouseDown;
            _canvas.LostMouseCapture += InternalCanvas_LostMouseCapture;
            _canvas.MouseLeftButtonDown += InternalCanvas_MouseLeftButtonDown;
            _canvas.MouseMove += InternalCanvas_MouseMove;

        }

        protected virtual void _canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _canvas.CaptureMouse();
        }

        protected virtual void _canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_canvas.IsMouseCaptured)
                _canvas.ReleaseMouseCapture();
        }

        protected virtual void InternalCanvas_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (State == CanvasState.Selecting)
            {
                _rectSelector.Visibility = Visibility.Collapsed;
                State = CanvasState.StandBy;
            }
        }

        protected virtual void InternalCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (State == CanvasState.StandBy)
            {
                State = CanvasState.Selecting;
                _rectSelector.Start = e.GetPosition(_canvas);
                _rectSelector.End = _rectSelector.Start;
                _rectSelector.Visibility = Visibility.Visible;
            }
        }

        protected virtual void InternalCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (State == CanvasState.Selecting && e.LeftButton == MouseButtonState.Pressed)
            {
                _rectSelector.End = e.GetPosition(_canvas);
            }

        }

        #endregion


    }

}
