using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input;
using Annex.Core.Input.InputEvents;
using Annex.Core.Time;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using Annex.Sfml.Graphics.PlatformTargets;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using SFML.Graphics;
using SFML.Window;
using WindowBase = Annex.Core.Graphics.Windows.WindowBase;

namespace Annex.Sfml.Graphics.Windows;

internal class SfmlWindow : WindowBase, IWindow, IDisposable
{
    private RenderTexture _buffer;
    private Sprite _bufferSprite;
    private RenderWindow _renderWindow;
    private readonly ICameraCache _cameraCache;
    private readonly IPlatformTargetFactory _platformTargetFactory;
    private readonly IInputHandler _inputHandler;
    private readonly ITimeService _timeService;
    private long? _timeSinceLastDraw = null;
    private AssetRegistry? _assetsToUseThisFrame;

    public uint ResolutionWidth { get; }
    public uint ResolutionHeight { get; }

    public object Buffer => _buffer.Texture;
    public uint BufferHeight => _buffer.Size.Y;
    public uint BufferWidth => _buffer.Size.X;

    public uint Width => _renderWindow.Size.X;
    public uint Height => _renderWindow.Size.Y;
    public int Left => _renderWindow.Position.X;
    public int Top => _renderWindow.Position.Y;

    public SfmlWindow(IContainer container, string title, uint width, uint height, WindowStyle windowStyle)
        : base(container)
    {
        _cameraCache = container.Resolve<ICameraCache>()!;
        _platformTargetFactory = container.Resolve<IPlatformTargetFactory>()!;
        _inputHandler = container.Resolve<IInputHandler>()!;
        _timeService = container.Resolve<ITimeService>()!;

        ResolutionWidth = width;
        ResolutionHeight = height;

        _renderWindow = CreateWindow(title, false, width, height, 0, 0, windowStyle);

        var defaultCamera = new Camera(CameraId.Default, Width, Height)
        {
            Region = new Core.Data.FloatRect(0, 0, 1, 1),
            Center = new Vector2f(Width / 2, Height / 2),
        };
        AddCamera(defaultCamera);

        var uiCamera = new Camera(CameraId.UI, Width, Height)
        {
            Region = new Core.Data.FloatRect(0, 0, 1, 1),
            Center = new Vector2f(Width / 2, Height / 2),
        };
        AddCamera(uiCamera);
    }

    public void Dispose()
    {
        Destroy(_renderWindow);
    }

    public void SetPosition(int x, int y)
    {
        _renderWindow.Position.Set(x, y);
    }

    public void SetIcon(uint sizeX, uint sizeY, object texture)
    {
        if (texture is not Texture sfmlTexture)
        {
            return;
        }
        using var image = sfmlTexture.CopyToImage();
        this._renderWindow?.SetIcon(sizeX, sizeY, image.Pixels);
    }

    public void SetMouseImage(object texture, uint sizeX, uint sizeY, uint offsetX, uint offsetY)
    {
        if (texture is not Texture sfmlTexture)
        {
            return;
        }
        using var image = sfmlTexture.CopyToImage();
        this._renderWindow?.SetMouseCursor(new Cursor(image.Pixels, new SFML.System.Vector2u(sizeX, sizeY), new SFML.System.Vector2u(offsetX, offsetY)));
    }

    protected override void RaisePropertyChanged(string propertyName)
    {
        if (propertyName == nameof(IsVisible))
        {
            _renderWindow!.SetVisible(IsVisible);
        }
        else if (propertyName == nameof(Title))
        {
            _renderWindow!.SetTitle(Title);
        }
    }

    #region RenderWindow management
    private RenderWindow CreateWindow(string title, bool isVisible, uint resolutionX, uint resolutionY, int positionX, int positionY, WindowStyle style)
    {
        var videoMode = new VideoMode(resolutionX, resolutionY);
        var renderWindow = new RenderWindow(videoMode, title, style.ToSfmlStyle());

        renderWindow.Size.Set(resolutionX, resolutionY);
        renderWindow.Position.Set(positionX, positionY);
        renderWindow.SetVisible(isVisible);

        _buffer = new RenderTexture(resolutionX, resolutionY);
        _bufferSprite = new Sprite(_buffer.Texture);

        AttachInputHandlers(renderWindow);
        return renderWindow;
    }

    internal void Destroy(RenderWindow renderWindow)
    {
        DetattachInputHandlers(renderWindow);
        renderWindow.Dispose();
    }
    #endregion

