using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes
{
    public class OnSceneEnterEventArgs
    {
        /// The first time a scene is loaded, the previous scene is null
        public IScene? PreviousScene { get; }

        public object Parameter { get; }

        public OnSceneEnterEventArgs(IScene? previousScene, object parameter) {
            this.PreviousScene = previousScene;
            Parameter = parameter;
        }
    }
}