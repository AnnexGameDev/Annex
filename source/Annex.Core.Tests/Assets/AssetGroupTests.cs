﻿using Annex.Core.Assets;
using Annex.Core.Assets.Bundles;
using FluentAssertions;
using Moq;
using Scaffold.Tests.Core;
using Scaffold.Tests.Core.Attributes;
using Scaffold.Tests.Core.Fixture;
using Xunit;

namespace Annex.Core.Tests.Assets
{
    public class AssetGroupTests
    {
        private readonly IFixture _fixture = new Fixture();

        public AssetGroupTests() {
            this._fixture.Register<IAssetGroup>(this._fixture.Create<AssetGroup>);
        }

        [Theory, AutoData]
        public void GivenNoBundleHasAnAssetForAGivenId_WhenGettingAsset_ThenReturnsNull(string aGivenAssetId) {
            // Arrange
            var theAssetGroup = this._fixture.Create<IAssetGroup>();

            // Act
            var theResolvedAsset = theAssetGroup.GetAsset(aGivenAssetId);

            // Assert
            theResolvedAsset.Should().BeNull();
        }

        [Theory, AutoData]
        public void GivenABundleHasAnAssetForAGivenId_WhenGettingAsset_ThenReturnsTheAsset(string aGivenAssetId) {
            // Arrange
            var anAssetBundleMock = this._fixture.Create<Mock<IAssetBundle>>();
            var theAssetGroup = this._fixture.Create<IAssetGroup>();
            var theExpectedAsset = this._fixture.Create<IAsset>();

            anAssetBundleMock.Setup(assetBundle => assetBundle.GetAsset(aGivenAssetId)).Returns(theExpectedAsset);

            theAssetGroup.AddBundle(anAssetBundleMock.Object);

            // Arrange
            var theActualAsset = theAssetGroup.GetAsset(aGivenAssetId);

            // Assert
            theActualAsset.Should().Be(theExpectedAsset);
        }

        [Theory, AutoData]
        public void GivenMultipleBundlesHaveAnAssetForAGivenId_WhenGettingAsset_ThenReturnsNull(string aGivenAssetId) {
            // Arrange
            var theAssetBundleMocks = this._fixture.CreateMany<Mock<IAssetBundle>>();
            var theAssetGroup = this._fixture.Create<IAssetGroup>();
            var theExpectedAsset = this._fixture.Create<IAsset>();

            theAssetBundleMocks
                .SetupAll(assetBundle => assetBundle.GetAsset(aGivenAssetId))
                .AllReturn(theExpectedAsset);

            foreach (var bundleMock in theAssetBundleMocks)
                theAssetGroup.AddBundle(bundleMock.Object);

            // Act
            var theResolvedAsset = theAssetGroup.GetAsset(aGivenAssetId);

            // Assert
            theResolvedAsset.Should().BeNull();
        }
    }
}