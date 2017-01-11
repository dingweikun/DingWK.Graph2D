using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graphic2D.Kernel.Controls
{
    [TemplatePart(Name = "PART_Page", Type = typeof(GraphicVisualPage))]
    [TemplatePart(Name = "PART_HorScrollBar", Type = typeof(ScrollBar))]
    [TemplatePart(Name = "PART_VerScrollBar", Type = typeof(ScrollBar))]
    public class GraphicVisualCanvas : Control
    {

        private PageAdorner _pageAdorner;
        public PageAdorner PageAdorner => _pageAdorner;



        static GraphicVisualCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicVisualCanvas), new FrameworkPropertyMetadata(typeof(GraphicVisualCanvas)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GraphicVisualPage page = GetTemplateChild("PART_Page") as GraphicVisualPage;
            AdornerDecorator addr = GetTemplateChild("PART_AdornerDecorator") as AdornerDecorator;

            if (page != null && addr != null)
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

                //page.PageOffsetChanged += (sender, e) =>
                //{
                //    SetScrollBars(e as PageRoutedEventArgs);
                //    e.Handled = true;
                //};


                _pageAdorner = new PageAdorner(page);
                _pageAdorner.Canvas.Children.Add(new Rectangle() { Width=200,Height=100, Fill=Brushes.Yellow});
                //_pageAdorner.LayoutTransform = new TranslateTransform(100, 200);
                addr.AdornerLayer.Add(_pageAdorner);



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
