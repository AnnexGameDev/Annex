using Annex_Old.Services;
using System.Collections.Generic;

namespace Annex_Old.Events
{
    public sealed class EventService : IEventService
    {
        private readonly EventQueue _queue;

        public EventService() {
            this._queue = new EventQueue();
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
            long lastTick = GameTime.Now;
            var scenes = ServiceProvider.SceneService;
            long timeDelta;

            while (!condition.ShouldTerminate()) {
                tick = GameTime.Now;
                timeDelta = tick - lastTick;
                lastTick = tick;

                foreach (int priority in Priorities.All) {
                    this.RunQueueLevel(this._queue.GetPriority(priority), timeDelta);
                    this.RunQueueLevel(scenes.CurrentScene.EventQueue.GetPriority(priority), timeDelta);
                }
            }
        }

        public IEvent? GetEvent(string id) {
            return this._queue.GetEvent(id) ?? ServiceProvider.SceneService.CurrentScene.EventQueue.GetEvent(id);
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
    }
}
