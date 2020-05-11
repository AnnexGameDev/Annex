using Annex.Resources.Managers;

namespace Annex.Resources.Loaders
{
    public class PakLoader : IDataLoader
    {
        private readonly PakFile _pakFile;

        public PakLoader(PakFile pakFile) {
            this._pakFile = pakFile;
        }

        public byte[] GetBytes(string key) {
            return this._pakFile.GetEntry(key);
        }

        public string GetString(string key) {
            Debug.Error($"PakResourceLoader does not support string loading");
            return string.Empty;
        }
    }
}
