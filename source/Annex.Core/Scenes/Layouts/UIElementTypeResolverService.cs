using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes.Layouts;

public interface IUIElementTypeResolver
{
    Type? ResolveType(string typeName);
}

public interface IUIElementTypeResolverService
{
    Type? ResolveType(string typeName);
}

internal class UIElementTypeResolverService : IUIElementTypeResolverService
{
    private readonly IEnumerable<IUIElementTypeResolver> _uiElementTypeResolvers;

    public UIElementTypeResolverService(IEnumerable<IUIElementTypeResolver> uiElementTypeResolvers) {
        _uiElementTypeResolvers = uiElementTypeResolvers;
    }

    public Type? ResolveType(string typeName) {
        foreach (var resolver in _uiElementTypeResolvers)
        {
            if (resolver.ResolveType(typeName) is Type type)
            {
                return type;
            }
        }
        return null;
    }
}

public abstract class UIElementTypeResolverBase : IUIElementTypeResolver
{
    private readonly Dictionary<string, Type> _knownTypes = new();

    protected void RegisterType<T>() where T : IUIElement {
        RegisterType(typeof(T));
    }

    protected void RegisterType(Type type) {
        _knownTypes.Add(type.Name.ToLower(), type);
    }

    public Type? ResolveType(string typeName) {
        if (_knownTypes.TryGetValue(typeName.ToLower(), out var type))
        {
            return type;
        }
        return null;
    }
}

internal class AnnexUIElementTypeResolver : UIElementTypeResolverBase
{
    public AnnexUIElementTypeResolver() {
        RegisterType<Button>();
        RegisterType<Container>();
        RegisterType<ContextMenu>();
        RegisterType<Label>();
        RegisterType<PasswordBox>();
        RegisterType<Scene>();
        RegisterType<Textbox>();
        RegisterType<Image>();
    }
}
