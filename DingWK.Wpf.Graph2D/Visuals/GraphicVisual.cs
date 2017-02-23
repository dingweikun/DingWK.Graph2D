using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Visuals
{
    public abstract class GraphicVisual : DrawingVisual, IVisualInfo, INotifyPropertyChanged
    {

        #region field & properties

        private VisualInfo _visualInfo;

        public VisualInfo VisualInfo => _visualInfo;
        
        public double Angle
        {
            get { return _visualInfo.Angle; }
            set
            {
                _visualInfo.Angle = value;
                UpdateTransform();
                OnPropertyChanged(nameof(Angle));
            }
        }

        public Brush Fill
        {
            get { return _visualInfo.Fill; }
            set
            {
                _visualInfo.Fill = value;
                UpdateFill();
                OnPropertyChanged(nameof(Fill));
            }
        }

        public Point Origin
        {
            get { return _visualInfo.Origin; }
            set
            {
                _visualInfo.Origin = value;
                UpdateTransform();
                OnPropertyChanged(nameof(Origin));
            }
        }

        public Pen Stroke
        {
            get { return _visualInfo.Stroke; }
            set
            {
                _visualInfo.Stroke = value;
                UpdateStroke();
                OnPropertyChanged(nameof(Stroke));
            }
        }

        #endregion


        #region constructor

        protected GraphicVisual(VisualInfo visualInfo)
        {
            _visualInfo = visualInfo;
        }

        #endregion


        #region methods

        internal void UpdateTransform()
        {
            TransformGroup tr = new TransformGroup();
            tr.Children.Add(new RotateTransform(Angle));
            tr.Children.Add(new TranslateTransform(Origin.X, Origin.Y));
            Transform = tr;
        }

        internal abstract void UpdateFill();
        internal abstract void UpdateStroke();
        internal abstract void UpdateVisualInfo();

        #endregion


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
