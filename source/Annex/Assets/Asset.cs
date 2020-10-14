using System;

namespace Annex.Assets
{
    public abstract class Asset : IDisposable
    {
        public string ID { get; }
        public long LastUse { get; private set; }
        private object _target;

        public Asset(string id, object target) {
            this.ID = id;
            this.SetTarget(target);
        }

        public void SetTarget(object obj) {
            this._target = obj;
            this.LastUse = GameTime.Now;
        }

        public object GetTarget() {
            this.LastUse = GameTime.Now;
            return this._target;
        }

        public abstract void Dispose();
    }
}
