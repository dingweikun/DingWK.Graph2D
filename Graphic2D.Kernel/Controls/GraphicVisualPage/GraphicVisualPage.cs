using Graphic2D.Kernel.Visuals;
using System;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    /// <summary>
    /// 基础界面元素，提供图形对象在WPF中显示的支持。
    /// A basic UI element which provides graphic rendering support in WPF.
    /// </summary>
    public class GraphicVisualPage : FrameworkElement
    {
        #region Properties

        #region PageOffset
        /// <summary>
        /// 
        /// </summary>
        public Point PageOffset
        {
            get { return (Point)GetValue(PageOffsetProperty); }
            set { SetValue(PageOffsetProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty PageOffsetProperty =
            DependencyProperty.Register(
                nameof(PageOffset),
                typeof(Point),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = delegate (DependencyObject d, object baseValue)
                    {
                        // 强制保留 2 位小数
                        // Round up to 2 decimal paleces.
                        Point offset = (Point)baseValue;
                        Math.Round(offset.X, 2);
                        Math.Round(offset.Y, 2);
                        return offset;
                    },

                    PropertyChangedCallback = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
                    {
                        GraphicVisualPage page = d as GraphicVisualPage;

                        // 更新页面内图形的显示位置
                        // Update graphic objects' position in the page.
                        page.SetVisualHostTranform();

                        // 更新网格的显示位置
                        // Update grid's position in the page.
                        page.SetGridVisualTranform();

                        // 激发 PageOffsetChangedEvent 事件
                        // Raise PageOffsetChangedEvent.
                        page.RaiseEvent(new PageRoutedEventArgs(PageOffsetChangedEvent, page));
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
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.None)
                {
                    CoerceValueCallback = CoercePageScaleValueCallback,
                    PropertyChangedCallback = PageScaleChangedCallback
                });
        //
        // Property Changed Callback
        //
        private static void PageScaleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 页面比例缩放时，同时调整页面偏移量，保证画面中心位置不变
            //
            double delt = (double)e.NewValue / (double)e.OldValue;
            GraphicVisualPage page = d as GraphicVisualPage;
            double offsetX = page.PageOffset.X * delt + page.ActualWidth / 2 * (1 - delt);
            double offsetY = page.PageOffset.Y * delt + page.ActualHeight / 2 * (1 - delt);
            page.PageOffset = new Point(offsetX, offsetY);

            // 更新网格 Update grid
            page.UpdateGridVisual();

            // 激发 PageScaleChangedEvent 事件
            // Raise PageScaleChangedEvent
            page.RaiseEvent(new PageRoutedEventArgs(PageScaleChangedEvent, page));
        }
        //
        // Coerce Value Callback
        //
        private static object CoercePageScaleValueCallback(DependencyObject d, object baseValue)
        {
            // 强制页面缩放比例范围 [0.01, 10], 保留2位小数。
            //
            double scale = Math.Round((double)baseValue, 2);
            return scale < 0.01 ? 0.01 : (scale > 10 ? 10 : scale);
        }

        #endregion

        #region PageSize
        /// <summary>
        /// 
        /// </summary>
        public Size PageSize
        {
            get { return (Size)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register(
                nameof(PageSize),
                typeof(Size),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(new Size(1000, 800), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        var page = d as GraphicVisualPage;

                        // 更新网格 Update grid
                        page.UpdateGridVisual();

                        // 激发 PageSizeChangedEvent 事件
                        // Raise PageSizeChangedEvent
                        page.RaiseEvent(new PageRoutedEventArgs(PageSizeChangedEvent, page));
                    }
                });
        #endregion

        #region PageBackColor
        /// <summary>
        /// 
        /// </summary>
        public SolidColorBrush PageBackColor
        {
            get { return (SolidColorBrush)GetValue(PageBackColorProperty); }
            set { SetValue(PageBackColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageBackColorProperty =
            DependencyProperty.Register(
                nameof(PageBackColor),
                typeof(SolidColorBrush),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        // 更新网格 Update grid
                        (d as GraphicVisualPage).UpdateGridVisual();
                    }
                });
        #endregion

        #region GridSize 
        /// <summary>
        /// 
        /// </summary>
        public double GridSize
        {
            get { return (double)GetValue(GridSizeProperty); }
            set { SetValue(GridSizeProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register(
                nameof(GridSize),
                typeof(double),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        double size = Math.Round((double)baseValue, 0);
                        return size < 1 ? 1 : (size > 50 ? 50 : size);
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        // 更新网格 Update grid
                        (d as GraphicVisualPage).UpdateGridVisual();
                    }
                });
        #endregion

        #region GridColor 
        /// <summary>
        /// 
        /// </summary>
        public Color GridColor
        {
            get { return (Color)GetValue(GridColorProperty); }
            set { SetValue(GridColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(
                nameof(GridColor),
                typeof(Color),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(Colors.Gray, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        // 更新网格 Update grid
                        (d as GraphicVisualPage).UpdateGridVisual();
                    }
                });
        #endregion

        #region ShowGrid 
        /// <summary>
        /// 
        /// </summary>
        public bool ShowGrid
        {
            get { return (bool)GetValue(ShowGridProperty); }
            set { SetValue(ShowGridProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register(
                nameof(ShowGrid),
                typeof(bool),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        // 更新网格 Update grid
                        (d as GraphicVisualPage).UpdateGridVisual();
                    }
                });
        #endregion

        #region Background 
        /// <summary>
        /// 
        /// </summary>
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                nameof(Background),
                typeof(Brush),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(Brushes.Transparent.CloneCurrentValue(), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback=(d,value)=>
                    {
                        return value != null ? value : Brushes.Transparent.CloneCurrentValue();
                    }
                });
        #endregion

        #region VisualHost 
        /// <summary>
        /// 
        /// </summary>
        public GroupVisual VisualHost
        {
            get { return (GroupVisual)GetValue(VisualHostProperty); }
            set { SetValue(VisualHostProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty VisualHostProperty =
            DependencyProperty.Register(
                nameof(VisualHost),
                typeof(GroupVisual),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None)
                {
                    PropertyChangedCallback = VisualHostChangedCallback
                });
        //
        // Property Changed Callback
        //
        private static void VisualHostChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GraphicVisualPage page = d as GraphicVisualPage;
            if (e.OldValue != null)
                page.Children.Remove((Visual)e.OldValue);
            if (e.NewValue != null)
            {
                page.Children.Add((Visual)e.NewValue);
                page.SetVisualHostTranform();
            }
        }
        #endregion

        #endregion


        #region Routed events

        #region PageOffsetChanged 事件
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler PageOffsetChanged
        {
            add { this.AddHandler(PageOffsetChangedEvent, value); }
            remove { this.RemoveHandler(PageOffsetChangedEvent, value); }
        }
        //
        // Routed event definition
        //
        public static readonly RoutedEvent PageOffsetChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageOffsetChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #region PageScaleChanged 事件
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler PageScaleChanged
        {
            add { this.AddHandler(PageScaleChangedEvent, value); }
            remove { this.RemoveHandler(PageScaleChangedEvent, value); }
        }
        //
        // Routed event definition
        //
        public static readonly RoutedEvent PageScaleChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageScaleChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #region PageSizeChanged 事件
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler PageSizeChanged
        {
            add { this.AddHandler(PageSizeChangedEvent, value); }
            remove { this.RemoveHandler(PageSizeChangedEvent, value); }
        }
        //
        // Routed event definition
        //
        public static readonly RoutedEvent PageSizeChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageSizeChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #region PageRenderSizeChanged 事件
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler PageRenderSizeChanged
        {
            add { this.AddHandler(PageRenderSizeChangedEvent, value); }
            remove { this.RemoveHandler(PageRenderSizeChangedEvent, value); }
        }
        //
        // Routed event definition
        //
        public static readonly RoutedEvent PageRenderSizeChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageRenderSizeChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #endregion


        #region Members related to graphic rendering support in WPF  

        private readonly DrawingVisual _gridVisual;

        public DrawingVisual GridVisual => _gridVisual;

        private readonly VisualCollection _children;

        public VisualCollection Children => _children;

        protected override Visual GetVisualChild(int index) => Children[index];

        protected override int VisualChildrenCount => Children.Count;

        #endregion


        #region Constructor

        public GraphicVisualPage()
        {
            _gridVisual = new DrawingVisual();
            _children = new VisualCollection(this);
            _children.Add(_gridVisual);

            Loaded += (sender, e) => UpdateGridVisual();
        }

        #endregion


        private void UpdateGridVisual()
        {
            if (GridVisual != null && IsLoaded)
            {
                DrawingContext dc = GridVisual.RenderOpen();

                Matrix mtx = PresentationSource.FromVisual(GridVisual).CompositionTarget.TransformToDevice;
                double dpiFactor = 1 / mtx.M11;
                double delt = dpiFactor / 2;

                double xlen = PageSize.Width * PageScale;
                double ylen = PageSize.Height * PageScale;

                Brush brush = new SolidColorBrush(GridColor);
                Pen majorPen = new Pen(brush.CloneCurrentValue(), 1 * dpiFactor);
                brush.Opacity = 0.5;
                Pen minorPen = new Pen(brush.CloneCurrentValue(), 1 * dpiFactor);

                if (majorPen.CanFreeze) majorPen.Freeze();
                if (minorPen.CanFreeze) minorPen.Freeze();

                double border = 10 * PageScale;

                GuidelineSet guidelineSet = new GuidelineSet();
                guidelineSet.GuidelinesX.Add(0 - delt);
                //guidelineSet.GuidelinesX.Add(-border - delt);
                guidelineSet.GuidelinesX.Add(xlen - delt);
                //guidelineSet.GuidelinesX.Add(xlen + border * 2.0 - delt);

                guidelineSet.GuidelinesY.Add(0 - delt);
                //guidelineSet.GuidelinesY.Add(-border - delt);
                guidelineSet.GuidelinesY.Add(ylen - delt);
                //guidelineSet.GuidelinesY.Add(ylen + border * 2.0 - delt);

                dc.PushGuidelineSet(guidelineSet);
                dc.DrawRectangle(PageBackColor, majorPen, new Rect(-border, -border, xlen + border * 2.0, ylen + border * 2.0));
                dc.DrawRectangle(null, minorPen, new Rect(0, 0, xlen, ylen));
                dc.Pop();

                if (ShowGrid)
                {
                    LineGeometry lgx = new LineGeometry(new Point(0, 0), new Point(0, ylen));
                    LineGeometry lgy = new LineGeometry(new Point(0, 0), new Point(xlen, 0));
                    if (lgx.CanFreeze) lgx.Freeze();
                    if (lgy.CanFreeze) lgy.Freeze();


                    for (double x = 0; x <= PageSize.Width; x += GridSize)
                    {
                        GuidelineSet gridGuidelines = new GuidelineSet();
                        gridGuidelines.GuidelinesX.Add(x * PageScale - delt);

                        dc.PushGuidelineSet(gridGuidelines);
                        dc.PushTransform(new TranslateTransform(x * PageScale, 0));
                        dc.DrawGeometry(null, (x / GridSize) % 5 == 0 ? majorPen : minorPen, lgx);
                        dc.Pop();
                        dc.Pop();
                    }

                    for (double y = 0; y <= PageSize.Height; y += GridSize)
                    {
                        GuidelineSet gridGuidelines = new GuidelineSet();
                        gridGuidelines.GuidelinesY.Add(y * PageScale - delt);

                        dc.PushGuidelineSet(gridGuidelines);
                        dc.PushTransform(new TranslateTransform(0, y * PageScale));
                        dc.DrawGeometry(null, (y / GridSize) % 5 == 0 ? majorPen : minorPen, lgy);
                        dc.Pop();
                        dc.Pop();
                    }
                }

                dc.Close();
            }
        }

        private void SetGridVisualTranform()
        {
            if (GridVisual != null)
            {
                GridVisual.Transform = new TranslateTransform(PageOffset.X, PageOffset.Y);
            }
        }

        private void SetVisualHostTranform()
        {
            if (VisualHost != null)
            {
                TransformGroup trans = new TransformGroup();
                trans.Children.Add(new ScaleTransform(PageScale, PageScale));
                trans.Children.Add(new TranslateTransform(PageOffset.X, PageOffset.Y));

                VisualHost.Transform = trans;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ActualWidth, ActualHeight));
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (IsLoaded)
            {
                // 页面窗口大小改变时，调整页面偏移量，保证画面中心位置不变
                PageOffset = new Point(
                    PageOffset.X + (RenderSize.Width - sizeInfo.PreviousSize.Width) / 2,
                    PageOffset.Y + (RenderSize.Height - sizeInfo.PreviousSize.Height) / 2);

                // 激发 PageRenderSizeChangedEvent 路由事件
                RaiseEvent(new PageRoutedEventArgs(PageRenderSizeChangedEvent, this));
            }
        }

        public void SetViewPortCenter(Point point)
        {

        }

        public void SetViewPortFullPage()
        {

        }

    }
}
