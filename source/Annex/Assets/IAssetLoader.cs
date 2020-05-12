#nullable enable
namespace Annex.Assets
{
    public interface IAssetLoader
    {
        string GetString(string key);
        byte[] GetBytes(string key);
    }
}
