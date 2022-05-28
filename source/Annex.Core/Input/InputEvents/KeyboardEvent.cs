namespace Annex_Old.Core.Input.InputEvents
{
    public abstract class KeyboardEvent : HardwareEvent
    {
        public KeyboardKey Key { get; }

        public KeyboardEvent(KeyboardKey key) {
            this.Key = key;
        }
    }
}