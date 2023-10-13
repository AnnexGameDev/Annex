using Annex.Core.Data;
using Annex.Core.Scenes.Elements;

namespace SampleProject.Scenes.ListViewExample;

internal class ListViewExampleScene : Scene
{
    public ListViewExampleScene() : base(size: new Vector2f(960, 640)) {
        var listView = new ListView(position: this.Position, size: this.Size)
        {
            BackgroundTextureId = "ui/buttons/whitebox.png",
            LineHeight = 50,
            FontSize = 30,
            FontColor = KnownColor.Red,
            //SelectedFontColor = KnownColor.White,
            //SelectedTextureId = "ui/buttons/orangefade.png",
            //IsSelectable = true,
            //MaxItems = ListView.Unlimited,
            //CanScroll = true
        };

        listView.AddItem("Hello".ToShared());
        listView.AddItem("World".ToShared());

        this.AddChild(listView);
    }
}
