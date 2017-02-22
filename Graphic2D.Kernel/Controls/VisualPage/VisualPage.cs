using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{

    public class VisualPage : FrameworkElement
    {
        const int GridSizeStep = 5;

        public DrawingVisual GridVisual { get; set; }


        #region OffsetX
        /// <summary>
        /// 
        /// </summary>
        public int OffsetX
        {
            get { return (int)GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty OffsetXProperty =
        DependencyProperty.Register(
                nameof(OffsetX),
                typeof(int),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        var page = d as VisualPage;

                        // 更新页面的显示偏移
                        // Update page offset trsansform.
                        page.UpdatePageOffsetTransform();

                        // 激发 PageOffsetChangedEvent 事件
                        // Raise PageOffsetChangedEvent.
                        page.RaiseEvent(new PageRoutedEventArgs(PageOffsetChangedEvent, page));
                    }
                });

        #endregion


        #region OffsetY
        /// <summary>
        /// 
        /// </summary>
        public int OffsetY
        {
            get { return (int)GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty OffsetYProperty =
        DependencyProperty.Register(
                nameof(OffsetY),
                typeof(int),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                    {
                        var page = d as VisualPage;

                        // 更新页面的显示偏移
                        // Update page offset trsansform.
                        page.UpdatePageOffsetTransform();

                        // 激发 PageOffsetChangedEvent 事件
                        // Raise PageOffsetChangedEvent.
                        page.RaiseEvent(new PageRoutedEventArgs(PageOffsetChangedEvent, page));
                    }
                });

        #endregion


        #region Scale

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty ScaleProperty =
        DependencyProperty.Register(
                nameof(Scale),
                typeof(double),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(0.0)
                {
                    CoerceValueCallback = CoerceScaleValueCallback,
                    PropertyChangedCallback = ScaleChangedCallback
                });
        //
        // Property Changed Callback
        //
        private static void ScaleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 更新网格 Update grid
            var page = d as VisualPage;
            page.UpdateGridVisual();

            // 页面比例缩放时，同时调整页面偏移量，保证画面中心位置不变
            //
            double delt = (double)e.NewValue / (double)e.OldValue;
            double offsetX = page.OffsetX * delt + page.ActualWidth / 2 * (1 - delt);
            double offsetY = page.OffsetY * delt + page.ActualHeight / 2 * (1 - delt);
            page.OffsetX = (int)offsetX;
            page.OffsetY = (int)offsetY;

            // 激发 PageScaleChangedEvent 事件
            // Raise PageScaleChangedEvent
            page.RaiseEvent(new PageRoutedEventArgs(PageScaleChangedEvent, page));
        }

        //
        // Coerce Value Callback
        //
        private static object CoerceScaleValueCallback(DependencyObject d, object baseValue)
        {
            // 强制页面缩放比例范围 [0.01, 10], 保留2位小数。
            //
            double scale = Math.Round((double)baseValue, 2);
            return scale < 0.01 ? 0.01 : (scale > 10 ? 10 : scale);
        }
        #endregion


        #region PageSize

        public Size PageSize
        {
            get { return (Size)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        private static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register(
                nameof(PageSize),
                typeof(Size),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(new Size(800, 600), FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                      {
                          var page = d as VisualPage;
                          page.UpdateGridVisual();

                          // 激发 PageSizeChangedEvent 事件
                          // Raise PageSizeChangedEvent
                          page.RaiseEvent(new PageRoutedEventArgs(PageSizeChangedEvent, page));
                      }
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
        private static readonly DependencyProperty GridSizeProperty =
            DependencyProperty.Register(
                nameof(GridSize),
                typeof(int),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    CoerceValueCallback = (d, value) =>
                    {
                        int n = (int)value / GridSizeStep;
                        return n > 0 ? n * GridSizeStep : GridSizeStep;
                    },

                    PropertyChangedCallback = (d, e) =>
                    {
                        (d as VisualPage).UpdateGridVisual();
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
        private static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(
                nameof(GridColor),
                typeof(Color),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(Colors.LightGray, FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    PropertyChangedCallback = (d, e) =>
                      {
                          (d as VisualPage).UpdateGridVisual();
                      }
                });
        #endregion


        #region BackColor
        /// <summary>
        /// 
        /// </summary>
        public Color BackColor
        {
            get { return (Color)GetValue(BackColorProperty); }
            set { SetValue(BackColorProperty, value); }
        }
        //
        // Dependency property definition
        //
        private static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register(
                nameof(BackColor),
                typeof(Color),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.AffectsRender));
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
        private static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register(
                nameof(ShowGrid),
                typeof(bool),
                typeof(VisualPage),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
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

        //#region PageRenderSizeChanged 事件
        ///// <summary>
        ///// 
        ///// </summary>
        //public event RoutedEventHandler PageRenderSizeChanged
        //{
        //    add { this.AddHandler(PageRenderSizeChangedEvent, value); }
        //    remove { this.RemoveHandler(PageRenderSizeChangedEvent, value); }
        //}
        ////
        //// Routed event definition
        ////
        //public static readonly RoutedEvent PageRenderSizeChangedEvent =
        //    EventManager.RegisterRoutedEvent(
        //        nameof(PageRenderSizeChanged),
        //        RoutingStrategy.Bubble,
        //        typeof(RoutedEventHandler),
        //        typeof(GraphicVisualPage));
        //#endregion

        #endregion




        private void UpdatePageOffsetTransform()
        {
            RenderTransform = new TranslateTransform(OffsetX, OffsetY);
            //RenderTransform = new TranslateTransform(OffsetX / Scale, OffsetY / Scale);
        }


        internal void UpdateGridVisual()
        {
            if (IsLoaded)
            {
                if (GridVisual == null)
                    GridVisual = new DrawingVisual();

                DrawingContext dc = GridVisual.RenderOpen();

                Matrix mtx = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice;
                double dpiFactor = 1 / mtx.M11;
                double delt = dpiFactor / 2;

                double xlen = PageSize.Width * Scale;
                double ylen = PageSize.Height * Scale;

                Brush brush = new SolidColorBrush(GridColor);
                Pen majorPen = new Pen(brush.CloneCurrentValue(), 1 * dpiFactor);
                brush.Opacity = 0.5;
                Pen minorPen = new Pen(brush.CloneCurrentValue(), 1 * dpiFactor);

                if (majorPen.CanFreeze) majorPen.Freeze();
                if (minorPen.CanFreeze) minorPen.Freeze();

                //double border = 10 * Scale;

                GuidelineSet guidelineSet = new GuidelineSet();
                guidelineSet.GuidelinesX.Add(0 - delt);
                guidelineSet.GuidelinesX.Add(xlen - delt);
                //guidelineSet.GuidelinesX.Add(-border - delt);
                //guidelineSet.GuidelinesX.Add(xlen + border * 2.0 - delt);

                guidelineSet.GuidelinesY.Add(0 - delt);
                guidelineSet.GuidelinesY.Add(ylen - delt);
                //guidelineSet.GuidelinesY.Add(-border - delt);
                //guidelineSet.GuidelinesY.Add(ylen + border * 2.0 - delt);

                dc.PushGuidelineSet(guidelineSet);
                //dc.DrawRectangle(BackColor, majorPen, new Rect(-border, -border, xlen + border * 2.0, ylen + border * 2.0));
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
                        gridGuidelines.GuidelinesX.Add(x * Scale - delt);

                        dc.PushGuidelineSet(gridGuidelines);
                        dc.PushTransform(new TranslateTransform(x * Scale, 0));
                        dc.DrawGeometry(null, (x / GridSize) % 5 == 0 ? majorPen : minorPen, lgx);
                        dc.Pop();
                        dc.Pop();
                    }

                    for (double y = 0; y <= PageSize.Height; y += GridSize)
                    {
                        GuidelineSet gridGuidelines = new GuidelineSet();
                        gridGuidelines.GuidelinesY.Add(y * Scale - delt);

                        dc.PushGuidelineSet(gridGuidelines);
                        dc.PushTransform(new TranslateTransform(0, y * Scale));
                        dc.DrawGeometry(null, (y / GridSize) % 5 == 0 ? majorPen : minorPen, lgy);
                        dc.Pop();
                        dc.Pop();
                    }
                }

                dc.Close();
            }

        }



        //#region PageGrid

        //public PageGrid PageGrid
        //{
        //    get { return (PageGrid)GetValue(GridProperty); }
        //    set { SetValue(GridProperty, value); }
        //}

        //public static DependencyProperty OffsetXProperty
        //{
        //    get
        //    {
        //        return OffsetXProperty1;
        //    }
        //}

        //public static DependencyProperty OffsetXProperty1
        //{
        //    get
        //    {
        //        return offsetXProperty;
        //    }
        //}

        //public static readonly DependencyProperty GridProperty =
        //    DependencyProperty.Register(nameof(PageGrid), typeof(PageGrid), typeof(VisualPage),
        //        new FrameworkPropertyMetadata(new PageGrid(), FrameworkPropertyMetadataOptions.AffectsRender)
        //        {
        //            CoerceValueCallback = (d, value) =>
        //            {
        //                return value != null ? value : new PageGrid();
        //            },

        //            PropertyChangedCallback = (d, e) =>
        //            {
        //                var page = d as VisualPage;
        //                if (page.IsLoaded)
        //                {
        //                    (e.NewValue as PageGrid).UpdateGirdVisual();
        //                }
        //            }
        //        });
        //#endregion




        public VisualPage()
        {


            Loaded += VisualPage_Loaded;
        }

        private void VisualPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGridVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (GridVisual != null)
            {
                UpdateGridVisual();
                drawingContext.DrawDrawing(GridVisual.Drawing);
            }

            drawingContext.DrawRectangle(null, new Pen(Brushes.Red,2), new Rect(0, 0, ActualWidth, ActualHeight));

        }



    }


}
