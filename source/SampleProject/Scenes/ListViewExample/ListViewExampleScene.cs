using Annex.Core.Data;
using Annex.Core.Scenes.Elements;

namespace SampleProject.Scenes.ListViewExample;

internal class ListViewExampleScene : Scene
{
    public ListViewExampleScene() : base(size: new Vector2f(960, 640)) {
        var listView = new ListView(position: this.Position, size: new Vector2f(100, 200))
        {
            BackgroundTextureId = "ui/buttons/whitebox.png",
            LineHeight = 50,
            FontSize = 30,
            FontColor = KnownColor.Red,
            SelectedTextureId = "ui/buttons/orangefade.png",
            SelectedFontColor = KnownColor.White,
            IsSelectable = true,
            ShowIndexPrefix = true,
            //CanScroll = true
        };

        listView.AddItem("Hello".ToShared());
        listView.AddItem("World".ToShared());

        for (int i = 0; i < 100; i++)
        {
            listView.AddItem($"{i}".ToShared());
        }

        this.AddChild(listView);
    }
}
