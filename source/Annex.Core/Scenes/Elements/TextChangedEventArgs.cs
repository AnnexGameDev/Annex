namespace Annex.Core.Scenes.Elements;

public class TextChangedEventArgs : EventArgs
{
    public string OldText { get; }

    public TextChangedEventArgs(string oldText)
    {
        OldText = oldText;
    }
}
