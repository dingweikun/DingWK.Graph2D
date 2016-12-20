using Graphic2D.Kernel.Graphic;
using System.ComponentModel;
using System.Windows;

namespace Graphic2D.Kernel.GraphicVisuals
{
    /// <summary> 
    /// 提供有关显示更新和变换操作的方法。
    /// Provides methods related to display updates and transform operations.
    /// </summary>
    public interface IGraphicVisual : IGraphic, INotifyPropertyChanged
    {
        /// <summary>
        /// 更新几何变换状态。Update transformation state. 
        /// </summary>
        void UpdateTransform();

        /// <summary>
        /// 更新可视元素的填充状态。Update visual element fill state.
        /// </summary>
        void UpdateVisualFill();

        /// <summary>
        /// 更新可视元素的轮廓绘制状态。Update visual element stroke drawing state.
        /// </summary>
        void UpdateVisualStroke();

        /// <summary>
        /// 旋转操作。Rotation transform operation.
        /// </summary>
        /// <param name="angle">
        /// 旋转角度，单位度。Rotation angle which is in degrees.
        /// </param>
        /// <param name="center">
        /// 旋转中心点。Rotation transform center point.
        /// </param>
        /// <param name="IsLocalCenter">
        /// 旋转中心点位置是否为局部坐标。Whether the rotation center point is in local coordinate.
        /// </param>
        void Rotate(double angle, Point center, bool IsLocalCenter);

        /// <summary>
        /// 比例缩放操作。Scale transform operation.
        /// </summary>
        /// <param name="scaleX">
        /// X 轴的缩放比例。The x-axis scale transform factor.
        /// </param>
        /// <param name="scaleY">
        /// Y 轴的缩放比例。The y-axis scale transform factor.
        /// </param>
        /// <param name="centerX">
        /// 缩放中心点的 X 坐标。The x-coordinate of scale transform center point.
        /// </param>
        /// <param name="centerY">
        /// 缩放中心点的 Y 坐标。The y-coordinate of scale transform center point.
        /// </param>
        /// <param name="IsLocalCenter">
        /// 缩放中心点位置是否为局部坐标。Whether the scale center point is in local coordinate.
        /// </param>
        void Scale(double scaleX, double scaleY, double centerX, double centerY, bool IsLocalCenter);

        /// <summary>
        /// 平移操作。Translation transform operation.
        /// </summary>
        /// <param name="dx">
        /// 沿 X 轴方向的移动距离。The distance to translate along the x-axis.
        /// </param>
        /// <param name="dy">
        /// 沿 Y 轴方向的移动距离。The distance to translate along the y-axis.
        /// </param>
        void Translate(double dx, double dy);

    }
}
