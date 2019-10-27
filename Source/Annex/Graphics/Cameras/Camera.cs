using Annex.Data.Shared;

namespace Annex.Graphics.Cameras
{
    public class Camera
    {
        public Vector Centerpoint { get; private set; }
        public Vector Size { get; private set; }
        public float CurrentZoom { get; private set; }

        public Camera() {
            this.Size = Vector.Create(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
            this.Centerpoint = Vector.Create(this.Size.X / 2, this.Size.Y / 2);
            this.CurrentZoom = 1;
        }

        public void Resize(float newWidth, float newHeight) {
            this.CurrentZoom = newHeight / GameWindow.RESOLUTION_HEIGHT;
            this.Size.Set(newWidth, newHeight);
        }

        public void Follow(Vector position) {
            this.Centerpoint = position;
        }

        public void SetPosition(float x, float y) {
            this.Centerpoint.Set(x, y);
        }

        public void ZoomIn(float delta) {
            this.Resize((1 + delta) * this.Size.X, (1 + delta) * this.Size.Y);
        }

        public void ZoomOut(float delta) {
            this.Resize((1 - delta) * this.Size.X, (1 - delta) * this.Size.Y);
        }

        public void Copy(Camera camera) {
            this.Centerpoint = camera.Centerpoint;
            this.Size = camera.Size;
            this.CurrentZoom = camera.CurrentZoom;
        }
    }
}
