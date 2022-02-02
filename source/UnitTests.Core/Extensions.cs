using Moq;
using Moq.Language.Flow;
using System.Linq.Expressions;

namespace UnitTests.Core
{
    public static class Extensions
    {
        public static void VerifyMany<T>(this IEnumerable<Mock<T>> collection, Expression<Action<T>> exp, Times times) where T : class {
            foreach (var entry in collection) {
                entry.Verify(exp, times);
            }
        }

        public static IEnumerable<ISetup<T>> SetupMany<T>(this IEnumerable<Mock<T>> collection, Expression<Action<T>> exp) where T : class {
            foreach (var entry in collection) {
                yield return entry.Setup(exp);
            }
        }

        public static IEnumerable<ICallbackResult> CallbackMany<T>(this IEnumerable<ISetup<T>> collection, Action<T> action) where T : class {
            foreach (var entry in collection) {
                yield return entry.Callback(action);
            }
        }
    }
}