using Graphic2D.Kernel.Graphic;
using Graphic2D.Kernel.Graphic.Geom;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.GraphicVisuals
{
    /// <summary>
    /// 内部封装了图形对象的可视化元素，该元素提供了图形对象在 WPF 中的显示支持和变换操作。
    /// The visual element which wraps the graphic object inside. 
    /// The visual element provides rendering support in WPF and transform operations.
    /// </summary>
    public class GraphicVisual : DrawingVisual, IGraphicVisual
    {
        #region Internal field

        // 要显示的内部图形对象。The inside graphic object to be rendering .
        protected Graphic<IGeom> _graphic;

        #endregion


        #region Constructors

        /// <summary>
        /// 内部构造函数，只用于派生类 GraphicVisualGroup 初始化时使用。
        /// A protected constructor which is only used by the derived class GraphicVisualGroup for initialization.
        /// </summary>
        protected GraphicVisual()
        {
            _graphic = new Graphic<IGeom>(new EmptyGeom());
            UpdateTransform();
        }

        /// <summary>
        /// 初始化一个显示对象实例。Initializes a GraphicVisual instance.
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
        /// 初始化一个显示对象的副本实例。Initializes a new copy instance of specific graphic object.
        /// </summary>
        public GraphicVisual(GraphicVisual graphicVisual)
            : this(graphicVisual._graphic)
        {
        }

        #endregion


        #region Method

        /// <summary>
        /// 更新可视化元素的显示。Updates visual for rendering. 
        /// </summary>
        public void UpdateVisual()
        {
            if (_graphic != null)
            {
                DrawingContext dc = RenderOpen();
                _graphic.Geom.DrawGeom(dc, Fill, Stroke);
                dc.Close();
            }
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
        /// 更新对象显示变换
        /// </summary>
        public virtual void UpdateTransform()
        {
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new RotateTransform(Angle));
            tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
            Transform = tr;
        }

        public virtual void UpdateVisualFill() => UpdateVisual();

        public virtual void UpdateVisualStroke() => UpdateVisual();

        public virtual void Rotate(double angle, Point center, bool IsLocalCenter = false)
        {
            Point cpot = IsLocalCenter ? Transform.Transform(center) : center;
            Transform trans = new RotateTransform(angle, cpot.X, cpot.Y);
            Origin = trans.Transform(Origin);
            Angle += angle;
            UpdateTransform();
        }

        public virtual void Move(double dx, double dy)
        {
            Origin += new Vector(dx, dy);
            UpdateTransform();
        }

        public virtual void Scale(double rx, double ry, double cx, double cy)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
