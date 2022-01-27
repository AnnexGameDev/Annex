namespace Annex.Core.Services
{
    public interface IMessage<T>
    {
        event EventHandler<T>? OnMessagePublished;

        public void Publish(object sender, T message);
    }
}
