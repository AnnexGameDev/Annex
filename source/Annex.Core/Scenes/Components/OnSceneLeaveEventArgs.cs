namespace Annex.Core.Scenes.Components
{
    public class OnSceneLeaveEventArgs
    {
        public IScene NextScene { get; }

        public OnSceneLeaveEventArgs(IScene nextScene) {
            this.NextScene = nextScene;
        }
    }
}