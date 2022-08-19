namespace Annex_Old.Assets.Streams.PakFile
{
    public class PakFileEntry
    {
        public readonly long Position;
        public readonly int Size;

        public PakFileEntry(long position, int size) {
            this.Position = position;
            this.Size = size;
        }
    }
}
