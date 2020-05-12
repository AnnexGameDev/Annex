using Annex.Assets.Managers;
using static Annex.Assets.Errors;

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
            Debug.Error(STRING_LOADING_NOT_SUPPORTED.Format(nameof(PakLoader)));
            return string.Empty;
        }
    }
}
