using Moq;
using Moq.Language.Flow;
using System.Linq.Expressions;

namespace UnitTests.Core
{
    public static class Extensions
    {
        public static IEnumerable<T> Objects<T>(this IEnumerable<Mock<T>> collection) where T : class {
            return collection.Select(x => x.Object);
        }

        public static void VerifyAll<T>(this IEnumerable<Mock<T>> collection, Expression<Action<T>> exp, Times times) where T : class {
            foreach (var entry in collection) {
                entry.Verify(exp, times);
            }
        }

        public static IEnumerable<ISetup<T>> SetupAll<T>(this IEnumerable<Mock<T>> collection, Expression<Action<T>> exp) where T : class {
            foreach (var entry in collection) {
                yield return entry.Setup(exp);
            }
        }

        public static IEnumerable<ISetup<T, TResult>> SetupAll<T, TResult>(this IEnumerable<Mock<T>> collection, Expression<Func<T, TResult>> exp) where T : class {
            foreach (var entry in collection) {
                yield return entry.Setup(exp);
            }
        }

        public static IEnumerable<IReturnsResult<T>> AllReturn<T, TResult>(this IEnumerable<ISetup<T, TResult>> setups, TResult returnValue) where T : class {
            foreach (var setup in setups) {
                yield return setup.Returns(returnValue);
            }
        }

        public static IEnumerable<ICallbackResult> CallbackMany<T>(this IEnumerable<ISetup<T>> collection, Action<T> action) where T : class {
            foreach (var entry in collection) {
                yield return entry.Callback(action);
            }
        }
    }
}