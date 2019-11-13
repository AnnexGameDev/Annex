using Annex.Data.Shared;
using Annex.Graphics.Cameras;
using Annex.Scenes;

namespace Annex.Graphics.Contexts
{
    public abstract class GraphicsContext : IDrawableContext
    {
        public abstract void Draw(TextContext ctx);
        public abstract void Draw(TextureContext ctx);
        public abstract void Draw(SpriteSheet sheet);
        public abstract void Draw(SolidRectangleContext rectangle);
        public abstract void BeginDrawing();

        public abstract bool IsMouseButtonDown(MouseButton button);
        public abstract bool IsKeyDown(KeyboardKey key);

        public abstract bool IsJoystickConnected(uint joystickId);
        public abstract bool IsJoystickButtonPressed(uint joystickId, JoystickButton button);
        public abstract float GetJoystickAxis(uint joystickId, JoystickAxis axis);

        public abstract void EndDrawing();
        public abstract void SetVisible(bool visible);

        public abstract Camera GetCamera();

        public abstract void Destroy();

        public abstract Vector GetRealMousePos();
    }
}
