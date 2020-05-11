#nullable enable
namespace Annex.Resources
{
    public interface IDataLoader
    {
        string GetString(string key);
        byte[] GetBytes(string key);
    }
}
