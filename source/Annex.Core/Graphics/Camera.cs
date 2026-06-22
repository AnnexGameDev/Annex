using Annex.Core.Data;

namespace Annex.Core.Graphics;

public class Camera
{
    private uint _originalWidth;
    private uint _originalHeight;
    private uint _originalResolutionWidth;
    private Vector2f _size = new Vector2f();

    public string CameraId { get; }
    public FloatRect Region { get; set; } = new(0, 0, 1, 1);
    public IReadonlyVector2<float> Size => _size;
    public IVector2<float> Center { get; set; } = new Vector2f();
    public IShared<float> Rotation { get; set; } = 0.0f.ToShared();

    private uint _currentZoomLevel = 1;
    private uint _maxZoomLevel = uint.MaxValue;

    public float TextRenderingSuperSampleScaleBasedOnZoom { get; private set; } = 1;

    public Camera(string cameraId, uint width, uint height)
    {
        CameraId = cameraId;
        _originalHeight = height;
        _originalWidth = _originalResolutionWidth = width;
        _size.Set(width, height);
    }

    public void Zoom(double delta)
    {
        if (delta > 0)
        {
            SetZoomLevel(_currentZoomLevel - 1);
        }
        else
        {
            SetZoomLevel(Math.Max(0, _currentZoomLevel + 1));
        }
    }

    public void SetBaseCameraDimentions(uint width, uint height)
    {
        _originalWidth = width;
        _originalHeight = height;
        SetZoomLevel(_currentZoomLevel);
    }

    public void SetZoomLevel(uint newZoomLevel)
    {
        if (newZoomLevel == 0)
        {
            return;
        }
        if (newZoomLevel > _maxZoomLevel)
        {
            if (_currentZoomLevel > _maxZoomLevel)
            {
                SetZoomLevel(_maxZoomLevel);
            }
            return;
        }

        _currentZoomLevel = newZoomLevel;
        _size.Set(_originalWidth * _currentZoomLevel, _originalHeight * _currentZoomLevel);

        const float increment = 0.5f;
        float superSampleScale = Math.Max((_originalResolutionWidth / Size.X), 1);
        float superSampleScaleInIncrement = (float)Math.Ceiling(superSampleScale / increment) * increment;
        TextRenderingSuperSampleScaleBasedOnZoom = superSampleScaleInIncrement;
    }

    public void SetMaxZoomLevel(uint zoomLevel)
    {
        _maxZoomLevel = zoomLevel;
        SetZoomLevel(_currentZoomLevel);
    }
}
