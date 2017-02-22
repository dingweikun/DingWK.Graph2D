using Graphic2D.Kernel.Visuals;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Graphic2D.Kernel.Model
{
    public sealed partial class VisualSelection : IList<GraphicVisual>
    {
        #region Fields & Properties

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

        private Rect _refRect;

        public Rect RefRect
        {
            get { return _refRect; }
            private set { _refRect = value; }
        }

        public DrawingGroup SelectionDrawing { get; private set; }

        #endregion


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


        #region Private Methods 

        private void SetRefRect()
        {
            var drawing = GetSelectionDrawing();
            drawing.Transform = new RotateTransform(-RefAngle);
            RefRect = drawing.Bounds;
            RefRect = new Rect(RefRect.X + 0.5, RefRect.Y + 0.5, RefRect.Width - 1, RefRect.Height - 1);
        }

        private void UpdateSelection()
        {
            // RefAngle = Count == 0 ? 0 : (Count == 1 ? _visuals[0].Angle : RefAngle);

            switch (Count)
            {
                case 0:
                    RefAngle = 0;
                    //RefRect = Rect.Empty;
                    break;
                case 1:
                    RefAngle = _visuals[0].Angle;
                    break;
                default:
                    RefAngle = RefAngle;
                    //RefRect = RefRect;
                    break;
            }
            SelectionDrawing = GetSelectionDrawing();

        }

        private DrawingGroup GetSelectionDrawing()
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            foreach (GraphicVisual gv in _visuals)
            {
                var drawing = GraphicVisualHelper.GetGraphicVisualDrawing(gv);
                drawing.Transform = gv.Transform;
                drawingGroup.Children.Add(drawing);
            }
            return drawingGroup;
        }

        #endregion


        #region Seleciton Methods

        public void AddIntoSelection(GraphicVisual gv)
        {
            if (gv != null && !_visuals.Contains(gv))
            {
                _visuals.Add(gv);
            }

            UpdateSelection();
            SetRefRect();
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
            SetRefRect();
        }

        public void RemoveFromSelection(GraphicVisual gv)
        {
            _visuals.Remove(gv);
            UpdateSelection();
            SetRefRect();

        }

        public void ClearSelection()
        {
            _visuals.Clear();
            UpdateSelection();
            SetRefRect();
        }

        #endregion


    }
}
