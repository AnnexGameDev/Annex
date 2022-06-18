namespace Annex.Core.Input.InputEvents
{
    public class KeyboardKeyReleasedEvent : KeyboardEvent
    {
        public KeyboardKeyReleasedEvent(KeyboardKey key) : base(key) {
        }
    }
}