using Annex.Assets.Streams;
using System.IO;
using System.Linq;

namespace Annex.Assets.Loaders
{
    public class FileSystemStreamer : IDataStreamer
    {
        private readonly string _folder;
        private readonly string[] _validExtensions;

        public FileSystemStreamer(string folder, params string[] validExtensions) {
            this._folder = folder;
            this._validExtensions = validExtensions;
        }

        public void Persist() {
        }

        public bool IsValidExtension(string path) {
            return this._validExtensions.Contains(path);
        }

        public byte[] Read(string key) {
            string file = Path.Combine(this._folder, key);
            Debug.Assert(File.Exists(file), $"The file system file {file} doesn't exist");
            return File.ReadAllBytes(file);
        }

        public void Write(string key, byte[] data) {
            string file = Path.Combine(this._folder, key);
            var parent = new FileInfo(file).Directory.FullName;
            if (!Directory.Exists(parent)) {
                Directory.CreateDirectory(parent);
            }
            File.WriteAllBytes(file, data);
        }
    }
}
