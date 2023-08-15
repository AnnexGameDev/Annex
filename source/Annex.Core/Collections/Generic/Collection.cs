namespace Annex.Core.Collections.Generic
{
    public static class Collection
    {
        public static IEnumerable<T> Create<T>(int numElements, T initialValue = default) {
            return new T[numElements].Select(_ => initialValue);
        }

        public static IEnumerable<TOutput> Permute<TInput1, TInput2, TOutput>(IEnumerable<TInput1> source1, IEnumerable<TInput2> source2, Func<TInput1, TInput2, TOutput> combinator) {
            return source1.SelectMany(source1Element => source2.Select(source2Element => combinator(source1Element, source2Element)));
        }
    }
}
