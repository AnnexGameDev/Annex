using Annex_Old.Core.Broadcasts;
using Annex_Old.Core.Broadcasts.Messages;
using Annex_Old.Core.Events.Core;
using Annex_Old.Core.Graphics.Windows;
using Annex_Old.Core.Networking;
using Annex_Old.Core.Networking.Connections;
using Annex_Old.Core.Networking.Packets;
using Annex_Old.Core.Scenes.Components;
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
