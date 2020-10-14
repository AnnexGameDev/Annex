using Annex.Events;

namespace Annex.Assets.Events
{
    public class AssetGCEvent : GameEvent
    {
        private readonly IAssetManager _manager;
        private readonly long _threshold;

        public AssetGCEvent(IAssetManager manager, long timeThreshold, string eventID, int interval_ms, int delay_ms) : base(eventID, interval_ms, delay_ms) {
            this._manager = manager;
            this._threshold = timeThreshold;
        }

        protected override void Run(EventArgs gameEventArgs) {
            var now = GameTime.Now;

            foreach (var asset in this._manager.GetCachedAssets()) {
                if (now - asset.LastUse > this._threshold) {
                    this._manager.UnloadCachedAsset(asset.ID);
                }
            }
        }
    }
}