using Annex.Graphics;

namespace Annex.UserInterface.Components
{
    public class Scene : Container
    {
        public Scene() {
            this.Position.Set(0, 0);
            this.Size.Set(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
        }
    }
}
