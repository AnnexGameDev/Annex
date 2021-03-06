﻿using System;
using System.Collections.Generic;
using static Annex.Events.Errors;

namespace Annex.Events
{
    public sealed class EventQueue
    {
        private readonly List<GameEvent>[] _queue;

        internal EventQueue() {
            this._queue = new List<GameEvent>[Priorities.Count];

            foreach (int priority in Priorities.All) {
                this._queue[priority] = new List<GameEvent>();
            }
        }

        public void AddEvent(PriorityType type, GameEvent gameEvent) {
            Debug.ErrorIf((int)type >= this._queue.Length || type < 0, INVALID_PRIORITY.Format(type));
            this._queue[(int)type].Add(gameEvent);
        }

        public void AddEvent(PriorityType type, Action<GameEventArgs> action, int interval_ms, int delay_ms = 0, string eventID = "") {
            this.AddEvent(type, new GameEvent(eventID, action, interval_ms, delay_ms));
        }

        public List<GameEvent> GetPriority(PriorityType type) {
            return this.GetPriority((int)type);
        }
        
        public List<GameEvent> GetPriority(int type) {
            return this._queue[type];
        }

        public GameEvent? GetEvent(string id) {
            foreach (var level in _queue) {
                foreach (var e in level) {
                    if (e.EventID == id) {
                        return e;
                    }
                }
            }
            return null;
        }
    }
}
