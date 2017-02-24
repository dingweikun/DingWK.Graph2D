using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphics
{
    public abstract class Graphic : IGraphic
    {
        #region feilds & properties

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
            set { _fill = value != null ? value.CloneCurrentValue() : null; }
        }


        Pen _stroke;
        public Pen Stroke
        {
            get { return _stroke; }
            set { _stroke = value != null ? value.CloneCurrentValue() : null; }
        }

        public Point Origin { get; set; }

        #endregion


    }
}
