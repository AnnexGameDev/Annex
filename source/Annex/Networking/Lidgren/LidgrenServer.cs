using Annex_Old.Events;
using Annex_Old.Networking.Configuration;
using Annex_Old.Networking.Packets;
using Annex_Old.Services;
using Lidgren.Network;
using System;

namespace Annex_Old.Networking.Lidgren
{
    public class LidgrenServer<T> : ServerEndpoint<T> where T : Connection, new()
    {
        private readonly NetPeerConfiguration _lidgrenConfig;
        private readonly NetDeliveryMethod _method;
        private NetServer? _lidgrenServer;
        private LidgrenReceiveMessageEvent? _receiveEvent;

        public LidgrenServer(ServerConfiguration config) : base(config) {
            this._lidgrenConfig = config;
            this._lidgrenConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            this._method = config.Method == TransmissionType.ReliableOrdered ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable;
        }

        public override void Destroy() {
            if (this._receiveEvent != null) {
                this._receiveEvent.OnServerReceive -= this.ProcessMessage;
                this._receiveEvent.MarkForRemoval();
                this._receiveEvent = null;
            }

            this._lidgrenServer!.Shutdown("shutdown");
            this._lidgrenServer = null;
        }

        public override void Start() {
            ServiceProvider.LogService?.WriteLineTrace(this, $"Creating server: {this.Configuration}");
            this._lidgrenServer = new NetServer(this._lidgrenConfig);

            this._receiveEvent = new LidgrenReceiveMessageEvent(this._lidgrenServer);
            this._receiveEvent.OnServerReceive += this.ProcessMessage;
            ServiceProvider.EventService.AddEvent(PriorityType.NETWORK, this._receiveEvent);

            this._lidgrenServer.Start();
        }

        private void ProcessMessage(NetIncomingMessage message) {

            if (message.SenderConnection == null) {
                if (message.MessageType == NetIncomingMessageType.WarningMessage) {
                    ServiceProvider.LogService?.WriteLineWarning(message.ReadString());
                }
                 return;
            }

            this.CreateConnectionIfNotExistsAndGet(message.SenderConnection);

            switch (message.MessageType) {
                case NetIncomingMessageType.StatusChanged:
                    this.HandleStatusChangedMessage(message);
                    break;
                case NetIncomingMessageType.ConnectionApproval:
                    this.HandleConnectionApprovalMessage(message);
                    break;
                case NetIncomingMessageType.Data:
                    this.HandleDataMessage(message);
                    break;
                case NetIncomingMessageType.WarningMessage:
                    Console.WriteLine($"Warning Message: {message.ReadString()}");
                    break;
                default:
                    Console.WriteLine($"[NET SERVER LIDGREN] - processing {message.MessageType} message");
                    break;
            }
        }

        private void HandleDataMessage(NetIncomingMessage message) {
            using var packet = new IncomingPacket(message.Data);
            var connection = this.GetConnection(message.SenderConnection);
            this.HandlePacket(connection, packet);
        }

        private void HandleConnectionApprovalMessage(NetIncomingMessage message) {
            message.SenderConnection.Approve(this._lidgrenServer!.CreateMessage("Approve"));
        }

        private void HandleStatusChangedMessage(NetIncomingMessage message) {
            var state = (NetConnectionStatus)message.ReadByte();
            var connection = this.GetConnection(message.SenderConnection);
            connection.SetState(state.ToConnectionState());
        }

        private protected override void SendPacket(T client, int packetID, OutgoingPacket packet) {
            var connection = client.BaseConnection as NetConnection;
            Debug.Assert(connection != null, $"Client was null");

            var message = this._lidgrenServer!.CreateMessage();
            message.Write(packetID);
            message.Write(packet.GetBytes());
            this._lidgrenServer.SendMessage(message, connection, this._method);
        }

        public override void DisconnectClient(int id) {
            var clientConnection = this.GetConnection(id);
            this.RemoveClient(id);
            var baseConnection = clientConnection.BaseConnection as NetConnection;
            baseConnection!.Disconnect("Disconnect");
            clientConnection.Destroy();
        }
    }
}
