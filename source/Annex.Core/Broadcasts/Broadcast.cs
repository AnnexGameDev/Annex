namespace Annex.Core.Broadcasts
{
    internal class Broadcast<T> : IBroadcast<T>
    {
        public event EventHandler<T>? OnBroadcastPublished;

        public void Publish(object sender, T message) {
            this.OnBroadcastPublished?.Invoke(sender, message);
        }
    }
}