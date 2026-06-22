using Annex.Core.Graphics;
using Annex.Sfml.Extensions;
using SFML.Graphics;

namespace Annex.Sfml.Collections.Generic;

internal class SfmlCamera
{
    public Camera Camera { get; }

    private View _view = new View();
    public View View
    {
        get
        {
            RefreshView();
            return _view;
        }
    }

    public SfmlCamera(Camera camera)
    {
        Camera = camera;
    }

    private void RefreshView()
    {

        if (_view.Center.DoesNotEqual(Camera.Center))
        {
            _view.Center = Camera.Center.ToSFML();
        }

        if (_view.Size.DoesNotEqual(Camera.Size))
        {
            _view.Size = Camera.Size.ToSFML();
        }

        if (_view.Rotation != Camera.Rotation.Value)
        {
            _view.Rotation = Camera.Rotation.Value;
        }

        if (_view.Viewport.DoesNotEqual(Camera.Region))
        {
            _view.Viewport = Camera.Region.ToSFML();
        }
    }
}

internal interface ICameraCache
{
    SfmlCamera? GetCamera(string cameraId);
    void AddCamera(Camera camera);
}
