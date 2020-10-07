﻿using Annex.Assets;
using Annex.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Annex.Events
{
    public sealed class EventService : IService
    {
        private readonly EventQueue _queue;
        public static long CurrentTime => ServiceProvider.EventService._sw.ElapsedMilliseconds;
        private readonly Stopwatch _sw;

        public EventService() {
            this._queue = new EventQueue();
            this._sw = new Stopwatch();
            this._sw.Start();
        }

        public void AddEvent(PriorityType type, IEvent e) {
            this._queue.AddEvent(type, e);
        }

        public void Run(ITerminationCondition condition) {
            // Environment.TickCount is based on GetTickCount() WinAPI function. It's in milliseconds But the actual precision of it is about 15.6 ms. 
            // So you can't measure shorter time intervals (or you'll get 0)                                                                  
            // [ ^ this gave me a lot of headache. Current workaround to get more precise time diffs is using stopwatch ]
            //                                                                                                                          
            // Source: https://stackoverflow.com/questions/243351/environment-tickcount-vs-datetime-now/6308701#6308701
            long tick;
            long lastTick = CurrentTime;
            var scenes = ServiceProvider.SceneService;
            long timeDelta;

            while (!condition.ShouldTerminate()) {
                tick = CurrentTime;
                timeDelta = tick - lastTick;
                lastTick = tick;

                foreach (int priority in Priorities.All) {
                    this.RunQueueLevel(this._queue.GetPriority(priority), timeDelta);
                    this.RunQueueLevel(scenes.CurrentScene.Events.GetPriority(priority), timeDelta);
                }
            }
        }

        public IEvent? GetEvent(string id) {
            return this._queue.GetEvent(id) ?? ServiceProvider.SceneService.CurrentScene.Events.GetEvent(id);
        }

        private void RunQueueLevel(List<IEvent> level, long diff) {
            for (int i = 0; i < level.Count; i++) {
                var args = level[i].Probe(diff);
                if (args.RemoveFromQueue) {
                    level.RemoveAt(i--);
                }
            }
        }

        public void Destroy() {

        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}
