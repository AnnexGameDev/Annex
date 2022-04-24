using Annex.Core.Scenes.Components;

namespace Annex.Core.Scenes
{
    public class OnSceneLeaveEventArgs
    {
        public IScene NextScene { get; }

        public OnSceneLeaveEventArgs(IScene nextScene) {
            this.NextScene = nextScene;
        }
    }
}