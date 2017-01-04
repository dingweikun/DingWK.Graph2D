using System.Windows;
using System.Windows.Controls;

namespace Graphic2D.Kernel.Controls
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [TemplatePart(Name ="PART_GraphicVisualPage", Type =typeof(GraphicVisualPage))]
    public class GraphicVisualCanvas : Control
    {
        static GraphicVisualCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphicVisualCanvas), new FrameworkPropertyMetadata(typeof(GraphicVisualCanvas)));
        }
    }
}