    #region Camera
    public Camera? GetCamera(CameraId cameraId) => GetCamera(cameraId.ToString());
    public Camera? GetCamera(string cameraId) => _cameraCache.GetCamera(cameraId)?.Camera;
    public void AddCamera(Camera camera) => _cameraCache.AddCamera(camera);
    #endregion

    #region Input Events
    private void AttachInputHandlers(RenderWindow renderWindow)
    {
        renderWindow.KeyPressed += OnKeyboardKeyPressed;
        renderWindow.KeyReleased += OnKeyboardKeyReleased;
        renderWindow.Closed += OnWindowClosed;
        renderWindow.MouseButtonPressed += OnMouseButtonPressed;
        renderWindow.MouseButtonReleased += OnMouseButtonReleased;
        renderWindow.MouseWheelScrolled += OnMouseScrollWheelMoved;
        renderWindow.MouseMoved += OnMouseMoved;
        renderWindow.GainedFocus += OnGainedFocus;
        renderWindow.LostFocus += OnLostFocus;
    }

    private void DetattachInputHandlers(RenderWindow renderWindow)
    {
        renderWindow.KeyPressed -= OnKeyboardKeyPressed;
        renderWindow.KeyReleased -= OnKeyboardKeyReleased;
        renderWindow.Closed -= OnWindowClosed;
        renderWindow.MouseButtonPressed -= OnMouseButtonPressed;
        renderWindow.MouseButtonReleased -= OnMouseButtonReleased;
        renderWindow.MouseWheelScrolled -= OnMouseScrollWheelMoved;
        renderWindow.MouseMoved -= OnMouseMoved;
        renderWindow.GainedFocus -= OnGainedFocus;
        renderWindow.LostFocus += OnLostFocus;
    }

    public void OnLostFocus(object? sender, EventArgs e) => _inputHandler.HandleWindowLostFocus(this);
    public void OnGainedFocus(object? sender, EventArgs e) => _inputHandler.HandleWindowGainedFocus(this);
    public void OnKeyboardKeyPressed(object? sender, KeyEventArgs e) => _inputHandler.HandleKeyboardKeyPressed(this, e.Code.ToKeyboardKey());
    public void OnKeyboardKeyReleased(object? sender, KeyEventArgs e) => _inputHandler.HandleKeyboardKeyReleased(this, e.Code.ToKeyboardKey());
    public void OnWindowClosed(object? sender, EventArgs e) => _inputHandler.HandleWindowClosed(this);

