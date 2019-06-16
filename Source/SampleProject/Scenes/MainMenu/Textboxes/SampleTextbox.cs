using Annex.Data;
using Annex.Scenes.Components;

namespace SampleProject.Scenes.MainMenu.Textboxes
{
    public class SampleTextbox : Textbox
    {
        public SampleTextbox() {
            this.Size.Set(200, 50);
            this.Position.Set(0, 0);
            this.ImageSurface.Set("gui/textboxes/whitebox.png");
            this.Font.Set("Augusta.ttf");
            this.RenderText.FontColor = RGBA.Black;
            this.RenderText.FontSize = 16;
        }
    }
}
