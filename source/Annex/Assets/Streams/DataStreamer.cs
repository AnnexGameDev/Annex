using System.Linq;

namespace Annex_Old.Assets.Streams
{
    public abstract class DataStreamer : IDataStreamer
    {
        protected readonly string _folder;
        protected readonly string[] _validExtensions;

        public DataStreamer(string folder, params string[] validExtensions) {
            this._folder = folder;
            this._validExtensions = validExtensions;
        }

        public bool IsValidExtension(string path) {
            return this._validExtensions.Contains(path);
        }

        public abstract void Persist();
        public abstract byte[] Read(string key);
        public abstract void Write(string key, byte[] data);
    }
}
