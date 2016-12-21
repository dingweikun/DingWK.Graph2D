using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Graphic
{
    /// <summary>
    /// 描述图形对象的接口。
    /// The graphic interface. 
    /// </summary>
    public interface IGraphic 
    {
        /// <summary>
        /// 用于填充 IGraphic 对象的画刷，如果为 null，则不填充 IGraphic 对象。
        /// The brush with which to fill the IGraphic instance. If the brush is null, no fill is drawn.
        /// </summary>
        Brush Fill { get; set; }

        /// <summary>
        /// 用于绘制 IGraphic 对象轮廓的画笔，如果为 null，则不绘制 IGraphic 对象的轮廓。
        /// The pen with which to stroke the IGraphic instance. If the pen is null, no stroke is drawn.
        /// </summary>
        Pen Stroke { get; set; }

        /// <summary>
        /// IGraphic 对象的局部坐标系的原点位置。
        /// The local coordinate system origin of IGraphic instance
        /// </summary>
        Point Origin { get; set; }

        /// <summary>
        /// IGraphic 对象的局部坐标系的旋转角度，单位为度。
        /// The local coordinate system  rotation angle of IGraphic instance， which is in degrees.
        /// </summary>
        double Angle { get; set; }

    }
}
