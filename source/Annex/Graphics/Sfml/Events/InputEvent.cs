using Annex.Events;
using System;
using static Annex.Graphics.EventIDs;

namespace Annex.Graphics.Sfml.Events
{
    public class InputEvent : GameEvent
    {
        private readonly Action _processInput;

        public InputEvent(Action processInput) : base(ProcessUserInputGameEventID, 16, 0) {
            this._processInput = processInput;
        }

        protected override void Run(Annex.Events.EventArgs gameEventArgs) {
            this._processInput.Invoke();
        }
    }
}
