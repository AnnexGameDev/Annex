namespace Annex.Core.Assets.Bundles
{
    public class PakFile : IAssetBundle
    {
        private string v;
        private string textureRoot;

        public PakFile(string v, string textureRoot) {
            this.v = v;
            this.textureRoot = textureRoot;
        }
    }
}