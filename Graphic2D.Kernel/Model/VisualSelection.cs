using Graphic2D.Kernel.Visuals;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Model
{
    public sealed class VisualSelection : IList<GraphicVisual>
    {
        private readonly List<GraphicVisual> _visuals;

        private double _refAngle;

        public double RefAngle
        {
            get
            {
                return _refAngle;
            }
            private set
            {
                _refAngle = value % 360;
            }
        }

        public DrawingGroup SelectionDrawing { get; private set; }

        public Rect RegionRect
        {
            get
            {
                Transform tr = SelectionDrawing.Transform;
                SelectionDrawing.Transform = Transform.Identity;
                Rect region = SelectionDrawing.Bounds;
                SelectionDrawing.Transform = tr;
                return region;
            }
        }

        #region Constructor

        public VisualSelection()
        {
            _visuals = new List<GraphicVisual>();
            UpdateSelection();
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

        public void AddIntoSelection(GraphicVisual gv)
        {
            if (gv != null && !_visuals.Contains(gv))
            {
                _visuals.Add(gv);
            }

            UpdateSelection();
        }

        public void AddIntoSelection(List<GraphicVisual> gvs)
        {
            foreach (GraphicVisual gv in gvs)
            {
                if (gv != null && !_visuals.Contains(gv))
                {
                    _visuals.Add(gv);
                }
            }
            UpdateSelection();
        }

        public void RemoveFromSelection(GraphicVisual gv)
        {
            _visuals.Remove(gv);
            UpdateSelection();
        }

        public void ClearSelection()
        {
            _visuals.Clear();
            UpdateSelection();
        }

        public DrawingGroup GetSelectionDrawing()
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            foreach (GraphicVisual gv in _visuals)
            {
                var drawing = VisualHelper.GetGraphicVisualDrawing(gv);
                drawing.Transform = gv.Transform;
                drawingGroup.Children.Add(drawing);
            }
            drawingGroup.Transform = new RotateTransform(-RefAngle);
            return drawingGroup;
        }

        public void UpdateSelection()
        {
            RefAngle = Count == 0 ? 0 : (Count == 1 ? _visuals[0].Angle : RefAngle);
            SelectionDrawing = GetSelectionDrawing();
        }

        #endregion
    }
}
