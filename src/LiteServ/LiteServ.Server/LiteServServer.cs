using System;
using System.Collections.Generic;
using System.Linq;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.Hubs;
using LiteServ.Common.Types.Routing;

namespace LiteServ.Server
{
    public class LiteServServer
    {
        private HubRoutingContext _routes;
        public void Start()
        {
            var hubs = GetHubs();
            var routeBuilders = hubs
                .Select(x=>Activator.CreateInstance(x)as LiteHub)
                .Select(x=> x.Build());
            _routes= new HubRoutingContext(routeBuilders);

        }

        public IExecutionResultBase Process(string path, IExecutionRequestBase request)
        {
            return _routes.Execute(path, request);
        }

        //Todo: we should be using DI for this but I am lazy and will do it later when it matters more :D
        private IEnumerable<Type> GetHubs()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(LiteHub).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        }
    }
}