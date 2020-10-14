using System.IO;

namespace Annex.Assets.Streams
{
    public class FileSystemStreamer : DataStreamer
    {
        public FileSystemStreamer(string folder, params string[] validExtensions) : base(folder, validExtensions) {
        }
        
        public override void Persist() {
        }

        public override byte[] Read(string key) {
            string file = Path.Combine(this._folder, key);
            Debug.Assert(File.Exists(file), $"The file system file {file} doesn't exist");
            return File.ReadAllBytes(file);
        }

        public override void Write(string key, byte[] data) {
            string file = Path.Combine(this._folder, key);
            var parent = new FileInfo(file).Directory.FullName;
            if (!Directory.Exists(parent)) {
                Directory.CreateDirectory(parent);
            }
            File.WriteAllBytes(file, data);
        }
    }
}
