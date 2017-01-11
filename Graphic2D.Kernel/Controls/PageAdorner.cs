using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    public class PageAdorner : Adorner
    {
        #region Members related to graphic rendering support in WPF  

        private readonly Canvas _canvas = new Canvas();

        public Canvas Canvas => _canvas;

        protected override Visual GetVisualChild(int index) => index == 0 ? Canvas : null;

        protected override int VisualChildrenCount => 1;

        #endregion


        public PageAdorner(UIElement adornedElement)
            : base(adornedElement)
        {            
            this.AddVisualChild(_canvas);

            




        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = base.ArrangeOverride(finalSize);
            if (_canvas != null)
            {
                var page = (AdornedElement as GraphicVisualPage);
                _canvas.RenderTransform = new TranslateTransform(page.PageOffsetX, page.PageOffsetY);
                _canvas.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));

                //UpdateOperators();
            }
            return size;
        }

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);

        //    drawingContext.DrawRectangle(
        //        new SolidColorBrush(Colors.LightBlue) { Opacity = 0.3 },
        //        null,
        //        new Rect(0, 0, ActualWidth, ActualHeight));
        //    drawingContext.DrawRectangle(
        //        null,
        //        new Pen(Brushes.Red, 2),
        //        new Rect(0, 0, 200, 400));

        //}
    }
}
