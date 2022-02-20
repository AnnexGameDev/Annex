namespace Annex.Core.Collections.Generic
{
    public interface IConcurrentList<T> : IList<T>
    {
        void Remove(Func<T, bool> predicate);
        void AddRange(IEnumerable<T> collection);
    }
}