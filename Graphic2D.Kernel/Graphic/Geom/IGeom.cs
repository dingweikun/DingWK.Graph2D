using System.Windows.Media;

namespace Graphic2D.Kernel.Graphic.Geom
{
    /// <summary>
    /// 描述几何形状的接口。
    /// The geometry interface.
    /// </summary>
    public interface IGeom
    {
        /// <summary>
        /// 几何形状的默认实例。
        /// The default geometry.
        /// </summary>
        IGeom DefaultGeom { get; }

        /// <summary>
        /// 几何形状绘制方法。
        /// Geometry drawing method.
        /// </summary>
        void DrawGeom(DrawingContext dc, Brush fill, Pen stroke);
    }
}
