using Annex_Old.Core.Collections.Generic;
using Annex_Old.Core.Graphics;

namespace Annex_Old.Sfml.Collections.Generic
{
    internal class CameraCache : ICameraCache
    {
        private readonly ICache<string, SfmlCamera> _cache = new Cache<string, SfmlCamera>();

        public void AddCamera(Camera camera) {
            this._cache.Add(camera.Id, new SfmlCamera(camera));
        }

        public SfmlCamera? GetCamera(string cameraId) {
            if (this._cache.TryGetValue(cameraId, out var camera)) {
                return camera;
            }
            return null;
        }

        public SfmlCamera? GetCamera(CameraId cameraId) => GetCamera(cameraId.ToString());
    }
}