    public void OnMouseMoved(object? sender, MouseMoveEventArgs e) => _inputHandler.HandleMouseMoved(this, RelatePointTo(e.X, e.Y, CameraId.UI));
    public void OnMouseScrollWheelMoved(object? sender, MouseWheelScrollEventArgs e) => _inputHandler.HandleMouseScrollWheelMoved(this, e.Delta);
    public void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e) => _inputHandler.HandleMouseButtonReleased(this, e.Button.ToMouseButton(), RelatePointTo(e.X, e.Y, CameraId.UI));
    public void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e) => _inputHandler.HandleMouseButtonPressed(this, e.Button.ToMouseButton(), RelatePointTo(e.X, e.Y, CameraId.UI));
    #endregion

    #region Canvas
    public void Draw(DrawContext context)
    {
        if (_assetsToUseThisFrame == null)
        {
            throw new InvalidOperationException("No assets specified for the current draw");
        }

        var platformTarget = _platformTargetFactory.GetPlatformTarget(context, _assetsToUseThisFrame);

        if (platformTarget == null)
        {
            return;
        }

        // Update the camera if we need to
        if (context.Camera != null)
        {
            var view = _cameraCache.GetCamera(context.Camera)?.View;

            if (view == null)
            {
                Log.Error($"Tried to set a view that doesn't exist: {context.Camera}");
            }
            else
            {
                _buffer.SetView(view);
            }

            platformTarget.TryDraw(_buffer);
        }
    }

    #endregion

    #region Hardware input
    public bool IsKeyDown(KeyboardKey key)
    {
        if (this._renderWindow?.HasFocus() != true)
            return false;

        return Keyboard.IsKeyPressed(key.ToSfmlKeyboardKey());
    }

    public IVector2<float> GetMousePos(CameraId cameraId = CameraId.UI)
    {
        var mousePos = Mouse.GetPosition(this._renderWindow);
        var camera = this._cameraCache.GetCamera(cameraId);
        return RelatePointTo(mousePos.X, mousePos.Y, cameraId);
    }

    public bool IsMouseButtonDown(MouseButton button)
    {
        return Mouse.IsButtonPressed(button.ToSfml());
    }

    public bool IsControllerConnected(uint controllerId)
    {
        return Joystick.IsConnected(controllerId);
    }

    public bool IsControllerButtonPressed(uint controllerId, ControllerButton button)
    {
        return Joystick.IsButtonPressed(controllerId, button.ToSfml());
    }

    public float GetControllerJoystickAxis(uint controllerId, ControllerJoystickAxis axis)
    {
        return Joystick.GetAxisPosition(controllerId, axis.ToSfml());
    }
    #endregion

    private IVector2<float> RelatePointTo(int x, int y, CameraId cameraId)
    {
        return RelatePointTo(x, y, cameraId.ToString());
    }

    private IVector2<float> RelatePointTo(int x, int y, string cameraId)
    {
        var camera = _cameraCache.GetCamera(cameraId);

        if (camera == null)
            throw new NullReferenceException($"The camera {cameraId} couldn't be found");

        if (_renderWindow == null)
            throw new NullReferenceException($"{nameof(_renderWindow)} is null when performing {nameof(RelatePointTo)}");

        var viewPos = _renderWindow.MapPixelToCoords(new SFML.System.Vector2i(x, y), camera.View);
        return new Vector2f(viewPos.X, viewPos.Y);
    }

    public (float top, float left, float bottom, float right) GetCameraBounds(string cameraId)
    {
        var camera = _cameraCache.GetCamera(cameraId);

        if (camera == null)
            throw new NullReferenceException($"The camera {cameraId} couldn't be found");

        float halfWidth = camera.View.Size.X / 2;
        float halfHeight = camera.View.Size.Y / 2;
        float centerX = camera.View.Center.X;
        float centerY = camera.View.Center.Y;

        return (centerY - halfHeight, centerX - halfWidth, centerY + halfHeight, centerX + halfWidth);
    }

    public Task DrawCurrentSceneAsync()
    {
        _assetsToUseThisFrame = Scene.Assets;
        _renderWindow.Clear();
        _buffer.Clear();

        long now = _timeService.Now;
        Scene.DrawOn(this, _timeService.ElapsedTimeSince(_timeSinceLastDraw ?? now));
        _timeSinceLastDraw = now;

        _buffer.Display();
        _renderWindow.Draw(_bufferSprite);
        _renderWindow.Display();
        _renderWindow.DispatchEvents();
        _assetsToUseThisFrame = null;
        return Task.CompletedTask;
    }

    public void UpdateBuffer()
    {
        _renderWindow.Draw(_bufferSprite);
    }

    private (float x, float y) GetCameraPoint(string cameraId, float uiCameraSpaceX, float uiCameraSpaceY)
    {
        if (cameraId == CameraId.UI.ToString())
        {
            return (uiCameraSpaceX, uiCameraSpaceY);
        }

        var uiCamera = GetCamera(CameraId.UI);
        var (top, left, bottom, right) = GetCameraBounds(cameraId);

        return (
            (right - left) * uiCameraSpaceX / uiCamera.Size.X + left,
            (bottom - top) * uiCameraSpaceY / uiCamera.Size.Y + top
        );
    }

    public (float x, float y) GetCameraPoint(string cameraId, MouseButtonPressedEvent @event) => GetCameraPoint(cameraId, @event.WindowX, @event.WindowY);
    public (float x, float y) GetCameraPoint(CameraId cameraId, MouseButtonPressedEvent @event) => GetCameraPoint(cameraId.ToString(), @event);
    public (float x, float y) GetCameraPoint(string cameraId, MouseButtonReleasedEvent @event) => GetCameraPoint(cameraId, @event.WindowX, @event.WindowY);
    public (float x, float y) GetCameraPoint(CameraId cameraId, MouseButtonReleasedEvent @event) => GetCameraPoint(cameraId.ToString(), @event);
    public (float x, float y) GetCameraPoint(string cameraId, MouseMovedEvent @event) => GetCameraPoint(cameraId, @event.WindowX, @event.WindowY);
    public (float x, float y) GetCameraPoint(CameraId cameraId, MouseMovedEvent @event) => GetCameraPoint(cameraId.ToString(), @event);
}