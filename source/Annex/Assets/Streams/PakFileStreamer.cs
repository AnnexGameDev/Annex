using Annex_Old.Assets.Streams.PakFile;
using System.IO;

namespace Annex_Old.Assets.Streams
{
    public class PakFileStreamer : DataStreamer
    {
        private PakFile.PakFile? _pakFile;
        private readonly PakFileBuilder _builder;
        private string PakFilePath => this._folder + ".pak";

        public PakFileStreamer(string folder, params string[] validExtensions) : base(folder, validExtensions) {
            this._builder = new PakFileBuilder();

            if (File.Exists(this.PakFilePath)) {
                this._pakFile = new PakFile.PakFile(this.PakFilePath);
            }
        }

        public override void Persist() {
            this._pakFile = this._builder.Build(this.PakFilePath);
        }

        public override byte[] Read(string key) {
            Debug.Assert(this._pakFile != null, "PakFile must be built before reading.");
            return this._pakFile!.GetEntry(key);
        }

        public override void Write(string key, byte[] data) {
            this._pakFile?.Dispose();
            this._pakFile = null;

            this._builder.Add(key, data);
        }
    }
}
