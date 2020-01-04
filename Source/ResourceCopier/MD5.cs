using System;
using System.IO;
using System.Text;

namespace ResourceCopier
{
    public sealed class MD5 : IDisposable
    {
        private readonly System.Security.Cryptography.MD5 _algorithm;

        public MD5() {
            this._algorithm = System.Security.Cryptography.MD5.Create();
        }

        public void Dispose() {
            this._algorithm.Dispose();
        }

        public string ComputeFileHash(string filepath) {
            return this.Compute(File.ReadAllBytes(filepath));
        }

        public string Compute(byte[] data) {
            byte[] hash = this._algorithm.ComputeHash(data);
            var sb = new StringBuilder();
            foreach (byte val in hash) {
                sb.Append(val.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
