using System;
using System.Collections.Generic;
using System.Linq;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.Hubs;
using LiteServ.Common.Types.Routing;

namespace LiteServ.Server
{
    public class LiteServServer
    {
        private RoutingContext _routes;
        public void Start()
        {
            var hubs = GetHubs();
            var routeBuilders = hubs
                .Select(x=>Activator.CreateInstance(x)as LiteHub)
                .Select(x=> x.Build());
            _routes= new RoutingContext(routeBuilders);

        }

        public IExecutionResultBase Process(string path, string content)
        {
            var executionContext = _routes.GetExecutionContext(path, content, new JsonSerializer());
            var request = executionContext.SerializationContext.CreateRequest(content);
            if (request.Status == SerializationStatus.Ok)
            {
                return executionContext.Action.Execute(request.Request);
            }
            return new ExecutionResult
            {
                Status = Status.InternalError
            };
        }

        //Todo: we should be using DI for this but I am lazy and will do it later when it matters more :D
        private IEnumerable<Type> GetHubs()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(LiteHub).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        }
    }
}