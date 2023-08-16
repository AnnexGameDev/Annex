using Annex.Core.Data;

namespace Annex.Core.Graphics
{
    public class Camera
    {
        public string Id { get; }
        public FloatRect Region { get; set; } = new(0, 0, 1, 1);
        public IVector2<float> Size { get; set; } = new Vector2f();
        public IVector2<float> Center { get; set; } = new Vector2f();
        public IShared<float> Rotation { get; set; } = 0.0f.ToShared();

        public Camera(CameraId id) : this(id.ToString()) {

        }

        public Camera(string id) {
            this.Id = id;
        }
    }
}
