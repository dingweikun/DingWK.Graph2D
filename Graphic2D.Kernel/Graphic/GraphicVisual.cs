using Graphic2D.Kernel.Geom;
using Graphic2D.Kernel.Graphic;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.GraphicVisuals
{
    /// <summary>
    /// 可视化图形类，内部封装了图形对象，并提供图形在 WPF 中的显示支持和变换操作。
    /// GraphicVisual is a visual element which wraps the graphic object inside. 
    /// GraphicVisual provides graphic rendering support in WPF and transform operations.
    /// </summary>
    public class GraphicVisual : DrawingVisual, IGraphicVisual
    {
        #region Internal field

        // 内部图形对象。
        // The internal graphic object.
        protected Graphic<IGeom> _graphic;

        #endregion


        #region Constructors

        /// <summary>
        /// 初始化一个可视化图形的实例。
        /// Initializes a GraphicVisual instance.
        /// </summary>
        public GraphicVisual(Graphic<IGeom> graphic)
        {
            if (graphic == null)
                throw new ArgumentNullException();
            _graphic = new Graphic<IGeom>(graphic);
            UpdateTransform();
            UpdateVisual();
        }

        /// <summary>
        /// 初始化一个可视化图形对象的副本实例。
        /// Initializes a new copy instance of specific GraphicVisual object.
        /// </summary>
        public GraphicVisual(GraphicVisual graphicVisual)
            : this(graphicVisual._graphic)
        {
        }

        #endregion


        #region Method

        /// <summary>
        /// 更新可视化元素。该方法将内部的图形对象绘制到可视化元素中，WPF将显示该可视化元素。
        /// This method renders graphic to the visual element which displays in WPF. 
        /// </summary>
        public void UpdateVisual()
        {
            DrawingContext dc = RenderOpen();
            _graphic.Geom.DrawGeom(dc, Fill, Stroke);
            dc.Close();
        }

        #endregion


        #region INotifyPropertyChanged interface implementation

        public event PropertyChangedEventHandler PropertyChanged;

        // 触发 PropertyChanged 事件的通用方法。
        // A general method to raise the PropertyChanged event.
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region IGraphic interface members

        /// <summary>
        /// 获取和设置可视化图形的旋转角度，单位为度。
        /// Gets and sets GraphicVisual rotation angle， which is in degrees.
        /// </summary>
        public virtual double Angle
        {
            get { return _graphic.Angle; }
            set
            {
                _graphic.Angle = value;
                UpdateTransform();
                NotifyPropertyChanged(nameof(Angle));
            }
        }

        /// <summary>
        /// 获取和设置填充画刷。
        /// Gets and sets fill brush.
        /// </summary>
        public virtual Brush Fill
        {
            get { return _graphic.Fill; }
            set
            {
                _graphic.Fill = value;
                UpdateVisual();
                NotifyPropertyChanged(nameof(Fill));
            }
        }

        /// <summary>
        /// 获取和设置可视化图形局部坐标系的原点位置。
        /// Gets and sets GraphicVisual local coordinate origin point.
        /// </summary>
        public virtual Point Origin
        {
            get { return _graphic.Origin; }
            set
            {
                _graphic.Origin = value;
                UpdateTransform();
                NotifyPropertyChanged(nameof(Origin));
            }
        }

        /// <summary>
        /// 获取和设置图形轮廓绘制画笔。
        /// Gets and sets stroke drawing pen.
        /// </summary>
        public virtual Pen Stroke
        {
            get { return _graphic.Stroke; }
            set
            {
                _graphic.Stroke = value;
                UpdateVisual();
                NotifyPropertyChanged(nameof(Stroke));
            }
        }

        #endregion


        #region IGraphicVisual interface members

        /// <summary>
        /// 更新可视化图形的几何变换状态。
        /// Update transformation state of GraphicVisual object. 
        /// </summary>
        public virtual void UpdateTransform()
        {
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new RotateTransform(Angle));
            tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
            Transform = tr;
        }

        /// <summary>
        /// 更新可视化图形的填充状态。
        /// Update fill state of GraphicVisual object.
        /// </summary>
        public virtual void UpdateVisualFill() => UpdateVisual();

        /// <summary>
        /// 更新可视化图形的轮廓绘制状态。
        /// Update stroke drawing state of GraphicVisual object.
        /// </summary>
        public virtual void UpdateVisualStroke() => UpdateVisual();


        /// <summary>
        /// 旋转操作。
        /// Rotation transform operation.
        /// </summary>
        /// <param name="angle">
        /// 旋转角度，单位度。
        /// Rotation angle which is in degrees.
        /// </param>
        /// <param name="center">
        /// 旋转中心点。
        /// Rotation transform center point.
        /// </param>
        /// <param name="IsLocalCenter">
        /// 旋转中心点位置是否为局部坐标，默认值为 false。
        /// Whether the rotation center point is in local coordinate, and default value is false.
        /// </param>
        public virtual void Rotate(double angle, Point center, bool IsLocalCenter = false)
        {
            Point cpot = IsLocalCenter ? Transform.Transform(center) : center;
            Transform trans = new RotateTransform(angle, cpot.X, cpot.Y);
            Origin = trans.Transform(Origin);
            Angle += angle;
        }
        
        /// <summary>
        /// 比例缩放操作。
        /// Scale transform operation.
        /// </summary>
        /// <param name="scaleX">
        /// X 轴的缩放比例。
        /// The x-axis scale transform factor.
        /// </param>
        /// <param name="scaleY">
        /// Y 轴的缩放比例。
        /// The y-axis scale transform factor.
        /// </param>
        /// <param name="centerX">
        /// 缩放中心点的 X 坐标。
        /// The x-coordinate of scale transform center point.
        /// </param>
        /// <param name="centerY">
        /// 缩放中心点的 Y 坐标。
        /// The y-coordinate of scale transform center point.
        /// </param>
        /// <param name="IsLocalCenter">
        /// 缩放中心点位置是否为局部坐标，默认值为 false。
        /// Whether the scale center point is in local coordinate, and default value is false..
        /// </param>
        public virtual void Scale(double rx, double ry, double cx, double cy, bool IsLocalCenter = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 平移操作。
        /// Translation transform operation.
        /// </summary>
        /// <param name="dx">
        /// 沿 X 轴方向的移动距离。
        /// The distance to translate along the x-axis.
        /// </param>
        /// <param name="dy">
        /// 沿 Y 轴方向的移动距离。
        /// The distance to translate along the y-axis.
        /// </param>
        public virtual void Translate(double dx, double dy)
        {
            Origin += new Vector(dx, dy);
        }

        #endregion

    }
}
