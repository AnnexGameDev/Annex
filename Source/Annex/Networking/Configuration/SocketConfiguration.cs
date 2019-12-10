namespace Annex.Networking.Configuration
{
    public class SocketConfiguration
    {
        public string AppIdentifier = "annex-app";
        public int Port = 4000;
        public string IP = "127.0.0.1";
        public Protocol Protocol = Protocol.TCP;

        public override string ToString() {
            return $"{{\r\n" +
                $"\t{nameof(IP)}: {IP}\r\n" +
                $"\t{nameof(Port)}: {Port}\r\n" +
                $"\t{nameof(AppIdentifier)}: {AppIdentifier}\r\n" +
                $"\t{nameof(Protocol)}: {Protocol}\r\n" +
                $"}}";
        }
    }
}
