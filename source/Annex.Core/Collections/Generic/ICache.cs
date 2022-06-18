namespace Annex.Core.Collections.Generic
{
    public interface ICache<TKey, TValue> where TKey : notnull
    {
        bool TryGetValue(TKey key, out TValue value);
        bool Contains(TKey key);
        void Add(TKey key, TValue value);
    }
}