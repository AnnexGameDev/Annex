using Annex.Core.Broadcasts;
using Annex.Core.Broadcasts.Messages;
using Annex.Core.Events.Core;
using Annex.Core.Graphics.Windows;
using Annex.Core.Networking;
using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Annex.Core.Scenes.Components;
using SampleProject.Scenes.Level2.Events;
using System;

namespace SampleProject.Scenes.Level2
{
    public class Level2 : Scene
    {
        private readonly IServerEndpoint _server;
        private readonly IBroadcast<RequestStopAppMessage> _requestStopAppMessage;

        public Level2(IBroadcast<RequestStopAppMessage> requestStopAppMessage, INetworkingEngine networkingEngine) {
            this._requestStopAppMessage = requestStopAppMessage;
            var config = new EndpointConfiguration();
            this._server = networkingEngine.CreateServer(config);
            this._server.Start();
            this.Events.Add(CoreEventPriority.Networking, new SendDataEvent(networkingEngine));
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing) {
                this._server.Dispose();
            }
        }

        public override void OnWindowClosed(IWindow window) {
            base.OnWindowClosed(window);
            this._requestStopAppMessage.Publish(this, new RequestStopAppMessage());
        }
    }

    public enum PacketId : int
    {
        SimpleMessage
    }

    [PacketHandler(PacketId.SimpleMessage)]
    public class SimpleMessagePacketHandler : IPacketHandler
    {
        public void Handle(Connection connection, IncomingPacket packet) {
            Console.WriteLine(packet.ReadString("msg"));
        }
    }
}
