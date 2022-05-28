using Annex_Old.Core.Scenes.Components;

namespace Annex_Old.Core.Scenes
{
    public class OnSceneEnterEventArgs
    {
        /// The first time a scene is loaded, the previous scene is null
        public IScene? PreviousScene { get; }

        public OnSceneEnterEventArgs(IScene? previousScene) {
            this.PreviousScene = previousScene;
        }
    }
}