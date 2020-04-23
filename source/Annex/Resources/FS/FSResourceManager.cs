#nullable enable
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources.FS
{
    public class FSResourceManager : ResourceManager
    {
        protected readonly Dictionary<string, object> _resources;
        
        public FSResourceManager() {
            this._resources = new Dictionary<string, object>();
        }

        public override object GetResource(string key) {
            key = key.ToLower();
            if (!this._resources.ContainsKey(key)) {
                this.Load(Path.Join(this._resourcePath, key));
            }
            Debug.Assert(this._resources.ContainsKey(key));
            return this._resources[key];
        }

        protected override void Load(string fullFilePath) {
            Debug.Assert(this._resourcePath != null);
            Debug.Assert(this._resourceLoader_FromString != null);
            Debug.Assert(File.Exists(fullFilePath));

            if (this._resourceValidator == null || this._resourceValidator(fullFilePath)) {
                string key = fullFilePath.Remove(0, this._resourcePath.Length).ToLowerInvariant().Replace('\\', '/');
                this._resources.Add(key, this._resourceLoader_FromString(Path.Combine(this._resourcePath, key)));
            }
        }

        protected internal override void PackageResourcesToBinary(string baseDir) {
            Debug.Assert(this._resourcePath != null);


            var localResourceFolderInfo = new DirectoryInfo(this._resourcePath);
            string folderName = localResourceFolderInfo.Name;
            var sourceResourceFolder = Path.Combine(baseDir, folderName);

            Directory.CreateDirectory(sourceResourceFolder);
            Directory.CreateDirectory(this._resourcePath);
            foreach (var sourceFile in Directory.GetFiles(sourceResourceFolder, "*", SearchOption.AllDirectories)) {
                string destinationFile = localResourceFolderInfo.Parent.FullName + sourceFile.Remove(0, baseDir.Length);
                var fi = new FileInfo(destinationFile);
                Directory.CreateDirectory(fi.Directory.FullName);
                File.Copy(sourceFile, destinationFile, true);
            }
        }
    }
}
