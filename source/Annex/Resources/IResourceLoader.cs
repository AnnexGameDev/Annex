#nullable enable
namespace Annex.Resources
{
    public interface IResourceLoader
    {
        bool Validate(IResourceLoaderArgs args);
        object? Load(IResourceLoaderArgs args, IDataLoader resourceLoader);
    }
}
