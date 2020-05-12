using Annex.Assets.Managers;

namespace Annex.Assets.Loaders
{
    public class PakLoader : IAssetLoader
    {
        private readonly PakFile _pakFile;

        public PakLoader(PakFile pakFile) {
            this._pakFile = pakFile;
        }

        public byte[] GetBytes(string key) {
            return this._pakFile.GetEntry(key);
        }

        public string GetString(string key) {
            Debug.Error($"{nameof(PakLoader)} does not support string loading");
            return string.Empty;
        }
    }
}
