using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Annex_Old.Assets.Streams
{
    public class AESEncryptionStreamer : IDataStreamer
    {
        private readonly IDataStreamer _baseDataStreamer;
        private readonly Aes aes;
        private readonly Rfc2898DeriveBytes rfc;

        public AESEncryptionStreamer(string password, IDataStreamer dataStreamer) {
            this._baseDataStreamer = dataStreamer;

            this.aes = Aes.Create();
            this.rfc = new Rfc2898DeriveBytes(password, new SHA256Managed().ComputeHash(Encoding.Unicode.GetBytes(password)));
            this.aes.Key = rfc.GetBytes(32);
            this.aes.IV = rfc.GetBytes(16);
        }

        public bool IsValidExtension(string path) {
            return this._baseDataStreamer.IsValidExtension(path);
        }

        public void Persist() {
            this._baseDataStreamer.Persist();
        }

        public byte[] Read(string key) {
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, this.aes.CreateDecryptor(), CryptoStreamMode.Write)) {
                    cs.Write(this._baseDataStreamer.Read(key));
                }
                return ms.ToArray();
            }
        }

        public void Write(string key, byte[] data) {
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, this.aes.CreateEncryptor(), CryptoStreamMode.Write)) {
                    cs.Write(data);
                }
                this._baseDataStreamer.Write(key, ms.ToArray());
            }
        }
    }
}
