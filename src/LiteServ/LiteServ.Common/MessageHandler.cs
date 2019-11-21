using LiteServ.Common.Serialization;
using LiteServ.Common.Types;
using LiteServ.Common.Types.Routing;

namespace LiteServ.Common
{
    public class MessageHandler
    {
        private readonly ISerializer _serializer;
        public MessageHandler(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        public Packet MessageReceived(Packet packet)
        {
            return packet;
        }
    }
}