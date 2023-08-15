using Annex_Old.Scenes.Components;

namespace Annex_Old.Scenes
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
