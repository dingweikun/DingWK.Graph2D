using Graphic2D.Kernel.Common;
using Graphic2D.Kernel.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Collections;

namespace Graphic2D.Kernel
{
    public class VisualTransformer : PropertyChangeNotify, IList<GraphicVisual>
    {
        private double _angle;
        private readonly List<GraphicVisual> _visuals;

        public double Angle
        {
            get { return _angle; }
            private set
            {
                _angle = value % 360;
                NotifyPropertyChanged(nameof(Angle));
            }
        }


        public VisualTransformer()
        {
            _angle = 0;
            _visuals = new List<GraphicVisual>();
        }
        

        public void Select(IList<GraphicVisual> visuals)
        {
            if (visuals != null)
            {
                foreach (GraphicVisual gv in visuals)
                {
                    if (_visuals.Contains(gv) || gv == null) continue;
                    _visuals.Add(gv);
                }
                UpdateAngle();
            }
        }

        public void UnSelect(IList<GraphicVisual> visuals)
        {
            if (visuals != null)
            {
                foreach (GraphicVisual gv in visuals)
                {
                    _visuals.Remove(gv);
                }
                UpdateAngle();
            }
        }

        public void ClearSelect()
        {
            _visuals.Clear();
            Angle = 0;
        }

       



        private void UpdateAngle()
        {
            if (Count < 2)
            {
                Angle = Count != 0 ? _visuals[0].Angle : 0;
            }
        }


        #region IList<GraphicVisual> members

        public int Count => _visuals.Count;

        GraphicVisual IList<GraphicVisual>.this[int index]
        {
            get { return _visuals[index]; }
            set { _visuals[index] = value; }
        }

        bool ICollection<GraphicVisual>.IsReadOnly => (_visuals as ICollection<GraphicVisual>).IsReadOnly;

        void ICollection<GraphicVisual>.Add(GraphicVisual item) => _visuals.Add(item);

        void ICollection<GraphicVisual>.Clear() => _visuals.Clear();

        bool ICollection<GraphicVisual>.Contains(GraphicVisual item) => _visuals.Contains(item);

        public void CopyTo(GraphicVisual[] array, int arrayIndex) => _visuals.CopyTo(array, arrayIndex);

        int IList<GraphicVisual>.IndexOf(GraphicVisual item) => _visuals.IndexOf(item);

        void IList<GraphicVisual>.Insert(int index, GraphicVisual item) => _visuals.Insert(index, item);

        bool ICollection<GraphicVisual>.Remove(GraphicVisual item) => _visuals.Remove(item);

        void IList<GraphicVisual>.RemoveAt(int index) => _visuals.RemoveAt(index);

        public IEnumerator<GraphicVisual> GetEnumerator() => _visuals.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _visuals.GetEnumerator();

        #endregion

    }
}
