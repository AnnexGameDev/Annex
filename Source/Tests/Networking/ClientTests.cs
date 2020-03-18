using Annex.Networking;
using Annex.Networking.Configuration;
using Annex.Networking.DotNet;
using Annex.Networking.Lidgren;
using Annex.Networking.Packets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Tests.Networking
{
    public class ClientTests
    {
        private class ClientConnection : Connection
        {
            public ClientConnection() {
                this.OnChangeConnectionState += this.ClientConnection_OnChangeConnectionState;
            }

            private void ClientConnection_OnChangeConnectionState(ConnectionState newState) {
                if (newState == ConnectionState.Connected) {
                    var endpoint = this.Endpoint as IServer;
                    using var packet = new OutgoingPacket();
                    packet.Write("hello world");
                    endpoint.SendPacket(this.BaseConnection, 1, packet);
                }
            }
        }

        private class ServerConnection : Connection
        {
        }

        private IEnumerable<(ClientEndpoint<ServerConnection> client, ServerEndpoint<ClientConnection> server)> GetPairs() {
            var clientConfig = new ClientConfiguration() {
                AppIdentifier = "test",
                Method = TransmissionType.ReliableOrdered,
                IP = "127.0.0.1",
                Port = 4000
            };
            var serverConfig = new ServerConfiguration() {
                AppIdentifier = "test",
                Method = TransmissionType.ReliableOrdered,
                IP = "127.0.0.1",
                Port = 4000
            };
            yield return (new DotNetClient<ServerConnection>(clientConfig), new DotNetServer<ClientConnection>(serverConfig));
            yield return (new LidgrenClient<ServerConnection>(clientConfig), new LidgrenServer<ClientConnection>(serverConfig));

            clientConfig = new ClientConfiguration() {
                AppIdentifier = "test",
                Method = TransmissionType.UnreliableUnordered,
                IP = "127.0.0.1",
                Port = 4000
            };
            serverConfig = new ServerConfiguration() {
                AppIdentifier = "test",
                Method = TransmissionType.UnreliableUnordered,
                IP = "127.0.0.1",
                Port = 4000
            };
            yield return (new DotNetClient<ServerConnection>(clientConfig), new DotNetServer<ClientConnection>(serverConfig));
            yield return (new LidgrenClient<ServerConnection>(clientConfig), new LidgrenServer<ClientConnection>(serverConfig));
        }

        [Test]
        public void ClientConnects() {
            foreach ((var client, var server) in this.GetPairs()) {
                Console.WriteLine(client.GetType() + " " + server.GetType());
                server.Start();

                client.PacketHandler.AddPacketHandler(1, (connection, packet) => {
                    string message = packet.ReadString();
                    Assert.AreEqual(message, "hello world");
                });

                client.Start();
                Thread.Sleep(1000);
                Assert.IsTrue(server.Connections.Get(0) != null);
                Assert.IsTrue(server.Connections.Get(0).State == ConnectionState.Connected);
            }
        }
    }
}
