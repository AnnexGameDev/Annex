namespace Annex.Core.Networking
{
    public class EndpointConfiguration
    {
        public string AppIdentifier { get; set; } = "annex-app";
        public int Port { get; set; } = 4000;
        public string IP { get; set; } = "127.0.0.1";
        public TransmissionType TransmissionType { get; set; } = TransmissionType.ReliableOrdered;

        public override string ToString() {
            return
@$"{{
    {nameof(this.IP)}: {this.IP},
    {nameof(this.Port)}: {this.Port},
    {nameof(this.AppIdentifier)}: {this.AppIdentifier},
    {nameof(this.TransmissionType)}: {this.TransmissionType}
}}
";
        }
    }
}
