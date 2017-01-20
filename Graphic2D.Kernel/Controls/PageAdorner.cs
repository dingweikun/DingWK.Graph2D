﻿using System.Windows;
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
                _canvas.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
                _canvas.RenderTransform = new TranslateTransform(page.PageOffset.X, page.PageOffset.Y);

                //UpdateOperators();
            }
            return size;
        }
    }
}
