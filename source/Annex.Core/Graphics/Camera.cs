using Annex.Core.Data;

namespace Annex.Core.Graphics;

public class Camera
{
    private float _aspectRatio;
    private Vector2f _size = new Vector2f();

    public string Id { get; }
    public FloatRect Region { get; set; } = new(0, 0, 1, 1);
    public IReadonlyVector2<float> Size => _size;
    public IVector2<float> Center { get; set; } = new Vector2f();
    public IShared<float> Rotation { get; set; } = 0.0f.ToShared();

    public Camera(string id, uint width, uint height)
    {
        Id = id;
        _size.Set(width, height);
        _aspectRatio = (float)width / height;
    }

    public Camera(CameraId id, uint width, uint height) : this(id.ToString(), width, height)
    {
    }

    public void Zoom(float percentage)
    {
        var newY = Size.Y * percentage;
        var newX = newY * _aspectRatio;
        _size.Set(newX, newY);
    }
}
