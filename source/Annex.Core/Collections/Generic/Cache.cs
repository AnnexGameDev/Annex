using System.Collections.Concurrent;

namespace Annex.Core.Collections.Generic
{
    //TODO: TEST ME
    public class Cache<TKey, TValue> : ICache<TKey, TValue> where TKey : notnull
    {
        private readonly IDictionary<TKey, CacheEntry> _dict = new ConcurrentDictionary<TKey, CacheEntry>();

        public void Add(TKey key, TValue value) {
            this._dict.Add(key, new CacheEntry(value));
        }

        public bool Contains(TKey key) {
            return this._dict.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value) {
            if (this.Contains(key)) {
                value = this._dict[key].Value;
                return true;
            }
            value = default;
            return false;
        }

        private class CacheEntry
        {
            public long LastAccessTime { get; private set; }

            private TValue _value;
            public TValue Value
            {
                get
                {
                    this.LastAccessTime = Environment.TickCount;
                    return _value;
                }
                set
                {
                    this._value = value;
                }
            }

            public CacheEntry(TValue value) {
                this.LastAccessTime = long.MinValue;
            }
        }
    }
}