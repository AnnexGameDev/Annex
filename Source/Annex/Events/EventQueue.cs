using System;
using System.Collections.Generic;

namespace Annex.Events
{
    public class EventQueue
    {
        private readonly List<GameEvent>[] _queue;

        public EventQueue() {
            this._queue = new List<GameEvent>[Priorities.Count];

            foreach (int priority in Priorities.All) {
                this._queue[priority] = new List<GameEvent>();
            }
        }

        public void AddEvent(PriorityType type, GameEvent e) {
            Debug.Assert((int)type < this._queue.Length);
            Debug.Assert((int)type >= 0);
            this._queue[(int)type].Add(e);
        }

        public void AddEvent(PriorityType type, Func<ControlEvent> e, int interval_ms, int delay_ms = 0) {
            this.AddEvent(type, new GameEvent(e, interval_ms, delay_ms));
        }

        public List<GameEvent> GetPriority(PriorityType type) {
            return this.GetPriority((int)type);
        }

        public List<GameEvent> GetPriority(int type) {
            return this._queue[type];
        }
    }
}
