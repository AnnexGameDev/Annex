using System;
using System.Collections.Generic;

namespace Annex
{
    public abstract class Singleton
    {
        private static readonly Dictionary<Type, Singleton> _singletons;

        static Singleton() {
            _singletons = new Dictionary<Type, Singleton>();
        }
        /// <summary>
        /// Creates a singleton of type A under the key of B.
        /// </summary>
        /// <typeparam name="B">Key type.</typeparam>
        /// <typeparam name="A">Actual instance.</typeparam>
        /// <returns></returns>
        protected static A Create<B, A>() where A : Singleton, new() {
            Debug.Assert(!SingletonExists<B>());
            _singletons.Add(typeof(B), new A());
            return (A)_singletons[typeof(B)];
        }

        protected static T Create<T>() where T : Singleton, new() {
            return Create<T, T>();
        }

        protected static T Get<T>() where T : Singleton {
            Debug.Assert(SingletonExists<T>());
            return (T)_singletons[typeof(T)];
        }

        protected static bool SingletonExists<T>() {
            return _singletons.ContainsKey(typeof(T));
        }
    }
}
