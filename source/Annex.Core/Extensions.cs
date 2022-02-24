namespace Annex.Core
{
    public static class Extensions
    {
        public static void FireAndForget(this Task task) {
            Task.Run(async () => await task).ConfigureAwait(false);
        }
    }
}