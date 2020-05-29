using Lidgren.Network;

namespace Annex.Networking.Lidgren
{
    public static class Extensions
    {
        public static ConnectionState ToConnectionState(this NetConnectionStatus status) {
            switch (status) {
                case NetConnectionStatus.Connected:
                    return ConnectionState.Connected;
                case NetConnectionStatus.Disconnected:
                    return ConnectionState.Disconnected;
                case NetConnectionStatus.Disconnecting:
                    return ConnectionState.Disconnected;
                case NetConnectionStatus.InitiatedConnect:
                case NetConnectionStatus.None:
                case NetConnectionStatus.ReceivedInitiation:
                case NetConnectionStatus.RespondedAwaitingApproval:
                case NetConnectionStatus.RespondedConnect:
                default:
                    return ConnectionState.Unknown;
            }
        }
    }
}
