using Annex.Data;
using Annex.Data.Binding;

namespace Annex.Graphics.Cameras
{
    public class Camera
    {
        //public Vector2f TopLeft => new Vector2f(this.Centerpoint.X - this.Size.X / 2, this.Centerpoint.Y - this.Size.Y / 2);
        public PVector Centerpoint { get; private set; }
        public PVector Size { get; private set; }
        public float CurrentZoom { get; private set; }

        public Camera() {
            this.Size = new PVector(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
            this.Centerpoint = new PVector(this.Size.X / 2, this.Size.Y / 2);
            this.CurrentZoom = 1;
        }

        public void Resize(float newWidth, float newHeight) {
            this.CurrentZoom = newHeight / GameWindow.RESOLUTION_HEIGHT;
            this.Size.Set(newWidth, newHeight);
        }

        public void Follow(Entity entity) {
            this.Centerpoint = new ScalingOffsetPVector(entity.EntityPosition, entity.EntitySize, 0.5f, 0.5f);
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
    }
}
