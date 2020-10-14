using Annex.Assets.Streams;
using NUnit.Framework;
using System;

namespace Tests.Assets.Streams
{
    public class AESEncryptionStreamerTests
    {
        private Random _rng = new Random();

        [Test]
        public void TestEncryptDecrypt() {
            var data = new byte[1024];
            this._rng.NextBytes(data);

            var stream = new AESEncryptionStreamer(Guid.NewGuid().ToString(), new StorageStreamer());

            stream.Write("", data);
            Assert.AreEqual(data, stream.Read(""));
        }

        private class StorageStreamer : IDataStreamer
        {
            private byte[] _data;

            public bool IsValidExtension(string path) {
                return true;
            }

            public void Persist() {
            }

            public byte[] Read(string key) {
                return _data;
            }

            public void Write(string key, byte[] data) {
                this._data = data;
            }
        }
    }
}
