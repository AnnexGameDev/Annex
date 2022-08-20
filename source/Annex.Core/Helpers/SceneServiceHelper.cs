using Annex.Core.Scenes;
using Annex.Core.Scenes.Components;

namespace Annex.Core.Helpers;

public class SceneServiceHelper
{
    private static ISceneService? _sceneService;

    public static IScene CurrentScene => _sceneService?.CurrentScene ?? throw new NullReferenceException($"{nameof(_sceneService)} is null");

    public SceneServiceHelper(ISceneService sceneService) {
        if (_sceneService != null) {
            throw new InvalidOperationException("Static helper resource is already instanciated");
        }

        _sceneService = sceneService;
    }
}
