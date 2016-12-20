using Graphic2D.Kernel.Graphic;
using System.ComponentModel;
using System.Windows;

namespace Graphic2D.Kernel.GraphicVisuals
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraphicVisual : IGraphic, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        void UpdateTransform();
        void UpdateVisualFill();
        void UpdateVisualStroke();


        void Rotate(double angle, Point center, bool IsLocalCenter);
        void Move(double dx, double dy);
        void Scale(double rx, double ry, double cx, double cy);

    }
}
