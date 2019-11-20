using System.Security.Cryptography.X509Certificates;

namespace LiteServ.Common.Types
{
    public class Packet
    {
        public string Content { get; set; }
        public int Checksum { get; set; }
        public string Path { get; set; }
    }
}