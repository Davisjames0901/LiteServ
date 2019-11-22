using LiteServ.Common.Serialization;
using LiteServ.Common.Types.LiteActions;
using LiteServ.Common.Types.Routing;

namespace LiteServ.Common
{
    public class ExecutionContext
    {
        public RoutingStatus RoutingStatus { get; set; }
        public SerializationContext SerializationContext { get; set; }
        public ILiteActionBase Action { get; set; }
    }
}