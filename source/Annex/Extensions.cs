using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex
{
    public static class Extensions
    {
        public static string Format(this string str, params object[] args) {
            return string.Format(str, args);
        }

        public static string ToCamelCaseWord(this string str) {
            return $"{char.ToUpper(str[0])}{str[1..].ToLower()}";
        }

        public static IEnumerable<T> Where_Safe<T>(this IEnumerable<T> collection, Func<T, bool> cond) {
            int count = collection.Count();
            for (int i = 0; i < count; i++) {
                var element = collection.ElementAt(i);
                if (cond(element)) {
                    yield return element;
                }
            }
        }
    }
}
