using System;
using System.Collections;
using System.Collections.Generic;

namespace AsteroidsEngine
{
    /// <summary>
    /// Надстройка над списком с событиями добавления и удаления элементов
    /// </summary>
    /// <typeparam name="T">Тип объектов в спеске</typeparam>
    public class ObservableList<T> : IEnumerable, IList, IList<T>
    {
        List<T> list = new List<T>();

        public T this[int index] { get => list[index]; set => list[index] = value; }

        public int Count => list.Count;

        public event Action<T> OnAdded;
        public event Action<T> OnRemoved;

        public void Add(T obj)
        {
            list.Add(obj);
            OnAdded?.Invoke(obj);
        }

        public int Add(object value)
        {
            return Add(value);
        }
        public bool Remove(T obj)
        {
            bool result = list.Remove(obj);
            OnRemoved?.Invoke(obj);
            return result;
        }

        public void Remove(object value)
        {
            Remove(value);
        }

        bool ICollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        public void RemoveAt(int index)
        {
            var deletingElement = list[index];
            ((IList)list).RemoveAt(index);
            OnRemoved?.Invoke(deletingElement);
        }


        #region Auto generated code
        public bool IsReadOnly => ((IList)list).IsReadOnly;

        public bool IsFixedSize => ((IList)list).IsFixedSize;

        public object SyncRoot => ((ICollection)list).SyncRoot;

        public bool IsSynchronized => ((ICollection)list).IsSynchronized;

        object IList.this[int index] { get => ((IList)list)[index]; set => ((IList)list)[index] = value; }
        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public bool Contains(object value)
        {
            return ((IList)list).Contains(value);
        }

        public void Clear()
        {
            ((IList)list).Clear();
        }

        public int IndexOf(object value)
        {
            return ((IList)list).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList)list).Insert(index, value);
        }
        public void CopyTo(Array array, int index)
        {
            ((ICollection)list).CopyTo(array, index);
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)list).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)list).Insert(index, item);
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)list).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)list).CopyTo(array, arrayIndex);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }
        #endregion
    }
}