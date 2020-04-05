using System;

namespace LiteServ.Common.Types
{
    public class RequestPacket
    {
        public string Content { get; set; }
        public int Checksum { get; set; }
        public string Path { get; set; }
        public Guid ClientId { get; set; }
    }
}