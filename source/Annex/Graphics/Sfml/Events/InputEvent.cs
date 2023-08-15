using Annex_Old.Events;
using System;
using static Annex_Old.Graphics.EventIDs;

namespace Annex_Old.Graphics.Sfml.Events
{
    public class InputEvent : GameEvent
    {
        private readonly Action _processInput;

        public InputEvent(Action processInput) : base(ProcessUserInputGameEventID, 16, 0) {
            this._processInput = processInput;
        }

        protected override void Run(Annex_Old.Events.EventArgs gameEventArgs) {
            this._processInput.Invoke();
        }
    }
}
