using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    /// <summary>
    /// 可视化的图形, 图形组, 文字的基类
    /// </summary>
    public abstract class GraphicVisual : DrawingVisual, IVisualInfo, INotifyPropertyChanged
    {

        #region Internal field

        private VisualInfo _graphicInfo;

        #endregion


        #region Properties

        public VisualInfo GraphicInfo
        {
            get { return _graphicInfo; }
            set
            {
                _graphicInfo = value;
                UpdateGraphicInfo();
                NotifyPropertyChanged(nameof(GraphicInfo));
            }
        }

        public double Angle
        {
            get { return _graphicInfo.Angle; }
            set
            {
                _graphicInfo.Angle = value;
                UpdateTransform();
                NotifyPropertyChanged(nameof(Angle));
            }
        }

        public Brush Fill
        {
            get { return _graphicInfo.Fill; }
            set
            {
                _graphicInfo.Fill = value;
                UpdateFill();
                NotifyPropertyChanged(nameof(Fill));
            }
        }

        public Point Origin
        {
            get { return _graphicInfo.Origin; }
            set
            {
                _graphicInfo.Origin = value;
                UpdateTransform();
                NotifyPropertyChanged(nameof(Origin));
            }
        }

        public Pen Stroke
        {
            get { return _graphicInfo.Stroke; }
            set
            {
                _graphicInfo.Stroke = value;
                UpdateStroke();
                NotifyPropertyChanged(nameof(Stroke));
            }
        }

        #endregion


        #region Constructor

        public GraphicVisual(VisualInfo graphicInfo)
        {
            _graphicInfo = graphicInfo;
        }

        #endregion


        #region Methods

        internal void UpdateTransform()
        {
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new RotateTransform(Angle));
            tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
            Transform = tr;
        }

        //public void Translate(double dx, double dy)
        //{
        //    Vector delt = new Vector(dx, dy);
        //    Origin += delt;
        //}

        //public void Rotate(double angle, double cx, double cy)
        //{
        //    Transform trans = new RotateTransform(angle, cx, cy);
        //    Origin = trans.Transform(Origin);
        //    Angle += angle;
        //}

        // abstract methods

        internal abstract void UpdateFill();
        internal abstract void UpdateStroke();
        internal abstract void UpdateGraphicInfo();

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

    }
}
