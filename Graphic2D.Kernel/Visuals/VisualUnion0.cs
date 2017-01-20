using Graphic2D.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public class VisualUnion : PropertyChangeNotify, IVisualInfo
    {

        #region Internal fields

        private VisualInfo _info;
        private ReadOnlyCollection<GraphicVisual> _visualCollection;

        #endregion


        #region Properties

        public ReadOnlyCollection<GraphicVisual> VisualCollection
        {
            get { return _visualCollection; }
            set
            {
                _visualCollection = value;
                NotifyPropertyChanged(nameof(VisualCollection));
            }
        }

        // IVisualInfo interfec members

        public double Angle
        {
            get { return _info.Angle; }
            set
            {
                SetAngle(value);
                NotifyPropertyChanged(nameof(Angle));
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

        Brush IVisualInfo.Fill => _info.Fill;

        Pen IVisualInfo.Stroke => _info.Stroke;

        Point IVisualInfo.Center => _info.Center;

        #endregion


        #region Constructors
        
        public VisualUnion(IList<GraphicVisual> visuals)
        {
            SetVisualCollection(visuals);
        }

        #endregion


        #region Internal methods

        internal void SetVisualCollection(IList<GraphicVisual> visuals)
        {
            if (visuals == null)
                throw new ArgumentNullException();

            _visualCollection = new ReadOnlyCollection<GraphicVisual>(visuals);
            _info = new VisualInfo() { Fill = null, Stroke = null };
            if(_visualCollection.Count == 1)
            {

            }

            switch (_visualCollection.Count)
            {
                case 0:
                    _info.Angle = 0;
                    _info.Origin = new Point(0, 0);
                    break;
                case 1:
                    _info.Angle = _visualCollection[1].Angle;
                    _info.Origin = _visualCollection[1].Origin;
                    break;
                default:

            }

        }

        internal void SetOrigin(Point origin)
        {

        }

        internal void SetAngle(double angle)
        {

        }
        #endregion


    }
}
