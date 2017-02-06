using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    /// <summary>
    /// 用于记录 GraphicVisual 对象信息的结构类型。
    /// A struct for holding the GraphicVisual instance information.
    /// </summary>
    public struct VisualInfo : IVisualInfo
    {

        #region Internal fields

        private double _angle;
        private Point _origin;
        private Brush _fill;
        private Pen _stroke;

        #endregion


        #region Static member

        public static VisualInfo Default => new VisualInfo()
        {
            Angle = 0,
            Origin = new Point(0, 0),
            Fill = Brushes.White,
            Stroke = new Pen(Brushes.Black, 1.0)
        };

        #endregion


        #region IVisualInfo interface members

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
        /// The local coordinate system rotation angle of IGraphic instance， which is in degrees.
        /// </summary>
        public double Angle
        {
            get { return _angle; }
            set { _angle = value % 360; }
        }

        #endregion

    }
}
