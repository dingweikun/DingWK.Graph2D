using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Graphic
{
    /// <summary>
    /// 用于记录 IGraphic 对象信息的结构类型。
    /// A struct for holding the IGraphic instance information.
    /// </summary>
    public struct GraphicInfo : IGraphic
    {
        #region internal field
        private Brush _fill;
        private Pen _stroke;
        private Point _origin;
        private double _angle;
        #endregion

        #region IGraphic interface members

        /// <summary>
        /// 用于填充 IGraphic 对象的画刷，如果为 null，则不填充 IGraphic 对象。
        /// The brush with which to fill the IGraphic instance. If the brush is null, no fill is drawn.
        /// </summary>
        public Brush Fill
        {
            get { return _fill; }
            set { _fill = (Brush)value?.GetAsFrozen(); }
        }

        /// <summary>
        /// 用于绘制 IGraphic 对象轮廓的画笔，如果为 null，则不绘制 IGraphic 对象的轮廓。
        /// The pen with which to stroke the IGraphic instance. If the pen is null, no stroke is drawn.
        /// </summary>
        public Pen Stroke
        {
            get { return _stroke; }
            set { _stroke = (Pen)value?.GetAsFrozen(); }
        }
        
        /// <summary>
        /// IGraphic 对象的局部坐标系的原点位置。
        /// The local coordinate system origin of IGraphic instance.
        /// </summary>
        public Point Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        /// <summary>
        /// IGraphic 对象的局部坐标系的旋转角度，单位为度。
        /// The local coordinate system  rotation angle of IGraphic instance， which is in degrees.
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set { _angle = value % 360; }
        }

        #endregion

    }
}
