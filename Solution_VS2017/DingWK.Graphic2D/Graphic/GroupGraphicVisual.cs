using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DingWK.Graphic2D.Graphic
{
    public sealed class GroupGraphicVisual : GraphicVisual, ICollection<GraphicVisual>, INotifyCollectionChanged
    {


        public GraphicVisual this[int index] => Children[index] as GraphicVisual;



        #region Group operation methods

        private IPlacement InGroupTransform(IPlacement place)
        {
            return new Placement()
            {
                Origin = this.Transform.Transform(place.Origin),
                Angle = place.Angle - this.Placement.Angle
            };
        }

        private IPlacement OutGroupTransform(IPlacement place)
        {
            return new Placement()
            {
                Origin = this.Transform.Inverse.Transform(place.Origin),
                Angle = place.Angle + this.Placement.Angle
            };
        }

        public void AddIntoGroup(GraphicVisual gv)
        {
            ((ICollection<GraphicVisual>)this).Add(gv);
            gv.Placement = InGroupTransform(gv.Placement);
        }

        public void AddIntoGroup(ICollection<GraphicVisual> gvCollection)
        {
            foreach (GraphicVisual gv in gvCollection)
                AddIntoGroup(gv);
        }

        public GraphicVisual RemoveFromGroup(GraphicVisual gv)
        {
            if (this.Count <= 1)
                return null;
            gv.Placement = OutGroupTransform(gv.Placement);
            ((ICollection<GraphicVisual>)this).Remove(gv);
            return gv;
        }

        #endregion



        #region Overrride methods

        protected override void UpdateVisualFill()
        {
            foreach (GraphicVisual gv in Children)
            {
                gv.Fill = this.Fill;
            }
        }

        protected override void UpdateVisualStroke()
        {
            foreach (GraphicVisual gv in Children)
            {
                gv.Stroke = this.Stroke;
            }
        }

        #endregion



        #region ICollection<GraphicVisual> implementation

        public int Count => Children.Count;

        public bool Contains(GraphicVisual item) => Children.Contains(item);

        public IEnumerator<GraphicVisual> GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator() as IEnumerator<GraphicVisual>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        void ICollection<GraphicVisual>.CopyTo(GraphicVisual[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);

        bool ICollection<GraphicVisual>.IsReadOnly => Children.IsReadOnly;

        void ICollection<GraphicVisual>.Add(GraphicVisual item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Children.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
        }

        void ICollection<GraphicVisual>.Clear()
        {
            Children.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        bool ICollection<GraphicVisual>.Remove(GraphicVisual item)
        {
            Children.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
            return true;
        }

        #endregion



        #region INotifyCollectionChanged implementation

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        #endregion

    }
}
