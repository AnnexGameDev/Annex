#nullable enable
namespace Annex.Resources
{
    public interface IResourceManager
    {
        bool GetResource(IResourceLoaderArgs args, out object? resource);
    }
}
