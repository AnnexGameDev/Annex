using Annex.Core.Scenes;
using Annex.Core.Scenes.Elements;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using System.Runtime.CompilerServices;

namespace Annex.Core.Graphics.Windows;

public abstract class WindowBase
{
    public Guid Id { get; } = Guid.NewGuid();

    private bool _isVisible = true;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }
    }

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            RaisePropertyChanged();
        }
    }

    private IScene _currentScene = new NullScene();
    private IContainer _container;

    public IScene Scene => _currentScene;
    public bool IsCurrentScene<T>() where T : IScene => _currentScene is T;

    public WindowBase(IContainer container)
    {
        _container = container;
    }

    protected abstract void RaisePropertyChanged([CallerMemberName] string property = "");

    public void LoadScene(IScene newScene, object? parameters = null, bool disposeOldScene = true)
    {
        Log.Normal($"Loading scene {newScene.GetType().Name}");

        var oldScene = _currentScene;

        var leavingSceneArgs = new OnSceneLeaveEventArgs(newScene);
        var enteringSceneArgs = new OnSceneEnterEventArgs(oldScene, parameters);

        _currentScene?.OnLeave(leavingSceneArgs);
        _currentScene = newScene;
        _currentScene.OnEnter(enteringSceneArgs);

        if (disposeOldScene)
        {
            oldScene?.Dispose();
        }
    }

    public void LoadScene<T>(object? parameters = null, bool disposeOldScene = true) where T : IScene
    {
        LoadScene(typeof(T), parameters, disposeOldScene);
    }

    public void LoadScene(Type sceneType, object? parameters = null, bool disposeOldScene = true)
    {
        if (!sceneType.IsAssignableTo(typeof(IScene)))
        {
            throw new ArgumentException($"Unable to cast {sceneType.Name} to a Scene");
        }
        IScene? newScene = _container.Resolve(sceneType) as IScene;

        // If the new scene can't be resolved, don't switch.
        if (newScene == null)
        {
            Log.Error($"Unable to resolve scene {sceneType.Name}.");
            return;
        }

        LoadScene(newScene, parameters, disposeOldScene);
    }

    private class NullScene : Scene
    {
        public NullScene() : base(null)
        {
        }
    }
}
