namespace Annex.Core.Assets;

internal class CacheEntry<T>
{
    public T Value { get; private set; }
    public long LastHit { get; private set; }

    public CacheEntry(T value, long now)
    {
        Value = value;
        LastHit = now;
    }

    public void Hit(long now)
    {
        LastHit = now;
    }
}
