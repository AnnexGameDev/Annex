namespace Annex_Old.Assets.Streams
{
    public interface IDataStreamer
    {
        bool IsValidExtension(string path);
        void Write(string key, byte[] data);
        byte[] Read(string key);
        void Persist();
    }
}
