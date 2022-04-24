namespace Annex.Core.Scenes.Components
{
    public interface ITextbox
    {
        string SelectedText { get; }
        int SelectionStart { get; }
        int SelectionLength { get; }

        void SelectText(int start, int length);
        void ClearSelectText();
    }
}
