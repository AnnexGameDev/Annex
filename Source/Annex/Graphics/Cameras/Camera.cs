namespace Annex.Graphics.Cameras
{
    public class Camera
    {
        public (float x, float y) TopLeft { get; private set; }
        public (float width, float height) Size { get; private set; }
        public float CurrentZoom { get; private set; }

        public Camera() {
            this.Size = (GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
            this.TopLeft = (0, 0);
            this.CurrentZoom = 1;
        }

        public void Resize(float newWidth, float newHeight) {
            float ratio1 = newWidth / this.Size.width;
            float ratio2 = newHeight / this.Size.height;
            Debug.Assert(ratio1 == ratio2);
            this.CurrentZoom = newHeight / GameWindow.RESOLUTION_HEIGHT;
            this.Size = (newWidth, newHeight);
        }

        public void Move(float x, float y, bool isCenterpoint = false) {
            if (isCenterpoint) {
                this.TopLeft = (x - this.Size.width / 2, y - this.Size.height / 2);
            } else {
                this.TopLeft = (x, y);
            }
        }
    }
}
