using System.Windows;

namespace Graphic2D.Kernel.Controls
{
    /// <summary>
    /// 包含 GraphicVisualPage 页面信息的路由事件参数。
    /// Contains state information of GraphicVisualPage.
    /// </summary>
    internal class PageRoutedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public double PageScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Size RenderSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Point PageOffset { get; set; }


        public PageRoutedEventArgs(RoutedEvent routedEvent, GraphicVisualPage source)
            : base(routedEvent, source)
        {
            PageScale = source.PageScale;
            PageSize = source.PageSize;
            RenderSize = source.RenderSize;
            PageOffset = new Point(source.PageOffsetX, source.PageOffsetY);
        }

    }
}
