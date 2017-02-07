using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    [TemplatePart(Name = "PART_Page", Type = typeof(GraphicVisualPage))]
    [TemplatePart(Name = "PART_HorScrollBar", Type = typeof(ScrollBar))]
    [TemplatePart(Name = "PART_VerScrollBar", Type = typeof(ScrollBar))]
    [TemplatePart(Name = "PART_HorPageRuler", Type = typeof(HorPageRuler))]
    [TemplatePart(Name = "PART_VerPageRuler", Type = typeof(VerPageRuler))]
    public partial class GraphicVisualCanvas : Control
    {
        private PageOperatorAdorner _operatorAdorner;
        public PageOperatorAdorner OperatorAdorner => _operatorAdorner;

        private GraphicVisualPage _page;
        public GraphicVisualPage Page => _page;


        static GraphicVisualCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicVisualCanvas), new FrameworkPropertyMetadata(typeof(GraphicVisualCanvas)));
        }

        public GraphicVisualCanvas()
        {
            Loaded += (sender, e) =>
            {
                // 手动刷新一次, 初始化滚动条位置
                SetScrollBars(new PageRoutedEventArgs(null, Page));
            };
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GraphicVisualPage page = GetTemplateChild("PART_Page") as GraphicVisualPage;
            _page = page;           

            if (page != null)
            {
                page.PageSizeChanged += (sender, e) =>
                {
                    SetScrollBars(e as PageRoutedEventArgs);
                    e.Handled = true;
                };

                page.PageScaleChanged += (sender, e) =>
                {
                    SetScrollBars(e as PageRoutedEventArgs);
                    e.Handled = true;
                };

                page.PageRenderSizeChanged += (sender, e) =>
                {
                    SetScrollBars(e as PageRoutedEventArgs);
                    e.Handled = true;
                };

                page.PageOffsetChanged += (sender, e) =>
                {
                    // 强制刷新
                    OperatorAdorner.InvalidateVisual();
                    e.Handled = true;
                };
            }

            AdornerDecorator addr = GetTemplateChild("PART_AdornerDecorator") as AdornerDecorator;
            if (addr != null)
            {
                _operatorAdorner = new PageOperatorAdorner(page);
                //_pageAdorner.Canvas.Background = Brushes.LightGreen.Clone();
                //_pageAdorner.Canvas.Background.Opacity = 0.3;
                addr.AdornerLayer.Add(_operatorAdorner);
            }
            
        }


        private void SetScrollBars(PageRoutedEventArgs args)
        {
            ScrollBar hScroll = GetTemplateChild("PART_HorScrollBar") as ScrollBar;

            double rangeX = args.PageSize.Width * args.PageScale;
            hScroll.Minimum = -args.RenderSize.Width / 2;
            hScroll.Maximum = -args.RenderSize.Width / 2 + rangeX;
            hScroll.SmallChange = args.RenderSize.Width / 50;
            hScroll.LargeChange = args.RenderSize.Width;
            hScroll.ViewportSize = args.RenderSize.Width;

            ScrollBar vScroll = GetTemplateChild("PART_VerScrollBar") as ScrollBar;

            double rangeY = args.PageSize.Height * args.PageScale;
            vScroll.Minimum = -args.RenderSize.Height / 2;
            vScroll.Maximum = -args.RenderSize.Height / 2 + rangeY;
            vScroll.SmallChange = args.RenderSize.Height / 50;
            vScroll.LargeChange = args.RenderSize.Height;
            vScroll.ViewportSize = args.RenderSize.Height;
        }


    }
}
