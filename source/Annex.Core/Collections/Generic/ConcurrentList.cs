using System.Collections;

namespace Annex.Core.Collections.Generic
{
    public partial class ConcurrentList<T>
    {
        private readonly List<T> _internalList;

        public ConcurrentList() {
            this._internalList = new List<T>();
        }
    }

    public partial class ConcurrentList<T> : IList<T>
    {
        public T this[int index]
        {
            get { lock (this._internalList) return this._internalList[index]; }
            set { lock (this._internalList) this._internalList[index] = value; }
        }

        public int Count { get { lock (this._internalList) return _internalList.Count; } }
        public bool IsReadOnly => false;

        public void Add(T item) { lock (this._internalList) this._internalList.Add(item); }

        public void Clear() { lock (this._internalList) this._internalList.Clear(); }

        public bool Contains(T item) { lock (this._internalList) return this._internalList.Contains(item); }

        public void CopyTo(T[] array, int arrayIndex) { lock (this._internalList) this._internalList.CopyTo(array, arrayIndex); }

        public IEnumerator<T> GetEnumerator() { lock (this._internalList) return this._internalList.GetEnumerator(); }

        public int IndexOf(T item) { lock (this._internalList) return this._internalList.IndexOf(item); }

        public void Insert(int index, T item) { lock (this._internalList) this._internalList.Insert(index, item); }

        public bool Remove(T item) { lock (this._internalList) return this._internalList.Remove(item); }

        public void RemoveAt(int index) { lock (this._internalList) this._internalList.RemoveAt(index); }

        IEnumerator IEnumerable.GetEnumerator() { lock (this._internalList) return this._internalList.GetEnumerator(); }
    }

    public partial class ConcurrentList<T> : IConcurrentList<T>
    {
        public void AddRange(IEnumerable<T> collection) {
            lock (this._internalList) {
                this._internalList.AddRange(collection);
            }
        }

        public void Remove(Func<T, bool> predicate) {
            lock (this._internalList) {
                for (int i = 0; i < this._internalList.Count; i++) {
                    if (predicate(this._internalList[i])) {
                        this._internalList.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}