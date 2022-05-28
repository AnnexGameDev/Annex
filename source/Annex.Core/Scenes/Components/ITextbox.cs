namespace Annex_Old.Core.Scenes.Components
{
    public interface ITextbox
    {
        int CursorIndex { get; }

        string SelectedText { get; }
        int SelectionStart { get; }
        int SelectionLength { get; }

        void SelectText(int start, int length);
        void ClearSelectText();
    }
}
