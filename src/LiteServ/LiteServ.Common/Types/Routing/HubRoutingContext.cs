using System.Collections.Generic;
using System.Linq;
using LiteServ.Common.Endpoints;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.Routing
{
    public class HubRoutingContext
    {
        public HubRoutingContext(IEnumerable<EndpointBuilder> builders)
        {
            _routes = builders.SelectMany(x => x.Endpoints)
                .ToDictionary(x => x.Key.Replace('\\', '/'), y => y.Value);
        }

        private readonly Dictionary<string, IEndpointBase> _routes;

        public IExecutionResultBase Execute(string path, IExecutionRequestBase request)
        {
            if (!_routes.ContainsKey(path))
            {
                return new ExecutionResult.ExecutionResult
                {
                    Status = Status.NotFound
                };
            }

            return _routes[path].Execute(request);
        }
    }
}