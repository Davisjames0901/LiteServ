using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiteServ.Common.Attributes;
using LiteServ.Common.Endpoints;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types.LiteActions;
using LiteServ.Common.Types.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace LiteServ.Common.Types
{
    public class RoutingContext
    {
        public RoutingContext(IServiceCollection container)
        {
            _routes = new Dictionary<string, IEndpointBase>();
            _container = container;
        }

        private Dictionary<string, IEndpointBase> _routes;
        private readonly IServiceCollection _container;

        public ExecutionContext GetExecutionContext(string path)
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
                Action = _routes[path].Action,
                RoutingStatus = RoutingStatus.Ok
            };
        }
        
        public void AddHub(Type hub)
        {
            var routePrefix = (hub.GetCustomAttributes(true)
                .SingleOrDefault(x => x.GetType() == typeof(RouteAttribute)) as RouteAttribute)
                ?.Route;
            
            var actions = hub.GetMethods()
                .Where(x => x.GetCustomAttributes(true)
                        .Any(a => a.GetType() == typeof(ActionAttribute)));
            
            AddEndpoints(hub, actions, routePrefix);
        }

        private void AddEndpoints(Type hubType, IEnumerable<MethodInfo> actions, string routePrefix)
        {
            foreach (var action in actions)
            {
                var actionRoute = ((ActionAttribute) action.GetCustomAttributes().Single(x => x.GetType() == typeof(ActionAttribute))).Route;
                var route = $"{routePrefix}/{actionRoute}".ToLower();
                
                _routes.Add(route, new Endpoint
                {
                    Action = new LiteAction<>()
                });
            }
        }
        
        // public IEndpoint<T> Add<T>(string path)
        // {
        //     var endpoint = new Endpoint<T>();
        //     Endpoints.Add(_routePrefix+path.ToLower(), endpoint);
        //     return endpoint;
        // }
    }
}