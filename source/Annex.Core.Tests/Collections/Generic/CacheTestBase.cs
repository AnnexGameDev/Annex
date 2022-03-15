using Annex.Core.Collections.Generic;
using FluentAssertions;
using Scaffold.Tests.Core.Attributes;
using Scaffold.Tests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Collections.Generic
{
    public abstract class CacheTestBase<TKey, TValue> where TKey : notnull {
        private readonly IFixture _fixture = new Fixture();

        public CacheTestBase() {
            this._fixture.Register<ICache<TKey, TValue>>(this._fixture.Create<Cache<TKey, TValue>>);
        }

        [Theory, AutoData]
        public void GivenAValueIsAddedToTheCacheForAGivenKey_WhenTryingAndGettingAValue_ThenReturnsTrueAndTheExpectedValue(TKey theExpectedValueKey, TValue theExpectedValue) {
            // Arrange
            ICache<TKey, TValue> theCache = this._fixture.Create<ICache<TKey, TValue>>();
            theCache.Add(theExpectedValueKey, theExpectedValue);

            // Act
            bool tryGetValueDidSucceed = theCache.TryGetValue(theExpectedValueKey, out TValue theActualValue);

            // Assert
            tryGetValueDidSucceed.Should().BeTrue();
            theActualValue.Should().Be(theExpectedValue);
        }

        [Theory, AutoData]
        public void GivenAValueIsAddedToTheCacheForAGivenKey_WhenCheckingIfCacheContainsTheKey_ThenReturnsTrue(TKey aGivenKey, TValue theValue) {
            // Arrange
            ICache<TKey, TValue> theCache = this._fixture.Create<ICache<TKey, TValue>>();
            theCache.Add(aGivenKey, theValue);

            // Act
            bool cacheContainsKey = theCache.Contains(aGivenKey);

            // Assert
            cacheContainsKey.Should().BeTrue();
        }

        [Theory, AutoData]
        public void GivenAKeyThatDoesntExistInTheCache_WhenTryingAndGettingAValue_ThenReturnsFalse(TKey aKeyThatDoesntExistInTheCache) {
            // Arrange
            ICache<TKey, TValue> theCache = this._fixture.Create<ICache<TKey, TValue>>();

            // Act
            bool tryGetValueDidSucceed = theCache.TryGetValue(aKeyThatDoesntExistInTheCache, out TValue _);

            // Assert
            tryGetValueDidSucceed.Should().BeFalse();
        }

        [Theory, AutoData]
        public void GivenAKeyThatDoesntExistInTheCache_WhenCheckingIfCacheContainsTheKey_ThenReturnsFalse(TKey aKeyThatDoesntExistInTheCache) {
            // Arrange
            ICache<TKey, TValue> theCache = this._fixture.Create<ICache<TKey, TValue>>();

            // Act
            bool cacheContainsKey = theCache.Contains(aKeyThatDoesntExistInTheCache);

            // Assert
            cacheContainsKey.Should().BeFalse();
        }
    }
}
