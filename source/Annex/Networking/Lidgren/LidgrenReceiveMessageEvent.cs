using Annex.Events;
using Lidgren.Network;

namespace Annex.Networking.Lidgren
{
    public class LidgrenReceiveMessageEvent : GameEvent
    {
        private readonly NetPeer _netPeer;
        public event System.Action<NetIncomingMessage>? OnServerReceive;

        public LidgrenReceiveMessageEvent(NetPeer netPeer) : base(0, 0) {
            this._netPeer = netPeer;
        }

        protected override void Run(EventArgs gameEventArgs) {
            NetIncomingMessage message;
            while ((message = this._netPeer.ReadMessage()) != null) {
                this.OnServerReceive?.Invoke(message);
            }
        }
    }
}
