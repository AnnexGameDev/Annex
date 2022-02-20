namespace Annex.Core.Input.InputEvents
{
    public abstract class InputEvent
    {
        public bool Handled { get; set; } = false;
    }
}