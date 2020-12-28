using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using Annex.Services;
using Lidgren.Network;
using System;

namespace Annex.Networking.Lidgren
{
    public class LidgrenClient<T> : ClientEndpoint<T> where T : Connection, new()
    {
        private readonly NetPeerConfiguration _lidgrenConfig;
        private readonly NetDeliveryMethod _method;
        private NetClient? _lidgrenClient;
        private LidgrenReceiveMessageEvent? _receiveEvent;

        public LidgrenClient(ClientConfiguration config) : base(config) {
            this._lidgrenConfig = config;
            this._method = config.Method == TransmissionType.ReliableOrdered ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable;
        }

        public override void Start() {
            ServiceProvider.LogService?.WriteLineTrace(this, $"Creating client: {this.Configuration}");
            this._lidgrenClient = new NetClient(this._lidgrenConfig);

            this._receiveEvent = new LidgrenReceiveMessageEvent(this._lidgrenClient);
            this._receiveEvent.OnServerReceive += this.ProcessMessage;
            ServiceProvider.EventService.AddEvent(PriorityType.NETWORK, this._receiveEvent);

            this._lidgrenClient.Start();
            this._lidgrenClient.Connect(this.Configuration.IP, this.Configuration.Port);
        }

        private void ProcessMessage(NetIncomingMessage message) {

            if (this.Connection == null) {
                this.CreateConnectionIfNotExistsAndGet(message.SenderConnection);
                Debug.ErrorIf(this.Connection == null, "Null connection");
            }
            var connection = this.Connection!;

            switch (message.MessageType) {
                case NetIncomingMessageType.StatusChanged: {
                    var state = (NetConnectionStatus)message.ReadByte();
                    connection.SetState(state.ToConnectionState());
                    break;
                }
                case NetIncomingMessageType.Data: {
                    using var packet = new IncomingPacket(message.Data);
                    this.HandlePacket(connection, packet);
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
            base.Destroy();
            if (this._receiveEvent != null) {
                this._receiveEvent.OnServerReceive -= this.ProcessMessage;
                this._receiveEvent.MarkForRemoval();
                this._receiveEvent = null;
            }

            this._lidgrenClient?.Disconnect("shutdown");
            this._lidgrenClient = null;
        }

        public override void SendPacket(int packetID, OutgoingPacket packet) {

            if (this._lidgrenClient == null) {
                ServiceProvider.LogService?.WriteLineWarning($"Canceling sending packet #{packetID} due to null client");
                return;
            }

            var message = this._lidgrenClient.CreateMessage();
            message.Write(packetID);
            message.Write(packet.GetBytes());
            this._lidgrenClient.SendMessage(message, this._method);
        }
    }
}
