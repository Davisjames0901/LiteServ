using LiteServ.Common.Serialization;

namespace LiteServ.Common
{
    public class MessageHandler
    {
        private readonly ISerializer _serializer;
        public MessageHandler(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
//        public Packet MessageReceived(Packet packet)
//        {
//            return packet;
//        }
    }
}