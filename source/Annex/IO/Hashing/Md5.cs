using System;
using System.IO;
using System.Text;

namespace Annex.IO.Hashing
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
            Debug.Assert(File.Exists(filepath));
            return this.Compute(File.ReadAllBytes(filepath));
        }

        public string Compute(byte[] data) {
            Debug.Assert(data.Length != 0);
            byte[] hash = this._algorithm.ComputeHash(data);
            var sb = new StringBuilder();
            foreach (byte val in hash) {
                sb.Append(val.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
