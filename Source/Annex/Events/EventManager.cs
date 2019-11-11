using Annex.Scenes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Annex.Events
{
    public sealed class EventManager : Singleton
    {
        private readonly EventQueue _queue;
        private static int _timeOffset;
        public static int CurrentTime => Environment.TickCount + _timeOffset;

        static EventManager() {
            // We want CurrentTime to be 0 at applicaiton start.
            // Calculate an offset.
            _timeOffset = -Environment.TickCount;
            Create<EventManager>();
        }
        public static EventManager Singleton => Get<EventManager>();

        public EventManager() {
            this._queue = new EventQueue();
        }

        public void AddEvent(PriorityType type, Func<ControlEvent> e, int interval_ms, int delay_ms = 0) {
            this._queue.AddEvent(type, e, interval_ms, delay_ms);
        }

        public void AddEvent(PriorityType type, GameEvent e) {
            this._queue.AddEvent(type, e);
        }

        internal void Run() {
            int tick;
            int lastTick = Environment.TickCount;
            var scenes = SceneManager.Singleton;

            while (!scenes.IsCurrentScene<GameClosing>()) {
                tick = Environment.TickCount;
                int diff = tick - lastTick;
                lastTick = tick;

                if (diff == 0) {
                    Thread.Yield();
                    continue;
                }

                foreach (int priority in Priorities.All) {
                    this.RunQueueLevel(this._queue.GetPriority(priority), diff);
                    this.RunQueueLevel(scenes.CurrentScene.Events.GetPriority(priority), diff);
                }

                Thread.Yield();
            }
        }

        private void RunQueueLevel(List<GameEvent> level, int diff) {
            for (int i = 0; i < level.Count; i++) {
                if (level[i].Probe(diff) == ControlEvent.REMOVE) {
                    level.RemoveAt(i--);
                }
            }
        }
    }
}
