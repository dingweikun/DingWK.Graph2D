using Graphic2D.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public class UnionVisual : PropertyChangeNotify,IVisualInfo
    { 
        private readonly List<GraphicVisual> _visuals;
        private VisualInfo _info;

        public Brush Fill
        {
            get { return _info.Fill; }
            set
            {
                SetFill(value);
                NotifyPropertyChanged(nameof(Fill));
            }
        }

        public Pen Stroke
        {
            get { return _info.Stroke; }
            set
            {
                SetStroke(value);
                NotifyPropertyChanged(nameof(Stroke));
            }
        }

        public Point Origin
        {
            get { return _info.Origin; }
            set
            {
                SetOrigin(value);
                NotifyPropertyChanged(nameof(Origin));
            }
        }

        public Point Center
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public double Angle
        {
            get { return _info.Angle; }
            set
            {
                SetAngle(value);
                NotifyPropertyChanged(nameof(Angle));
            }
        }


        internal UnionVisual()
        {
            _visuals = new List<GraphicVisual>();
            _info = VisualInfo.Default;
        }


        internal void SetFill(Brush brush)
        {
            _info.Fill = brush;
            foreach(GraphicVisual gv in _visuals)
            {
                gv.Fill = _info.Fill;
            }
        }

        internal void SetStroke(Pen pen)
        {
            _info.Stroke = pen;
            foreach (GraphicVisual gv in _visuals)
            {
                gv.Stroke = _info.Stroke;
            }
        }

        internal void SetOrigin(Point origin)
        {
            Vector delta = origin - _info.Origin;
            foreach(GraphicVisual gv in _visuals)
            {
                gv.Origin += delta;
            }
            _info.Origin = origin;
        }

        internal void SetAngle(double value)
        {
           _
        }



    }
}
