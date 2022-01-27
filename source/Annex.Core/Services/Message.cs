namespace Annex.Core.Services
{
    internal class Message<T> : IMessage<T>
    {
        public event EventHandler<T>? OnMessagePublished;

        public void Publish(object sender, T message) {
            this.OnMessagePublished?.Invoke(sender, message);
        }
    }
}