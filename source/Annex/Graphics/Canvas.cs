#nullable enable
using Annex.Data.Shared;
using Annex.Graphics.Cameras;
using Annex.Graphics.Contexts;
using Annex.Scenes;

namespace Annex.Graphics
{
    public abstract class Canvas : IDrawableSurface, IHardwarePollable, IService
    {
        public const string DrawGameEventID = "draw-game";

        public abstract Vector GetResolution();
        public abstract Camera GetCamera();
        public abstract void SetVideoMode(VideoMode mode);
        public abstract void ChangeResolution(uint width, uint height);
        public abstract void SetVisible(bool visible);
        public abstract void ProcessEvents();

        internal abstract void BeginDrawing();
        internal abstract void EndDrawing();

        public abstract Vector GetRealMousePos();
        public abstract Vector GetGameWorldMousePos();
        public abstract bool IsMouseButtonDown(MouseButton button);
        public abstract bool IsKeyDown(KeyboardKey key);
        public abstract bool IsJoystickConnected(uint joystickId);
        public abstract bool IsJoystickButtonPressed(uint joystickId, JoystickButton button);
        public abstract float GetJoystickAxis(uint joystickId, JoystickAxis axis);
        public abstract void Destroy();
        public abstract void Draw(TextContext ctx);
        public abstract void Draw(TextureContext ctx);
        public abstract void Draw(SpriteSheetContext sheet);
        public abstract void Draw(SolidRectangleContext rectangle);
    }
}
