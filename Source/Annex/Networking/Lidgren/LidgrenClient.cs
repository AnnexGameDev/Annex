using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using Annex.Scenes;
using Lidgren.Network;
using System;

namespace Annex.Networking.Lidgren
{
    public class LidgrenClient<T> : ClientEndpoint<T> where T : Connection, new()
    {
        private NetPeerConfiguration _lidgrenConfig;
        private NetClient _lidgrenClient;
        private NetDeliveryMethod _method;

        public LidgrenClient(ClientConfiguration config) : base(config) {
            this._lidgrenConfig = config;
            this._method = config.Method == TransmissionType.ReliableOrdered ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable;
        }

        public override void Start() {
            Console.WriteLine($"Creating client: {this.Configuration}");
            this._lidgrenClient = new NetClient(this._lidgrenConfig);
            this._lidgrenClient.Start();

            this._lidgrenClient.Connect(this.Configuration.IP, this.Configuration.Port);

            ServiceProvider.EventManager.AddEvent(PriorityType.NETWORK, this.OnReceive, 0, 0, NetworkEventID);
        }

        private ControlEvent OnReceive() {
            if (ServiceProvider.SceneManager.IsCurrentScene<GameClosing>()) {
                this.Destroy();
                return ControlEvent.REMOVE;
            }

            NetIncomingMessage message;
            while ((message = this._lidgrenClient.ReadMessage()) != null) {
                this.ProcessMessage(message);
            }

            return ControlEvent.NONE;
        }

        private void ProcessMessage(NetIncomingMessage message) {
            this.Connections.CreateIfNotExistsAndGet(message.SenderConnection, this);

            switch (message.MessageType) {
                case NetIncomingMessageType.StatusChanged: {
                    var state = (NetConnectionStatus)message.ReadByte();
                    var connection = this.Connections.Get(message.SenderConnection);
                    connection.SetState(state.ToConnectionState());
                    break;
                }
                case NetIncomingMessageType.Data: {
                    using var packet = new IncomingPacket(message.Data);
                    var connection = this.Connections.Get(message.SenderConnection);
                    this.PacketHandler.HandlePacket(connection, packet);
                    break;
                }
                case NetIncomingMessageType.WarningMessage:
                    Console.WriteLine($"Warning Message: {message.ReadString()}");
                    break;
                default:
                    Console.WriteLine($"[NET CLIENT LIDGREN] - processing {message.MessageType} message");
                    break;
            }
        }

        public override void Destroy() {
            this._lidgrenClient.Disconnect("shutdown");
        }

        public override void SendPacket(int packetID, OutgoingPacket packet) {
            var message = this._lidgrenClient.CreateMessage();
            message.Write(packetID);
            message.Write(packet.GetBytes());
            this._lidgrenClient.SendMessage(message, this._method);
        }
    }
}
