#nullable enable
using Annex.Scenes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Annex.Events
{
    public sealed class EventManager : Singleton
    {
        private readonly EventQueue _queue;
        public static long CurrentTime => Environment.TickCount64;

        static EventManager() {
            Create<EventManager>();
        }
        public static EventManager Singleton => Get<EventManager>();

        public EventManager() {
            this._queue = new EventQueue();
        }

        public void AddEvent(PriorityType type, Func<ControlEvent> e, int interval_ms, int delay_ms = 0, string eventID = "") {
            this._queue.AddEvent(eventID, type, e, interval_ms, delay_ms);
        }

        public void AddEvent(PriorityType type, GameEvent e) {
            this._queue.AddEvent(type, e);
        }

        internal void Run() {
            long tick;
            long lastTick = CurrentTime;
            var scenes = SceneManager.Singleton;

            while (!scenes.IsCurrentScene<GameClosing>()) {
                tick = CurrentTime;
                long diff = tick - lastTick;
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

        public GameEvent? GetEvent(string id) {
            return this._queue.GetEvent(id) ?? SceneManager.Singleton.CurrentScene.Events.GetEvent(id);
        }

        private void RunQueueLevel(List<GameEvent> level, long diff) {
            for (int i = 0; i < level.Count; i++) {
                if (level[i].Probe(diff) == ControlEvent.REMOVE) {
                    level.RemoveAt(i--);
                }
            }
        }
    }
}
