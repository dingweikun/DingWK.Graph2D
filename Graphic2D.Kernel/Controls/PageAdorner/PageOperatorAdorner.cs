using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Graphic2D.Kernel.Controls
{
    public class PageOperatorAdorner : Adorner
    {
        public Canvas _canvas;
        public PageOperator _operator;

        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index) => index != 0 ? null : _canvas;


        public PageOperatorAdorner(UIElement adornedElement) 
            : base(adornedElement)
        {
            _canvas = new Canvas();
            _canvas.Background = Brushes.Red.Clone();
            _canvas.Background.Opacity = 0.2;
            if (_canvas.Background.CanFreeze)
                _canvas.Background.Freeze();
            AddVisualChild(_canvas);
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            Size size = base.ArrangeOverride(finalSize);
            if (_canvas != null)
            {
                _canvas.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
                UpdateOperators();
            }
            return size;
        }


        protected void UpdateOperators()
        {
            var page = AdornedElement as GraphicVisualPage;
            Transform offsetTansform = new TranslateTransform(page.PageOffsetX, page.PageOffsetY);
            foreach (PageOperator oper in _canvas.Children)
            {
                if (oper != null)
                {
                    oper.Update(page.PageScale);
                    TransformGroup trans = new TransformGroup();
                    trans.Children.Add(oper.RenderTransform);
                    trans.Children.Add(offsetTansform);
                    oper.RenderTransform = trans;
                }
            }
        }




    }
}
