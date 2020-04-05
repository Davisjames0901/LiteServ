using System;

namespace LiteServ.Common.Types
{
    public class ResponsePacket
    {
        public Status Status { get; set; }
        public string Content { get; set; }
        public int Checksum { get; set; }
        public Guid ClientId { get; set; }
    }
}