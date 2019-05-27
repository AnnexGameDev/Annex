using Annex.UserInterface;
using Annex.UserInterface.Scenes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Annex.Events
{
    public class EventQueue : Singleton
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

        public void Run() {
            int tick;
            int lastTick = Environment.TickCount;
            var ui = Singleton.Get<UI>();

            while (!ui.IsCurrentScene<GameClosing>()) {
                tick = Environment.TickCount;
                int diff = tick - lastTick;
                lastTick = tick;

                if (diff == 0) {
                    Thread.Yield();
                    continue;
                }

                foreach (int priority in Priorities.All) {
                    for (int i = 0; i < this._queue[priority].Count; i++) {
                        if (this._queue[priority][i].Probe(diff) == ControlEvent.REMOVE) {
                            this._queue[priority].RemoveAt(i--);
                        }
                    }
                }

                Thread.Yield();
            }
        }
    }
}
