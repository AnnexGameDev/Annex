using Annex.Core.Data;

namespace Annex.Core.Graphics;

public class Camera
{
    private readonly uint _originalWidth;
    private readonly uint _originalHeight;
    private Vector2f _size = new Vector2f();

    public string Id { get; }
    public FloatRect Region { get; set; } = new(0, 0, 1, 1);
    public IReadonlyVector2<float> Size => _size;
    public IVector2<float> Center { get; set; } = new Vector2f();
    public IShared<float> Rotation { get; set; } = 0.0f.ToShared();

    public IShared<float> TextRenderingSuperSampleScaleBasedOnZoom { get; } = new Shared<float>(1);

    public Camera(string id, uint width, uint height)
    {
        Id = id;
        _originalHeight = height;
        _originalWidth = width;
        _size.Set(width, height);
    }

    public Camera(CameraId id, uint width, uint height) : this(id.ToString(), width, height)
    {
    }

    public void Zoom(float percentage)
    {
        var newY = Size.Y * percentage;
        var newX = newY * _originalWidth / _originalHeight;
        _size.Set(newX, newY);

        const float increment = 0.5f;
        float superSampleScale = Math.Max((_originalWidth / Size.X), 1);
        float superSampleScaleInIncrement = (float)Math.Ceiling(superSampleScale / increment) * increment;
        TextRenderingSuperSampleScaleBasedOnZoom.Set(superSampleScaleInIncrement);
    }
}
