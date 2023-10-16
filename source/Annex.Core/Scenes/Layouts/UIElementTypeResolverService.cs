using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes.Layouts;

public interface IUIElementTypeResolver
{
    Type? ResolveType(string typeName, Type sceneType);
}

public interface IUIElementTypeResolverService
{
    Type? ResolveType(string typeName, Type sceneType);
}

internal class UIElementTypeResolverService : IUIElementTypeResolverService
{
    private readonly IEnumerable<IUIElementTypeResolver> _uiElementTypeResolvers;

    public UIElementTypeResolverService(IEnumerable<IUIElementTypeResolver> uiElementTypeResolvers) {
        _uiElementTypeResolvers = uiElementTypeResolvers;
    }

    public Type? ResolveType(string typeName, Type sceneType) {
        foreach (var resolver in _uiElementTypeResolvers)
        {
            if (resolver.ResolveType(typeName, sceneType) is Type type)
            {
                return type;
            }
        }
        return null;
    }
}

public abstract class UIElementTypeResolverBase : IUIElementTypeResolver
{
    private readonly Dictionary<string, Type> _knownGlobalTypes = new();
    private readonly Dictionary<Type, Dictionary<string, Type>> _knownSceneTypes = new();

    protected void RegisterGlobalType<T>() where T : IUIElement {
        RegisterGlobalType(typeof(T));
    }

    protected void RegisterGlobalType(Type type) {
        _knownGlobalTypes.Add(type.Name.ToLower(), type);
    }

    protected void RegisterSceneType<TScene, TElement>() where TScene : IScene where TElement : IUIElement {
        RegisterSceneType(typeof(TScene), typeof(TElement));
    }

    protected void RegisterSceneType(Type sceneType, Type elementType) {
        if (!_knownSceneTypes.ContainsKey(sceneType))
        {
            _knownSceneTypes.Add(sceneType, new Dictionary<string, Type>());
        }

        _knownSceneTypes[sceneType].Add(elementType.Name.ToLower(), elementType);
    }

    public Type? ResolveType(string typeName, Type sceneType) {

        typeName = typeName.ToLower();

        // Is there a registered type for that scene?
        if (_knownSceneTypes.TryGetValue(sceneType, out var sceneTypes))
        {
            if (sceneTypes.TryGetValue(typeName, out var resolvedSceneType))
            {
                return resolvedSceneType;
            }
        }

        // If not, is there a global type?
        if (_knownGlobalTypes.TryGetValue(typeName, out var resolvedGlobalType))
        {
            return resolvedGlobalType;
        }
        return null;
    }
}

internal class AnnexUIElementTypeResolver : UIElementTypeResolverBase
{
    public AnnexUIElementTypeResolver() {
        RegisterGlobalType<Button>();
        RegisterGlobalType<Container>();
        RegisterGlobalType<ContextMenu>();
        RegisterGlobalType<Label>();
        RegisterGlobalType<PasswordBox>();
        RegisterGlobalType<Scene>();
        RegisterGlobalType<Textbox>();
        RegisterGlobalType<Image>();
        RegisterGlobalType<ListView>();
    }
}
