namespace Annex.Core.Broadcasts
{
    public interface IBroadcast<T>
    {
        event EventHandler<T>? OnBroadcastPublished;

        public void Publish(object sender, T broadcast);
    }
}
