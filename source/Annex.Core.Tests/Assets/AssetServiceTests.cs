using Annex.Core.Assets;
using AutoFixture;

namespace Annex.Core.Tests.Assets
{
    public class AssetServiceTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly IAssetService _assetService;

        public AssetServiceTests() {
            this._assetService = this._fixture.Create<AssetService>();
        }
    }
}