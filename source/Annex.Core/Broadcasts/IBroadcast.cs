namespace Annex_Old.Core.Broadcasts
{
    public interface IBroadcast<T>
    {
        event EventHandler<T>? OnBroadcastPublished;

        public void Publish(object sender, T broadcast);
    }
}
