using Annex_Old.Core.Networking.Packets;
using Scaffold.Collections.Generic;
using Scaffold.Logging;
using System.Net.Sockets;

namespace Annex_Old.Core.Networking.Engines.DotNet.Endpoints
{
    public abstract class TcpEndpoint : IEndpoint
    {
        protected readonly EndpointConfiguration Config;
        protected readonly Socket Socket;

        protected ConcurrentHashSet<TcpConnection> Connections = new();

        public TcpEndpoint(EndpointConfiguration config) {
            this.Config = config;
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected void HandleNewConnection(TcpConnection connection) {

            if (this.Connections.Contains(connection)) {
                Log.Trace(LogSeverity.Error, $"Connection {connection} is already registered");
                return;
            }

            this.Connections.Add(connection);
            connection.ListenForIncomingPackets();
        }

        protected void HandleDisconnectedConnection(TcpConnection connection) {
            if (!this.Connections.Contains(connection)) {
                Log.Trace(LogSeverity.Error, $"Connection {connection} is not registered");
                return;
            }

            this.Connections.Remove(connection);
        }

        protected void SendTo(TcpConnection connection, OutgoingPacket packet) {
            if (!this.Connections.Contains(connection)) {
                Log.Trace(LogSeverity.Error, $"Connection {connection} is not registered");
                return;
            }
            connection.SendOutgoingPacket(packet);
        }

        public void Dispose() {

            foreach (var connection in this.Connections) {
                connection.Dispose();
            }

            this.Socket.Disconnect(false);
            this.Socket.Dispose();
        }
    }
}
