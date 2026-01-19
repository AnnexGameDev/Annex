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

    public void LoadScene<T>(object? parameters = null) where T : IScene
    {
        Log.Verbose($"Loading scene {typeof(T).Name}");

        var newScene = _container.Resolve<T>();
        var oldScene = _currentScene;

        // If the new scene can't be resolved, don't switch.
        if (newScene == null)
        {
            Log.Error($"Unable to resolve scene {typeof(T).Name}.");
            return;
        }

        SwitchTo(newScene, parameters);
    }

    private void SwitchTo<T>(T newScene, object? parameters = null) where T : IScene
    {
        var oldScene = _currentScene;

        var leavingSceneArgs = new OnSceneLeaveEventArgs(newScene);
        var enteringSceneArgs = new OnSceneEnterEventArgs(oldScene, parameters);

        _currentScene?.OnLeave(leavingSceneArgs);
        _currentScene = newScene;
        _currentScene.OnEnter(enteringSceneArgs);

        oldScene?.Dispose();
    }

    private class NullScene : Scene
    {
    }
}
