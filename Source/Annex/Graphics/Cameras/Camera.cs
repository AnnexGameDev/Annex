using Annex.Data;

namespace Annex.Graphics.Cameras
{
    public class Camera
    {
        //public Vector2f TopLeft => new Vector2f(this.Centerpoint.X - this.Size.X / 2, this.Centerpoint.Y - this.Size.Y / 2);
        public Vector2f Centerpoint { get; private set; }
        public Vector2f Size { get; private set; }
        public float CurrentZoom { get; private set; }

        public Camera() {
            this.Size = new Vector2f(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
            this.Centerpoint = new Vector2f(this.Size.X / 2, this.Size.Y / 2);
            this.CurrentZoom = 1;
        }

        public void Resize(float newWidth, float newHeight) {
            float ratio1 = newWidth / this.Size.X;
            float ratio2 = newHeight / this.Size.Y;
            Debug.Assert(ratio1 == ratio2);
            this.CurrentZoom = newHeight / GameWindow.RESOLUTION_HEIGHT;
            this.Size.Set(newWidth, newHeight);
        }

        public void Follow(Entity entity) {
            this.Centerpoint = new OffsetVector2f(entity.EntityPosition, entity.Centerpoint);
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
