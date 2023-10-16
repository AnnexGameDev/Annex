using Annex.Core.Data;
using Annex.Core.Scenes.Elements;
using Annex.Core.Scenes.Layouts.Html;

namespace SampleProject.Scenes.ListViewExample;

internal class ListViewExampleScene : Scene
{
    public ListViewExampleScene(IHtmlSceneLoader htmlSceneLoader) {
        htmlSceneLoader.Load("listviewdemo.html", this);

        var listView = GetElementById<ListView>("lst");

        listView.AddItem("Hello".ToShared());
        listView.AddItem("World".ToShared());

        for (int i = 0; i < 100; i++)
        {
            listView.AddItem($"{i}".ToShared());
        }

        this.AddChild(listView);
    }
}
