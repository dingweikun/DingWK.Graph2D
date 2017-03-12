using System;
using System.Collections;
using System.Collections.Generic;

namespace DingWK.Graphic2D.Graphics
{

    public sealed class GroupGraphic : Graphic, ICollection<Graphic>
    {

        private readonly List<Graphic> _children;


        #region constructors

        public GroupGraphic()
        {
            _children = new List<Graphic>();
        }

        public GroupGraphic(ICollection<Graphic> graphics)
            : this()
        {
            foreach (Graphic g in graphics)
                _children.Add(g);
        }

        #endregion


        #region ICollection<Graphic> implementation

        public int Count => _children.Count;

        bool ICollection<Graphic>.IsReadOnly => ((ICollection<Graphic>)_children).IsReadOnly;

        public void Add(Graphic item) => _children.Add(item);

        public void Clear() => _children.Clear();

        public bool Contains(Graphic item)
        {
            return _children.Contains(item);
        }

        public void CopyTo(Graphic[] array, int arrayIndex)
        {
            _children.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Graphic> GetEnumerator()
        {
            return ((ICollection<Graphic>)_children).GetEnumerator();
        }

        public bool Remove(Graphic item)
        {
            return _children.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator() => ((ICollection<Graphic>)_children).GetEnumerator();

        #endregion


        //#region IList<Graphic> implemente

        //public Graphic this[int index]
        //{
        //    get { return _children[index]; }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException(nameof(value));
        //        _children[index] = value;
        //    }
        //}

        //public int Count => _children.Count;

        //public void Add(Graphic item)
        //{
        //    if (item == null)
        //        throw new ArgumentNullException(nameof(item));
        //    _children.Add(item);
        //}

        //public void Clear() => _children.Clear();

        //public bool Contains(Graphic item) => _children.Contains(item);

        //public void CopyTo(Graphic[] array, int arrayIndex) => _children.CopyTo(array, arrayIndex);

        //public IEnumerator<Graphic> GetEnumerator() => ((IList<Graphic>)_children).GetEnumerator();

        //public int IndexOf(Graphic item) => _children.IndexOf(item);

        //public void Insert(int index, Graphic item)
        //{
        //    if (item == null)
        //        throw new ArgumentNullException(nameof(item));
        //    _children.Insert(index, item);
        //}

        //public bool Remove(Graphic item) => _children.Remove(item);

        //public void RemoveAt(int index) => _children.RemoveAt(index);

        //IEnumerator IEnumerable.GetEnumerator() => ((IList<Graphic>)_children).GetEnumerator();

        //bool ICollection<Graphic>.IsReadOnly => ((ICollection<Graphic>)_children).IsReadOnly;

        //#endregion



    }

}


