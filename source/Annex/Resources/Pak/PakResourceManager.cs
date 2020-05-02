#nullable enable
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources.Pak
{
    public class PakResourceManager : ResourceManager
    {
        protected readonly Dictionary<string, object> _resources;
        private PakFile? _pak;
        private string PakFilePath => this._resourcePath + "data.pak";

        public PakResourceManager() {
            this._resources = new Dictionary<string, object>();
        }

        public override object GetResource(string key) {
            if (this._pak == null) {
                Debug.Assert(this._resourcePath != null, $"Resource path is not set");
                this._pak = PakFile.CreateForInput(this.PakFilePath);
            }

            key = key.ToLower();
            if (!this._resources.ContainsKey(key)) {
                this.Load(key);
            }
            Debug.Assert(this._resources.ContainsKey(key), $"Resource {key} is not contained in the pak file");
            return this._resources[key];
        }

        protected override void Load(string key) {
            Debug.Assert(this._resourceLoader_FromBytes != null, $"ResourceLoader_FromBytes is not set");

            if (this._resourceValidator == null || this._resourceValidator(key)) {
                this._resources.Add(key, this._resourceLoader_FromBytes(this._pak.GetEntry(key)));
            }
        }

        protected internal override void PackageResourcesToBinary(string baseDir) {
            Debug.Assert(this._resourcePath != null, "Resource path is not set");


            var directoryInfo = new DirectoryInfo(this._resourcePath);
            var localResourceFolderInfo = directoryInfo;
            string folderName = localResourceFolderInfo.Name;
            var sourceResourceFolder = Path.Combine(baseDir, folderName);

            Directory.CreateDirectory(sourceResourceFolder);
            Directory.CreateDirectory(this._resourcePath);

            var pak = PakFile.CreateForOutput();
            foreach (var sourceFile in Directory.GetFiles(sourceResourceFolder, "*", SearchOption.AllDirectories)) {
                string id = sourceFile.Remove(0, sourceResourceFolder.Length + 1).ToLowerInvariant().Replace('\\', '/');
                byte[] data = File.ReadAllBytes(sourceFile);
                pak.AddEntry(id, data);
            }
            this._pak?.Dispose();
            this._pak = null;
            pak.Save(this.PakFilePath);
        }
    }
}
