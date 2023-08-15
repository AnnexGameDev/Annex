namespace Annex.Core.Scenes.Elements;

public interface ITextbox
{
    string Text { get; set; }
    int CursorIndex { get; }

    string SelectedText { get; }
    int SelectionStart { get; }
    int SelectionLength { get; }

    void SelectText(int start, int length);
    void ClearSelectText();
}
