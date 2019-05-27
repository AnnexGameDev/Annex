using Annex.Data;
using Annex.UserInterface.Components;

namespace SampleProject.Scenes.MainMenu.Buttons
{
    public class SampleButton : Button
    {
        public SampleButton() {
            this.Position.Set(0, 540);
            this.Size.Set(300, 100);
            this.RenderBoxSurface.Set("gui/buttons/orangefade.png");
            this.Caption.Set("Hello world!");
            this.Font.Set("Augusta.ttf");
            this.RenderText.FontColor = RGBA.White;
            this.RenderText.FontSize = 26;
            this.RenderText.BorderColor = RGBA.Black;
            this.RenderText.BorderThickness = 3;
        }
    }
}
