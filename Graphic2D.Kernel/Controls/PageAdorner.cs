using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    public class PageAdorner : Adorner
    {
        #region Members related to graphic rendering support in WPF  

        //private readonly VisualCollection _children;

        //public VisualCollection Children => _children;

        //protected override Visual GetVisualChild(int index) => Children[index];

        //protected override int VisualChildrenCount => Children.Count;


        private readonly Grid _canvas = new Grid();

        public Grid Canvas => _canvas;

        protected override Visual GetVisualChild(int index) => index == 0 ? Canvas : null;

        protected override int VisualChildrenCount => 1;

        #endregion


        public PageAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            this.AddVisualChild(_canvas);
            this.AddLogicalChild(_canvas);
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(
                new SolidColorBrush(Colors.LightBlue) { Opacity = 0.3 },
                null,
                new Rect(0, 0, ActualWidth, ActualHeight));
            drawingContext.DrawRectangle(
                null,
                new Pen(Brushes.Red,2),
                new Rect(0, 0, 200, 400));

        }
    }
}
