using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Visuals
{
    public class VisualInfo : IVisualInfo
    {

        #region feilds & properties

        public static readonly VisualInfo Default = 
            new VisualInfo(Brushes.White, new Pen(Brushes.Black, 1));

        double _angle;
        public double Angle
        {
            get { return _angle; }
            set { _angle = value % 360; }
        }

        Brush _fill;
        public Brush Fill
        {
            get { return _fill; }
            set { _fill = value?.CloneCurrentValue(); }
        }


        Pen _stroke;
        public Pen Stroke
        {
            get { return _stroke; }
            set { _stroke = value?.CloneCurrentValue(); }
        }

        public Point Origin { get; set; }

        #endregion


        #region constructors

        public VisualInfo()
        {
        }

        public VisualInfo(Brush fill, Pen stroke)
        {
            Fill = fill;
            Stroke = stroke;
        }

        public VisualInfo(IVisualInfo info)
        {
            Fill = info.Fill;
            Stroke = info.Stroke;
            Origin = info.Origin;
            Angle = info.Angle;
        }

        #endregion

    }
}
