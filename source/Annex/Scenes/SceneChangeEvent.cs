using Annex.Scenes.Components;

namespace Annex.Scenes
{
    public class SceneChangeEvent
    {
    }

    public class SceneOnEnterEvent : SceneChangeEvent
    {
        public Scene PreviousScene;
    }

    public class SceneOnLeaveEvent : SceneChangeEvent
    {
        public Scene NextScene;
    }
}
