using Annex.Core.Collections.Generic;
using Annex.Core.Graphics;

namespace Annex.Sfml.Collections.Generic;

internal class CameraCache : ICameraCache
{
    private readonly ICache<string, SfmlCamera> _cache = new Cache<string, SfmlCamera>();

    public void AddCamera(Camera camera)
    {
        _cache.Add(camera.CameraId, new SfmlCamera(camera));
    }

    public SfmlCamera? GetCamera(string cameraId)
    {
        if (_cache.TryGetValue(cameraId, out var camera))
        {
            return camera;
        }
        return null;
    }
}
