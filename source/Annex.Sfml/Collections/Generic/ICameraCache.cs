using Annex_Old.Core.Graphics;
using Annex_Old.Sfml.Extensions;
using SFML.Graphics;

namespace Annex_Old.Sfml.Collections.Generic
{
    public class SfmlCamera
    {
        public Camera Camera { get; }

        private View _view = new View();
        public View View
        {
            get {
                RefreshView();
                return this._view;
            }
        }

        public SfmlCamera(Camera camera) {
            this.Camera = camera;
        }

        private void RefreshView() {

            if (this._view.Center.DoesNotEqual(this.Camera.Center)) {
                this._view.Center = this.Camera.Center.ToSFML();
            }

            if (this._view.Size.DoesNotEqual(this.Camera.Size)) {
                this._view.Size = this.Camera.Size.ToSFML();
            }

            if (this._view.Rotation != this.Camera.Rotation.Value) {
                this._view.Rotation = this.Camera.Rotation.Value;
            }

            if (this._view.Viewport.DoesNotEqual(this.Camera.Region)) {
                this._view.Viewport = this.Camera.Region.ToSFML();
            }
        }
    }

    public interface ICameraCache
    {
        SfmlCamera? GetCamera(CameraId cameraId);
        SfmlCamera? GetCamera(string cameraId);
        void AddCamera(Camera camera);
    }
}
