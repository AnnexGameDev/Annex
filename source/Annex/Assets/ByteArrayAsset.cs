namespace Annex.Assets
{
    public class ByteArrayAsset : Asset
    {
        public ByteArrayAsset(string id, byte[] target) : base(id, target) {
        }

        public override void Dispose() {
        }
    }
}
