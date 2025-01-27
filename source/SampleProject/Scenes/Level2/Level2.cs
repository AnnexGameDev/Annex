using Annex.Core.Graphics.Windows;
using Annex.Core.Networking;
using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Annex.Core.Scenes.Elements;
using Annex.Core.Time;
using System;
using System.Threading.Tasks;

namespace SampleProject.Scenes.Level2;

public class Level2 : Scene
{
    private readonly IServerEndpoint _server;

    public Level2(INetworkingEngine networkingEngine, ITimeService timeService)
    {

    }

    //public Level2(INetworkingEngine networkingEngine, ITimeService timeService)
    //{
    //    var config = new EndpointConfiguration();
    //    this._server = networkingEngine.CreateServer(config);
    //    this._server.Start();

    //    Game.Events.Add(new GameEvent(timeService, new SendDataEvent(networkingEngine).RunAsync, 1000));
    //}

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            this._server.Dispose();
        }
    }

    public override void OnWindowClosed(IWindow window)
    {
        base.OnWindowClosed(window);
        Game.Stop();
    }
}

public enum PacketId : int
{
    SimpleMessage
}

public class SimpleMessagePacketHandler : IPacketHandler
{
    public int Id { get; } = (int)PacketId.SimpleMessage;

    public Task HandleAsync(IConnection connection, IncomingPacket packet)
    {
        Console.WriteLine(packet.ReadString("msg"));
        return Task.CompletedTask;
    }
}
