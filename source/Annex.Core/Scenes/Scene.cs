using Annex.Core.Scenes.Components;

namespace Annex.Core.Scenes
{
    public interface IScene
    {
        void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs);
        void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs);
    }
}