#nullable enable
namespace Annex.Resources.Pak
{
    public class PakFileEntry
    {
        public readonly long Position;
        public readonly int Size;
        public readonly byte[]? Data;

        public PakFileEntry(long position, int size, byte[]? data = null) {
            this.Position = position;
            this.Size = size;
            this.Data = data;
        }
    }
}
