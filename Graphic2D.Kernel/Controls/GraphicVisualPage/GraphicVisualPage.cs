using Graphic2D.Kernel.GraphicVisuals;
using System;
using System.Windows;
using System.Windows.Documents;
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

        /// <summary>
        /// 
        /// </summary>
        public PageOperatorAdorner OperAdorner { get; private set; }


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
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        // 保留 2 位小数
                        // Round up to 2 decimal paleces.
                        return Math.Round((double)baseValue, 2);
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        GraphicVisualPage page = d as GraphicVisualPage;

                        // 更新页面内图形的显示位置
                        // Update graphic objects' position in the page.
                        page.SetGraphicHostTranform();

                        // 激发 PageOffsetChangedEvent 事件
                        // Raise PageOffsetChangedEvent.
                        page.RaiseEvent(new PageRoutedEventArgs(PageOffsetChangedEvent, page));
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
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = (d, baseValue) =>
                    {
                        // 保留 2 位小数
                        // Round up to 2 decimal paleces.
                        return Math.Round((double)baseValue, 2);
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        GraphicVisualPage page = d as GraphicVisualPage;

                        // 更新页面内图形的显示位置
                        // Update graphic objects' position in the page.
                        page.SetGraphicHostTranform();

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
            double offsetX = page.PageOffsetX * delt + page.ActualWidth / 2 * (1 - delt);
            double offsetY = page.PageOffsetY * delt + page.ActualHeight / 2 * (1 - delt);
            page.PageOffsetX = offsetX;
            page.PageOffsetY = offsetY;

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
                    // 回调函数，激发 PageSizeChangedEvent 路由事件
                    PropertyChangedCallback = (d, e) =>
                    {
                        var page = d as GraphicVisualPage;
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
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region PageBorderWidth
        /// <summary>
        /// 
        /// </summary>
        public double PageBorderWidth
        {
            get { return (double)GetValue(PageBorderWidthProperty); }
            set { SetValue(PageBorderWidthProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty PageBorderWidthProperty =
            DependencyProperty.Register(
                nameof(PageBorderWidth),
                typeof(double),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsRender));
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
                new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region GridColor 
        /// <summary>
        /// 
        /// </summary>
        public SolidColorBrush GridColor
        {
            get { return (SolidColorBrush)GetValue(GridColorProperty); }
            set { SetValue(GridColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(
                nameof(GridColor),
                typeof(SolidColorBrush),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.AffectsRender));
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
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
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
                new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region GraphicHost 
        /// <summary>
        /// 
        /// </summary>
        public GraphicVisualGroup GraphicHost
        {
            get { return (GraphicVisualGroup)GetValue(GraphicHostProperty); }
            set { SetValue(GraphicHostProperty, value); }
        }
        //
        // Dependency property definition
        //
        public static readonly DependencyProperty GraphicHostProperty =
            DependencyProperty.Register(
                nameof(GraphicHost),
                typeof(GraphicVisualGroup),
                typeof(GraphicVisualPage),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None)
                {
                    PropertyChangedCallback = GraphicHostChangedCallback
                });
        //
        // Property Changed Callback
        //
        private static void GraphicHostChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GraphicVisualPage page = d as GraphicVisualPage;
            if (e.OldValue != null)
                page.Children.Remove((Visual)e.OldValue);
            if (e.NewValue != null)
            {
                page.Children.Add((Visual)e.NewValue);
                page.SetGraphicHostTranform();
            }
        }
        #endregion

        #endregion


        #region Routed events

        #region PagePageOffsetChanged 事件
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

        private readonly VisualCollection _children;

        public VisualCollection Children => _children;

        protected override Visual GetVisualChild(int index) => Children[index];

        protected override int VisualChildrenCount => Children.Count;

        #endregion


        #region Constructor

        public GraphicVisualPage()
        {
            // 初始化可视化对象集合
            _children = new VisualCollection(this);

            Loaded += (sender, e) =>
            {
                AdornerLayer ad = AdornerLayer.GetAdornerLayer(this);

                if (ad != null)
                {
                    OperAdorner = new PageOperatorAdorner(this);
                    ad.Add(OperAdorner);
                    OperAdorner._canvas.Children.Add(new ResizeRotateOperator());

                    //OperAdorner.DataContext = this.GraphicHost[1];
                }

            };

        }

        #endregion


        private void SetGraphicHostTranform()
        {
            //throw new NotImplementedException();
        }

    }
}
