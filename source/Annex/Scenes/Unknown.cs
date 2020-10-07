using Annex.Scenes.Components;
using Annex.Services;

namespace Annex.Scenes
{
    internal class Unknown : Scene
    {
        public Unknown() : base(0, 0) {

        }

        public override void OnLeave(OnSceneLeaveEvent e) {
            ServiceProvider.SceneService.UnloadScene<Unknown>();
        }
    }
}
