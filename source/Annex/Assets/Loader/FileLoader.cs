using System.IO;

namespace Annex.Assets.Loaders
{
    public class FileLoader : IAssetLoader
    {
        public byte[] GetBytes(string key) {
            Debug.Assert(File.Exists(key), $"The asset file {key} does not exist");
            return File.ReadAllBytes(key);
        }

        public string GetString(string key) {
            Debug.Assert(File.Exists(key), $"The asset file {key} does not exist");
            return key;
        }
    }
}