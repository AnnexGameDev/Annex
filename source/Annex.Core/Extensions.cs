namespace Annex.Core
{
    public static class Extensions
    {
        public static void FireAndForget(this Task task) {
            Task.Run(async () => await task).ConfigureAwait(false);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
            foreach (var element in collection) {
                action(element);
            }
        }
    }
}