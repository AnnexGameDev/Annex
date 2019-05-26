using Annex;
using Annex.Graphics;
using Annex.UserInterface.Components;
using SampleProject.Data;

namespace SampleProject.Scenes
{
    public class MainMenu : Scene
    {
        private DataManager _data;
        
        public MainMenu() {
            this._data = Singleton.Get<DataManager>();
        }

        public override void Draw(IDrawableContext surfaceContext) {
            this._data.Player.Draw(surfaceContext);
            base.Draw(surfaceContext);
        }
    }
}
