﻿using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using Annex.Scenes;
using Lidgren.Network;
using System;

namespace Annex.Networking.Lidgren
{
    public class Server<T> : Networking.Server<T> where T : Connection, new()
    {
        private NetPeerConfiguration _lidgrenConfig;
        private NetServer _lidgrenServer;
        private NetDeliveryMethod _method;

        public Server(ServerConfiguration config) : base(config) {
            this._lidgrenConfig = config;
            this._lidgrenConfig.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            this._method = config.Method == TransmissionType.ReliableOrdered ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable;
        }

        public override void Destroy() {
            this._lidgrenServer.Shutdown("shutdown");
        }

        public override void Start() {
            Console.WriteLine($"Creating server: {this.Configuration}");
            this._lidgrenServer = new NetServer(this._lidgrenConfig);
            this._lidgrenServer.Start();

            EventManager.Singleton.AddEvent(PriorityType.NETWORK, this.OnReceive, 0, 0, NetworkEventID);
        }

        private ControlEvent OnReceive() {

            if (SceneManager.Singleton.IsCurrentScene<GameClosing>()) {
                this.Destroy();
                return ControlEvent.REMOVE;
            }

            NetIncomingMessage message;
            while ((message = this._lidgrenServer.ReadMessage()) != null) {
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
                case NetIncomingMessageType.ConnectionApproval: {
                    message.SenderConnection.Approve(this._lidgrenServer.CreateMessage("Approve"));
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
                    Console.WriteLine($"[NET SERVER LIDGREN] - processing {message.MessageType} message");
                    break;
            }
        }

        public override void SendPacket(T client, int packetID, OutgoingPacket packet) {
            var connection = client.BaseConnection as NetConnection;
            Debug.Assert(connection != null);

            var message = this._lidgrenServer.CreateMessage();
            message.Write(packetID);
            message.Write(packet.GetBytes());
            this._lidgrenServer.SendMessage(message, connection, this._method);
        }
    }
}
