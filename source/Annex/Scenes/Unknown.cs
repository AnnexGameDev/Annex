using Annex_Old.Scenes.Components;
using Annex_Old.Services;

namespace Annex_Old.Scenes
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
