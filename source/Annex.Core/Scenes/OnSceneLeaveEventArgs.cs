using Annex_Old.Core.Scenes.Components;

namespace Annex_Old.Core.Scenes
{
    public class OnSceneLeaveEventArgs
    {
        public IScene NextScene { get; }

        public OnSceneLeaveEventArgs(IScene nextScene) {
            this.NextScene = nextScene;
        }
    }
}