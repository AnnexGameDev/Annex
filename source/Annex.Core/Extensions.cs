using Annex.Core.Assets;
using Scaffold.DependencyInjection;
using Scaffold.Extensions;

namespace Annex.Core;

public static class Extensions
{
    public static void FireAndForget(this Task task) {
        Task.Run(async () => await task).ConfigureAwait(false);
    }

    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
        foreach (var element in collection)
        {
            action(element);
        }
    }

    public static IEnumerable<K> Indicies<T, K>(this IEnumerable<T> collection, Func<int, K> selector) {
        int count = collection.Count();
        for (int i = 0; i < count; i++)
        {
            yield return selector(i);
        }
    }

    public static IEnumerable<K> Select<T, K>(this IEnumerable<T> collection, Func<int, T, K> selector) {
        int count = collection.Count();
        for (int i = 0; i < count; i++)
        {
            yield return selector(i, collection.ElementAt(i));
        }
    }

    public static string ToCamelCaseWord(this string str) {
        return $"{char.ToUpper(str[0])}{str[1..].ToLower()}";
    }

    public static void RegisterAssetGroup(this IContainer container, string groupId) {
        container.RegisterAggregate<IAssetGroup>(() => new AssetGroup(groupId));
    }
}