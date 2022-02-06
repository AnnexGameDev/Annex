namespace Annex.Core.Assets.Bundles
{
    public class FileSystemDirectory : IAssetBundle
    {
        private string v;
        private string textureRoot;

        public FileSystemDirectory(string v, string textureRoot) {
            this.v = v;
            this.textureRoot = textureRoot;
        }
    }
}