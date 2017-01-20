using Graphic2D.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public class VisualUnion : PropertyChangeNotify, IList<GraphicVisual>
    {

        private double _refAngle;

        private readonly List<GraphicVisual> _visuals;

        public double RefAngle
        {
            get { return _refAngle; }
            private set
            {
                _refAngle = value % 360;
                NotifyPropertyChanged(nameof(RefAngle));
            }
        }


        #region Constructor

        public VisualUnion()
        {
            _visuals = new List<GraphicVisual>();
            _refAngle = 0;
        }

        #endregion


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

        void ICollection<GraphicVisual>.CopyTo(GraphicVisual[] array, int arrayIndex) => _visuals.CopyTo(array, arrayIndex);

        int IList<GraphicVisual>.IndexOf(GraphicVisual item) => _visuals.IndexOf(item);

        void IList<GraphicVisual>.Insert(int index, GraphicVisual item) => _visuals.Insert(index, item);

        bool ICollection<GraphicVisual>.Remove(GraphicVisual item) => _visuals.Remove(item);

        void IList<GraphicVisual>.RemoveAt(int index) => _visuals.RemoveAt(index);

        public IEnumerator<GraphicVisual> GetEnumerator() => _visuals.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion


        #region Methods

        public Rect GetBoundsRect()
        {
            if (Count == 0)
            {
                return Rect.Empty;
            }
            else if (Count == 1)
            {
                return _visuals[0].Drawing.Bounds;
            }
            else
            {
                DrawingGroup drawingGroup = new DrawingGroup();
                foreach(GraphicVisual gv in _visuals)
                {
                    var drawing = gv.Drawing;
                    drawing.Transform = gv.Transform;
                    drawingGroup.Children.Add(drawing);
                }

                drawingGroup.Transform = new RotateTransform(RefAngle);
                Rect bound = drawingGroup.Bounds;

                Point loc = drawingGroup.Transform.Inverse.Transform(bound.Location);

                return new Rect();
            }
        }

        #endregion
    }
}
