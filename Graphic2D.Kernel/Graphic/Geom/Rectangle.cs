using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Graphic.Geom
{
    /// <summary>
    /// 圆角矩形几何。
    /// A rounded rectangle geometry.
    /// </summary>
    public struct Rectangle : IGeom
    {
        #region Field

        private Rect _rect;

        #endregion


        #region Properties

        /// <summary>
        /// 获取或设置一个 Rect 结构，该结构表示了矩形的宽度、高度和左上角位置。
        /// Gets and sets the Rect structure that specifies the width, 
        /// height and location of the rectangle geometry.
        /// </summary>
        public Rect Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        /// <summary>
        /// 获取或设置矩形 x 轴方向的圆角半径。
        /// Gets and sets the radius in the X dimension of the rounded corners.
        /// </summary>
        public double RadiusX { get; set; }

        /// <summary>
        /// 获取或设置矩形 y 轴方向的圆角半径。
        /// Gets and sets the radius in the Y dimension of the rounded corners.
        /// </summary>
        public double RadiusY { get; set; }

        /// <summary>
        /// 获取或设置矩形几何的宽度和高度。
        /// Gets or sets the width and height of the rectangle geometry.
        /// </summary>
        public Size Size
        {
            get { return _rect.Size; }
            set { _rect.Size = value; }
        }

        /// <summary>
        /// 获取或设置矩形几何左边的 x 轴值。
        /// Gets or sets the x-axis value of the left side of the rectangle geometry.
        /// </summary>
        public double X
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }

        /// <summary>
        /// 获取或设置矩形几何顶边的 y 轴值。
        /// Gets or sets the y-axis value of the left side of the rectangle geometry.
        /// </summary>
        public double Y
        {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }

        /// <summary>
        /// 获取或设置矩形几何的宽度。
        /// Gets or sets the width of the rectangle geometry.
        /// </summary>
        public double Width
        {
            get { return _rect.Width; }
            set { _rect.Width = value; }
        }

        /// <summary>
        /// 获取或设置矩形几何的高度。
        /// Gets or sets the height of the rectangle geometry.
        /// </summary>
        public double Height
        {
            get { return _rect.Height; }
            set { _rect.Height = value; }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// 初始化一个 Rectangle 结构的实例。
        /// Initializes a new instance of the Rectangle structure.
        /// </summary>
        /// <param name="rect">
        /// 表示矩形几何的宽度、高度和左上角位置的 Rect 结构。
        /// A Rect structure that specifies the width, height and location of the rectangle geometry.
        /// </param>
        /// <param name="radiusX">
        /// 矩形几何 x 轴方向的圆角半径，缺省值为0。
        /// The radius in the X dimension of the rounded corners，and the default value is 0.
        /// </param>
        /// <param name="radiusY">
        /// 矩形几何 y 轴方向的圆角半径，缺省值为0。
        /// The radius in the Y dimension of the rounded corners，and the default value is 0.
        /// </param>
        public Rectangle(Rect rect, double radiusX = 0, double radiusY = 0)
        {
            _rect = rect;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        #endregion


        #region IGeom members 

        /// <summary>
        /// 默认的圆角矩形几何，位于 (0,0)，宽度和长度为 100，圆角半径为 0。
        /// Default rounded rectangle geometry that is loacted at (0,0), 
        /// the width and heigth are 100, corner radii are 0.
        /// </summary>
        public IGeom DefaultGeom => new Rectangle(new Rect(0, 0, 100, 100));

        /// <summary>
        /// 绘制圆角矩形。
        /// Draws rounded rectangle. 
        /// </summary>
        public void DrawGeom(DrawingContext dc, Brush fill, Pen stroke)
        {
            dc.DrawRoundedRectangle(fill, stroke, Rect, RadiusX, RadiusX);
        }
        #endregion
    }
}
