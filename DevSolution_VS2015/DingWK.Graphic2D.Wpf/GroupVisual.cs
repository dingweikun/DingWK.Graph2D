using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingWK.Graphic2D.Wpf
{
    public sealed class GroupVisual : GraphicVisual, IList<GraphicVisual>, INotifyCollectionChanged
    {
        private readonly List<GraphicVisual> _children;

        public GroupVisual()
        {


        }


        #region IList<GraphicVisual> implementation

        public GraphicVisual this[int index]
        {
            get { return _children[index]; }
            set { _children[index] = value; }
        }

        public int Count
        {
            get
            {
                return _children.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<GraphicVisual>)_children).IsReadOnly;
            }
        }

        public void Add(GraphicVisual item)
        {
            _children.Add(item);
        }

        public void Clear()
        {
            _children.Clear();
        }

        public bool Contains(GraphicVisual item)
        {
            return _children.Contains(item);
        }

        public void CopyTo(GraphicVisual[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GraphicVisual> GetEnumerator()
        {
            return ((IList<GraphicVisual>)_children).GetEnumerator();
        }

        public int IndexOf(GraphicVisual item)
        {
            return _children.IndexOf(item);
        }

        public void Insert(int index, GraphicVisual item)
        {
            _children.Insert(index, item);
        }

        public bool Remove(GraphicVisual item)
        {
            return _children.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _children.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<GraphicVisual>)_children).GetEnumerator();
        }

        #endregion


        #region INotifyCollectionChanged implementation

        public event NotifyCollectionChangedEventHandler GroupVisualChildrenChanged;

        // A general method to raise the CollectionChanged event.
        private void OnGroupVisualChildrenChanged(NotifyCollectionChangedEventArgs args)
        {
            GroupVisualChildrenChanged?.Invoke(this, args);
        }

        #endregion

    }
}
