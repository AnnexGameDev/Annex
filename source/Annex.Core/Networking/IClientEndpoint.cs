﻿using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IClientEndpoint : IEndpoint
    {
        IConnection Connection { get; }
        void Send(OutgoingPacket packet);
        void Start(CancellationToken? cancellationToken = null);
    }
}
