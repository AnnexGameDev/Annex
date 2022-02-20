namespace Annex.Core.Input.InputEvents
{
    public class KeyboardEvent : InputEvent
    {
        public KeyboardKey Key { get; }

        public KeyboardEvent(KeyboardKey key) {
            this.Key = key;
        }
    }
}