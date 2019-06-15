using Annex.Events;
using Annex.Graphics;

namespace Annex.UserInterface.Components
{
    public class Scene : Container
    {
        public readonly EventQueue Events;

        public Scene() {
            this.Events = new EventQueue();
            this.Position.Set(0, 0);
            this.Size.Set(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT);
        }

        public override void Draw(IDrawableContext surfaceContext) {
            this.DrawGameContent(surfaceContext);
            base.Draw(surfaceContext);
        }

        public virtual void DrawGameContent(IDrawableContext surfaceContext) {

        }
    }
}
