using Annex.Scenes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Annex.Events
{
    public sealed class GameEvents : Singleton
    {
        private readonly EventQueue _queue;

        static GameEvents() {
            Create<GameEvents>();
        }
        public static GameEvents Singleton => Get<GameEvents>();

        public GameEvents() {
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
