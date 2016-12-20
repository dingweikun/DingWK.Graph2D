using Graphic2D.Kernel.Graphic.Geom;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Graphic
{
    /// <summary>
    /// 泛型图形类，表示一个强类型的图形对象。
    /// Generics graphic class，represents a strongly typed graphic object.
    /// </summary>
    /// <typeparam name="T">
    /// 图形对象内部几何形状的类型。The internal geometry type of graphic object.
    /// </typeparam>
    public class Graphic<T> : IGraphic
        where T : IGeom
    {
        #region Internal field

        private GraphicInfo _graphicInfo;

        #endregion


        #region Properties

        /// <summary>
        /// 访问或设置一个 GraphicInfo 结构，该结构指定了图形对象的轮廓画笔、填充画刷、局部坐标系原点位置和旋转角度。
        /// Gets and sets a GraphicInfo structure that specifies the stroke pen, fill brush, 
        /// local coordinate origin and rotation angle. 
        /// </summary>
        public GraphicInfo GraphicInfo
        {
            get { return _graphicInfo; }
            set { _graphicInfo = value; }
        }

        /// <summary>
        /// 访问或设置图形对象的内部几何形状。
        /// Gets and sets the geometry of graphic instance.
        /// </summary>
        public T Geom { get; set; }

        #endregion


        #region IGraphic interface members

        /// <summary>
        /// 获取或设置填充图形对象的画刷，如果为 null，则不填充图形对象。
        /// Gets and sets the brush with which to fill the graphic instance. If the brush is null, no fill is drawn.
        /// </summary>
        public Brush Fill
        {
            get { return _graphicInfo.Fill; }
            set { _graphicInfo.Fill = value; }
        }

        /// <summary>
        /// 获取或设置绘制图形对象轮廓的画笔，如果为 null，则不绘制图形对象的轮廓。
        /// Gets and sets the pen with which to stroke the graphic instance. If the pen is null, no stroke is drawn.
        /// </summary>
        public Pen Stroke
        {
            get { return _graphicInfo.Stroke; }
            set { _graphicInfo.Stroke = value; }
        }

        /// <summary>
        /// 获取或设置图形对象局部坐标系的原点位置。
        /// Gets and sets the local coordinate system origin of graphic instance.
        /// </summary>
        public Point Origin
        {
            get { return _graphicInfo.Origin; }
            set { _graphicInfo.Origin = value; }
        }

        /// <summary>
        /// 获取或设置图形对象局部坐标系的旋转角度，单位为度。
        /// Gets and sets the local coordinate system rotation angle of graphic instance， which is in degrees.
        /// </summary>
        public double Angle
        {
            get { return _graphicInfo.Angle; }
            set { _graphicInfo.Angle = value; }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// 初始化一个图形对象实例。Initializes a new graphic instance.
        /// </summary>
        /// <param name="geom">
        /// 图形的几何形状。The internal geometry of the graphic instance.
        /// </param>
        /// <param name="graphicInfo">
        /// 图形的画笔、画刷和局部坐标系信息。
        /// The information for setting the stroke pen，fill brush， 
        /// local coordinate origin and rotation angle of the graphic instance.
        /// </param>
        public Graphic(T geom, GraphicInfo graphicInfo)
        {
            Geom = geom;
            _graphicInfo = graphicInfo;
        }

        /// <summary>
        /// 初始化一个图形对象实例。Initializes a new graphic instance.
        /// </summary>
        /// <param name="geom">
        /// 图形的几何形状。The internal geometry of the graphic instance.
        /// </param>
        /// <param name="angle">
        /// 图形局部坐标系的旋转角度。The local coordinate rotation angle of the graphic instance.
        /// </param>
        /// <param name="origin">
        /// 图形局部坐标系的原点。The local coordinate origin of the graphic instance.
        /// </param>
        /// <param name="fill">
        /// 填充图形的画刷。The brush with which to fill the graphic instance.
        /// </param>
        /// <param name="stroke">
        /// 绘制图形轮廓的画笔。The pen with which to stroke the graphic instance.
        /// </param>
        public Graphic(T geom, Brush fill, Pen stroke, Point origin, double angle)
            : this(geom, new GraphicInfo() { Fill = fill, Stroke = stroke, Origin = origin, Angle = angle })
        {
        }

        /// <summary>
        /// 初始化一个局部坐标原点位置在（0,0），旋转角度为 0 的图形对象实例。
        /// Initializes a new graphic instance. 
        /// The graphic local coordinate origin is located at (0,0) and without rotation.
        /// </summary>
        /// <param name="geom">
        /// 图形的几何形状。The internal geometry of the graphic instance.
        /// </param>
        /// <param name="fill">
        /// 填充图形的画刷。The brush with which to fill the graphic instance.
        /// </param>
        /// <param name="stroke">
        /// 绘制图形轮廓的画笔。The pen with which to stroke the graphic instance.
        /// </param>
        public Graphic(T geom, Brush fill, Pen stroke)
            : this(geom, fill, stroke, new Point(0, 0), 0)
        {
        }

        /// <summary>
        /// 初始化一个局部坐标原点位置在（0,0），旋转角度为 0 的图形对象实例，该图形轮廓是线宽为 1 的黑色实线，填充画刷为白色。
        /// Initializes a new graphic instance. The graphic local coordinate origin is located at (0,0) and without rotation.
        /// The graphic stroke is drawn by 1 thinkness black solidline pen，and filled with a white solidbrush. 
        /// </summary>
        /// <param name="geom">
        /// 图形的几何形状。The internal geometry of the graphic instance.
        /// </param>
        public Graphic(T geom)
            : this(geom, Brushes.White, new Pen(Brushes.Black, 1), new Point(0, 0), 0)
        {
        }

        /// <summary>
        /// 初始化一个图形对象的副本实例。Initializes a new copy instance by specific graphic object.
        /// </summary>
        public Graphic(Graphic<T> graphic)
            : this(graphic.Geom, graphic.GraphicInfo)
        {
        }

        #endregion

    }
}
