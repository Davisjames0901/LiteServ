using System.Collections.Generic;
using System.Linq;
using LiteServ.Common.Endpoints;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.Routing;

namespace LiteServ.Common.Types
{
    public class RoutingContext
    {
        public RoutingContext(IEnumerable<EndpointBuilder> builders)
        {
            _routes = builders.SelectMany(x => x.Endpoints)
                .ToDictionary(x => x.Key.Replace('\\', '/'), y => y.Value);
        }

        private readonly Dictionary<string, IEndpointBase> _routes;

        public ExecutionContext GetExecutionContext(string path, string request, ISerializer serializer)
        {
            if (!_routes.ContainsKey(path))
            {
                return new ExecutionContext
                {
                    RoutingStatus = RoutingStatus.NotFound
                };
            }
            return new ExecutionContext
            {
                //todo: in the future we should have the endpoint create the serialization context for multiple type handles
                Action = _routes[path].Action,
                SerializationContext = _routes[path].Action.GetSerializationContext(serializer),
                RoutingStatus = RoutingStatus.Ok
            };
        }
    }
}