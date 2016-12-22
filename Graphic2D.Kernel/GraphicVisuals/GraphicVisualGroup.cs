using Graphic2D.Kernel.Geom;
using Graphic2D.Kernel.Graphic;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Media;

namespace Graphic2D.Kernel.GraphicVisuals
{
    public class GraphicVisualGroup : GraphicVisual, ICollection<IGraphicVisual>, INotifyCollectionChanged
    {

        #region Constructor

        /// <summary>
        /// 初始化一个图形组的实例。
        /// Initializes a GraphicVisualGroup instance.
        /// </summary>
        public GraphicVisualGroup()
            : base(new Graphic<IGeom>(new EmptyGeom()))
        {
        }

        #endregion


        #region Indexer

        /// <summary>
        /// 图形组内部子图的索引器。
        /// 
        /// </summary>
        public IGraphicVisual this[int index]
        {
            get
            {
                return (IGraphicVisual)Children[index];
            }
            internal set
            {
                Children[index] = (Visual)value;
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, Children[index]));
            }
        }

        #endregion


        #region Override members

        /// <summary>
        /// 更新所有子图形填充样式
        /// </summary>
        public override void UpdateVisualFill()
        {
            foreach (IGraphicVisual gv in Children)
            {
                gv.Fill = this.Fill;
            }
        }

        /// <summary>
        /// 更新所有子图形轮廓样式
        /// </summary>
        public override void UpdateVisualStroke()
        {
            foreach (IGraphicVisual gv in Children)
            {
                gv.Stroke = this.Stroke;
            }
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


        #region ICollection<IGraphicVisual> interface members

        public int Count => Children.Count;

        void ICollection<IGraphicVisual>.Add(IGraphicVisual item)
        {
            Children.Add((Visual)item);
            GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        void ICollection<IGraphicVisual>.Clear()
        {
            Children.Clear();
            GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        bool ICollection<IGraphicVisual>.Remove(IGraphicVisual item)
        {
            var visual = item as Visual;
            if (!Children.Contains(visual))
                return false;
            Children.Remove(visual);
            GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            return true;
        }

        void ICollection<IGraphicVisual>.CopyTo(IGraphicVisual[] array, int arrayIndex)
            => Children.CopyTo(array, arrayIndex);

        bool ICollection<IGraphicVisual>.Contains(IGraphicVisual item)
            => Children.Contains((Visual)item);

        bool ICollection<IGraphicVisual>.IsReadOnly => Children.IsReadOnly;

        public IEnumerator<IGraphicVisual> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// GraphicVisualGroup 迭代器
        /// </summary>
        public struct Enumerator : IEnumerator<IGraphicVisual>
        {
            VisualCollection visuals;
            int position;

            public Enumerator(GraphicVisualGroup graphicVisualGroup)
            {
                visuals = graphicVisualGroup.Children;
                position = -1;
            }

            public IGraphicVisual Current => (IGraphicVisual)visuals[position];

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


        #region Methods

        public void GroupIn(GraphicVisual visual)
        {
            ((ICollection<IGraphicVisual>)this).Add(GroupTransIn(visual));
        }


        public GraphicVisual UnGroup(GraphicVisual visual)
        {
            if (Children.Contains(visual as Visual))
            {
                ((ICollection<IGraphicVisual>)this).Remove(GroupTransOut(visual));
            }
            return visual;
        }

        public GraphicVisual[] UnGroupAll()
        {
            GraphicVisual[] visuals = new GraphicVisual[Count];

            for (int i = 0; i < Count; i++)
            {
                visuals[0] = GroupTransOut(this[i]) as GraphicVisual;
            }

            ((ICollection<IGraphicVisual>)this).Clear();

            return visuals;
        }


        public void MoveForward(GraphicVisual visual)
        {
            int index = Children.IndexOf(visual);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(index - 1, visual);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveBack(GraphicVisual visual)
        {
            int index = Children.IndexOf(visual);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(index + 1, visual);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveToFirst(GraphicVisual visual)
        {
            int index = Children.IndexOf(visual);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Insert(0, visual);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void MoveToLast(GraphicVisual visual)
        {
            int index = Children.IndexOf(visual);
            if (0 <= index && index < Count)
            {
                Children.RemoveAt(index);
                Children.Add(visual);
                GraphicVisualGroupChildrenChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private IGraphicVisual GroupTransIn(IGraphicVisual visual)
        {
            visual.Angle = visual.Angle - this.Angle;
            visual.Origin = this.Transform.Inverse.Transform(visual.Origin);
            return visual;

        }

        private IGraphicVisual GroupTransOut(IGraphicVisual visual)
        {
            visual.Origin = this.Transform.Transform(visual.Origin);
            visual.Angle = visual.Angle + this.Angle;
            return visual;
        }
        #endregion

    }

}
