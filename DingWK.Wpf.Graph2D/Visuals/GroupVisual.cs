using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Media;

namespace DingWK.Wpf.Graph2D.Visuals
{
    public sealed class GroupVisual : GraphicVisual, ICollection<GraphicVisual>, INotifyCollectionChanged
    {

        #region constructors

        public GroupVisual(ICollection<GraphicVisual> visuals, VisualInfo info)
           : base(info)
        {
            foreach (GraphicVisual gv in visuals)
            {
                this.AddIntoGroup(gv);
            }


            UpdateVisualInfo();
        }

        public GroupVisual(ICollection<GraphicVisual> visuals)
            : this(visuals, VisualInfo.Default)
        {
        }

        #endregion



        #region Indexer

        public GraphicVisual this[int index]
        {
            get
            {
                return Children[index] as GraphicVisual;
            }
            set
            {
                Children[index] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Children[index]));
            }
        }

        #endregion



        #region methods

        private Rect BuildResizeRect()
        {
            Rect rect = new Rect();

            return rect;
        }


        #region override methodes

        /// <summary>
        /// Update all children's fill brushes.
        /// </summary>
        internal override void UpdateFill()
        {
            foreach (GraphicVisual gv in Children)
            {
                gv.Fill = this.Fill;
            }
        }

        /// <summary>
        /// Update all children's strokes.
        /// </summary>
        internal override void UpdateStroke()
        {
            foreach (GraphicVisual gv in Children)
            {
                gv.Stroke = this.Stroke;
            }
        }

        /// <summary>
        /// Update the transform of group instance，and update children's strokes and brushes.
        /// </summary>
        internal override void UpdateVisualInfo()
        {
            // update group instance transform

            UpdateTransform();

            // update children's strokes and brushes

            foreach (GraphicVisual gv in Children)
            {
                VisualInfo info = gv.VisualInfo;
                info.Fill = this.Fill;
                info.Stroke = this.Stroke;
                gv.VisualInfo = info;
            }
        }

        #endregion


        #region grouping methods

        private void GroupTransIn(GraphicVisual graphic)
        {
            graphic.Angle = graphic.Angle - this.Angle;
            graphic.Origin = this.Transform.Inverse.Transform(graphic.Origin);
        }

        private void GroupTransOut(GraphicVisual graphic)
        {
            graphic.Origin = this.Transform.Transform(graphic.Origin);
            graphic.Angle = graphic.Angle + this.Angle;
        }

        public void AddIntoGroup(GraphicVisual graphic)
        {
            GroupTransIn(graphic);
            ((ICollection<GraphicVisual>)this).Add(graphic);
        }

        public GraphicVisual RemoveFromGroup(GraphicVisual graphic)
        {
            if (Children.Contains(graphic as Visual))
            {
                GroupTransOut(graphic);
                ((ICollection<GraphicVisual>)this).Remove(graphic);
            }
            return graphic;
        }

        public GraphicVisual[] ClearGroup()
        {
            GraphicVisual[] graphics = new GraphicVisual[Count];

            for (int i = 0; i < Count; i++)
            {
                graphics[i] = this[i];
                GroupTransOut(graphics[i]);
            }

            ((ICollection<GraphicVisual>)this).Clear();

            return graphics;
        }

        #endregion


        /*/
        #region children position moving methods 

        public void MoveForward(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(index - 1, graphic);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveBackward(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(index + 1, graphic);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveToFirst(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(0, graphic);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveToLast(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Add(graphic);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        #endregion
        //*/

        #endregion



        #region ICollection<GraphicVisual> interface members

        public int Count => Children.Count;

        void ICollection<GraphicVisual>.Add(GraphicVisual item)
        {
            Children.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        void ICollection<GraphicVisual>.Clear()
        {
            Children.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        bool ICollection<GraphicVisual>.Remove(GraphicVisual item)
        {
            var visual = item as Visual;
            if (!Children.Contains(visual))
                return false;
            Children.Remove(visual);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            return true;
        }

        void ICollection<GraphicVisual>.CopyTo(GraphicVisual[] array, int arrayIndex)
            => Children.CopyTo(array, arrayIndex);

        bool ICollection<GraphicVisual>.Contains(GraphicVisual item)
            => Children.Contains(item);

        bool ICollection<GraphicVisual>.IsReadOnly => Children.IsReadOnly;

        public IEnumerator<GraphicVisual> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// GroupVisual children iterator
        /// </summary>
        public struct Enumerator : IEnumerator<GraphicVisual>
        {
            VisualCollection visuals;
            int position;

            public Enumerator(GroupVisual graphicGroup)
            {
                visuals = graphicGroup.Children;
                position = -1;
            }

            public GraphicVisual Current => visuals[position] as GraphicVisual;

            object IEnumerator.Current => visuals[position];

            public void Dispose() { }

            public bool MoveNext()
            {
                if (position < visuals.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset() => position = -1;
        }

        #endregion



        #region INotifyCollectionChanged implementation

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        #endregion


    }
}
