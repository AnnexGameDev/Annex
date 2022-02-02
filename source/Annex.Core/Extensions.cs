using System.Collections;

namespace Annex.Core
{
    public static class Extensions
    {
        // HACK: Major source of hackiness
        public static IList MakeGeneric(this IEnumerable<dynamic> col, Type type) {
            var lst = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));

            foreach (var value in col) {
                lst.Add(value);
            }

            return lst;
        }

        public static void FireAndForget(this Task task) {
            Task.Run(async () => await task).ConfigureAwait(false);
        }
    }
}