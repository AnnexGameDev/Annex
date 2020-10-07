using Annex.Scenes.Components;

namespace Annex.Scenes
{
    public class OnSceneEnterEvent
    {
        public readonly Scene PreviousScene;

        public OnSceneEnterEvent(Scene previousScene) {
            this.PreviousScene = previousScene;
        }
    }

    public class OnSceneLeaveEvent
    {
        public readonly Scene NextScene;

        public OnSceneLeaveEvent(Scene nextScene) {
            this.NextScene = nextScene;
        }
    }
}
