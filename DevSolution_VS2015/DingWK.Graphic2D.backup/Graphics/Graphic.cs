using System;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Graphic2D.Graphics
{


    public interface IGraphic
    {
        Guid ID { get; }

        Point Origin { get; set; }

        double Angle { get; set; }

        Brush Fill { get; set; }

        Pen Stroke { get; set; }

    }




    public abstract class Graphic : IGraphic
    {

        #region fields & properties

        private double _angle;

        public double Angle
        {
            get { return _angle; }
            set { _angle = value % 360; }
        }

        public Brush Fill { get; set; }

        public Guid ID { get; private set; }

        public Point Origin { get; set; }

        public Pen Stroke { get; set; }

        #endregion


        protected Graphic()
        {
            ID = Guid.NewGuid();
        }

    }
}
