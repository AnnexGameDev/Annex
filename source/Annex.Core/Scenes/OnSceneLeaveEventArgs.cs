using Annex.Core.Scenes.Elements;

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