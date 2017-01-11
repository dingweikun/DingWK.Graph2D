using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Media;

namespace Graphic2D.Kernel.Visuals
{
    public class GroupVisual : GraphicVisual, ICollection<GraphicVisual>, INotifyCollectionChanged
    {

        #region Constructors

        public GroupVisual(VisualInfo graphicInfo)
            : base(graphicInfo)
        {
            UpdateGraphicInfo();
        }

        public GroupVisual()
            : this(VisualInfo.Default)
        {
        }

        #endregion


        #region Indexer

        /// <summary>
        /// 图形组内部子图的索引器。
        /// 
        /// </summary>
        public GraphicVisual this[int index]
        {
            get
            {
                return Children[index] as GraphicVisual;
            }
            internal set
            {
                Children[index] = value;
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Children[index]));
            }
        }

        #endregion


        #region Methods


        #region Override methodes

        /// <summary>
        /// 更新所有子图形填充样式
        /// </summary>
        internal override void UpdateFill()
        {
            foreach (GraphicVisual gv in Children)
            {
                gv.Fill = this.Fill;
            }
        }

        /// <summary>
        /// 更新所有子图形轮廓样式
        /// </summary>
        internal override void UpdateStroke()
        {
            foreach (GraphicVisual gv in Children)
            {
                gv.Stroke = this.Stroke;
            }
        }

        /// <summary>
        /// 更新图形组的几何变换状态，更新所有子图的轮廓和填充样式。
        /// </summary>
        internal override void UpdateGraphicInfo()
        {
            // 更新图形组的几何变换状态

            UpdateTransform();

            // 更新所有子图的轮廓和填充样式

            foreach (GraphicVisual gv in Children)
            {
                VisualInfo info = gv.GraphicInfo;
                info.Fill = this.Fill;
                info.Stroke = this.Stroke;
                gv.GraphicInfo = info;
            }
        }

        #endregion


        #region Grouping methods

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
                
        public void GroupIn(GraphicVisual graphic)
        {
            GroupTransIn(graphic);
            ((ICollection<GraphicVisual>)this).Add(graphic);
        }

        public GraphicVisual UnGroup(GraphicVisual graphic)
        {
            if (Children.Contains(graphic as Visual))
            {
                GroupTransOut(graphic);
                ((ICollection<GraphicVisual>)this).Remove(graphic);
            }
            return graphic;
        }

        public GraphicVisual[] UnGroupAll()
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


        #region Children position moving methods 

        public void MoveForward(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(index - 1, graphic);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveBack(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(index + 1, graphic);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveToFirst(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(0, graphic);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveToLast(GraphicVisual graphic)
        {
            int index = Children.IndexOf(graphic);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Add(graphic);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        #endregion


        #endregion


        #region ICollection<GraphicVisual> interface members

        public int Count => Children.Count;

        void ICollection<GraphicVisual>.Add(GraphicVisual item)
        {
            Children.Add(item);
            GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        void ICollection<GraphicVisual>.Clear()
        {
            Children.Clear();
            GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        bool ICollection<GraphicVisual>.Remove(GraphicVisual item)
        {
            var visual = item as Visual;
            if (!Children.Contains(visual))
                return false;
            Children.Remove(visual);
            GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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
        /// GroupVisual 迭代器
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


        #region INotifyCollectionChanged interface implementation

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        // 添加一个触发 CollectionChanged 事件的通用方法
        // A general method to raise the CollectionChanged event.
        private void GraphicVisualGroupChildrenChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        #endregion

    }
}
