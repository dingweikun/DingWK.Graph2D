using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphic
{
    public class GraphicVisualHost : UIElement
    {

        #region WPF visual display support

        private readonly VisualCollection _visuals;

        public VisualCollection Visuals => _visuals;

        protected override int VisualChildrenCount => _visuals.Count;

        protected override Visual GetVisualChild(int index) => _visuals[index];

        #endregion


        public GraphicVisualHost()
        {
            _visuals = new VisualCollection(this);
        }


    }
}
