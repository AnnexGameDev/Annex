using System.IO;

namespace Annex.Resources.Loaders
{
    public class FileLoader : IDataLoader
    {
        public byte[] GetBytes(string key) {
            Debug.Assert(File.Exists(key), $"The resource file {key} does not exist");
            return File.ReadAllBytes(key);
        }

        public string GetString(string key) {
            Debug.Assert(File.Exists(key), $"The resource file {key} does not exist");
            return key;
        }
    }
}