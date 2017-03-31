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

        private IPlaceInfo InGroupTransform(IPlaceInfo info)
        {
            return new PlaceInfo()
            {
                Origin = this.Transform.Transform(info.Origin),
                Angle = info.Angle - this.PlaceInfo.Angle
            };
        }

        private IPlaceInfo OutGroupTransform(IPlaceInfo info)
        {
            return new PlaceInfo()
            {
                Origin = this.Transform.Inverse.Transform(info.Origin),
                Angle = info.Angle + this.PlaceInfo.Angle
            };
        }

        public void AddIntoGroup(GraphicVisual gv)
        {
            ((ICollection<GraphicVisual>)this).Add(gv);
            gv.PlaceInfo = InGroupTransform(gv.PlaceInfo);
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
            gv.PlaceInfo = OutGroupTransform(gv.PlaceInfo);
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
