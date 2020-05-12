using System.IO;
using static Annex.Assets.Errors;

namespace Annex.Assets.Loaders
{
    public class FileLoader : IAssetLoader
    {
        public byte[] GetBytes(string key) {
            Debug.Assert(File.Exists(key), ASSET_FILE_DOESNT_EXIST.Format(key));
            return File.ReadAllBytes(key);
        }

        public string GetString(string key) {
            Debug.Assert(File.Exists(key), ASSET_FILE_DOESNT_EXIST.Format(key));
            return key;
        }

        public void Write(string source, string destination) {
            var parent = new FileInfo(destination).Directory.FullName;
            if (!Directory.Exists(parent)) {
                Directory.CreateDirectory(parent);
            }
            File.Copy(source, destination, true);
        }
    }
}