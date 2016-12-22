using Graphic2D.Kernel.GraphicVisuals;
using System;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    /// <summary>
    /// 提供图形显示支持的基础界面元素。
    /// A basic UI element which provides graphic rendering support.
    /// </summary>
    public class GraphicVisualPage : FrameworkElement
    {
        #region Properties

        #region PageOffsetX 属性
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
                    CoerceValueCallback = delegate (DependencyObject d, object baseValue)
                    {
                        // 强制保留 2 位小数
                        //
                        return Math.Round((double)baseValue, 2);
                    },
                    PropertyChangedCallback = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
                    {
                        GraphicVisualPage page = d as GraphicVisualPage;

                        // 更新页面内图形的显示位置
                        //
                        page.SetGraphicHostTranform();

                        // 激发事件
                        //
                        page.RaiseEvent(new PageRoutedEventArgs(PageOffsetChangedEvent, page));
                    }
                });

        #endregion

        #region PageOffsetY 属性
        //
        // CLR属性包装
        //
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
                    CoerceValueCallback = delegate (DependencyObject d, object baseValue)
                    {
                        // 强制保留 2 位小数
                        return Math.Round((double)baseValue, 2);
                    },
                    PropertyChangedCallback = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
                    {
                        GraphicVisualPage page = d as GraphicVisualPage;

                        // 更新页面内图形的显示位置
                        page.SetGraphicHostTranform();

                        // 激发 PageOffsetChangedEvent 事件
                        page.RaiseEvent(new PageRoutedEventArgs(PageOffsetChangedEvent, page));
                    }
                });
        #endregion

        #region PageScale 属性
        //
        // CLR属性包装
        //
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
        // 回调函数
        //
        private static void PageScaleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 页面比例缩放时，同时调整页面偏移量，保证画面中心位置不变
            double delt = (double)e.NewValue / (double)e.OldValue;
            GraphicVisualPage page = d as GraphicVisualPage;
            double offsetX = page.PageOffsetX * delt + page.ActualWidth / 2 * (1 - delt);
            double offsetY = page.PageOffsetY * delt + page.ActualHeight / 2 * (1 - delt);
            page.PageOffsetX = offsetX;
            page.PageOffsetY = offsetY;

            // 激发 PageScaleChangedEvent 事件
            page.RaiseEvent(new PageRoutedEventArgs(PageScaleChangedEvent, page));
        }
        //
        // 强制函数
        //
        private static object CoercePageScaleValueCallback(DependencyObject d, object baseValue)
        {
            // 强制页面缩放比例范围 [0.01, 10], 保留2位小数。
            double scale = Math.Round((double)baseValue, 2);
            return scale < 0.01 ? 0.01 : (scale > 10 ? 10 : scale);
        }

        #endregion

        #region PageSize 属性
        //
        // CLR属性包装
        //
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
                    PropertyChangedCallback = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
                    {
                        var page = d as GraphicVisualPage;
                        page.RaiseEvent(new PageRoutedEventArgs(PageSizeChangedEvent, page));
                    }
                });
        #endregion

        #region PageBackColor 属性
        //
        // CLR属性包装
        //
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

        #region PageBorderWidth 属性
        //
        // CLR属性包装
        //
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

        #region GridSize 属性
        //
        // CLR属性包装
        //
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

        #region GridColor 属性
        //
        // CLR属性包装
        //
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

        #region ShowGrid 属性
        //
        // CLR属性包装
        //
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

        #region Background 属性
        //
        // CLR属性包装
        //
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

        #region GraphicHost 属性
        //
        // CLR属性包装
        //
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
        // 回调函数
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
        //
        // CLR 事件包装
        //
        public event RoutedEventHandler PageOffsetChanged
        {
            add { this.AddHandler(PageOffsetChangedEvent, value); }
            remove { this.RemoveHandler(PageOffsetChangedEvent, value); }
        }
        //
        // 路由事件定义
        //
        public static readonly RoutedEvent PageOffsetChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageOffsetChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #region PageScaleChanged 事件
        //
        // CLR 事件包装
        //
        public event RoutedEventHandler PageScaleChanged
        {
            add { this.AddHandler(PageScaleChangedEvent, value); }
            remove { this.RemoveHandler(PageScaleChangedEvent, value); }
        }
        //
        // 路由事件定义
        //
        public static readonly RoutedEvent PageScaleChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageScaleChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #region PageSizeChanged 事件
        //
        // CLR 事件包装
        //
        public event RoutedEventHandler PageSizeChanged
        {
            add { this.AddHandler(PageSizeChangedEvent, value); }
            remove { this.RemoveHandler(PageSizeChangedEvent, value); }
        }
        //
        // 路由事件定义
        //
        public static readonly RoutedEvent PageSizeChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageSizeChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #region PageRenderSizeChanged 事件
        //
        // CLR 事件包装
        //
        public event RoutedEventHandler PageRenderSizeChanged
        {
            add { this.AddHandler(PageRenderSizeChangedEvent, value); }
            remove { this.RemoveHandler(PageRenderSizeChangedEvent, value); }
        }
        //
        // 路由事件定义
        //
        public static readonly RoutedEvent PageRenderSizeChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(PageRenderSizeChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(GraphicVisualPage));
        #endregion

        #endregion


        #region Members related to graphic rendering support  

        private readonly VisualCollection _children;

        public VisualCollection Children => _children;

        protected override Visual GetVisualChild(int index) => Children[index];

        protected override int VisualChildrenCount => Children.Count;

        #endregion



        private void SetGraphicHostTranform()
        {
            throw new NotImplementedException();
        }

    }
}
