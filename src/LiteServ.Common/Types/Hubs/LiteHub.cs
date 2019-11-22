using System;

namespace LiteServ.Common.Types.Hubs
{
    public abstract class LiteHub
    {
        public abstract string Route { get; }
        public abstract void BuildEndpoints(EndpointBuilder endpointBuilder);

        public EndpointBuilder Build()
        {
            var route = Route?.ToLower() ?? string.Empty;
            route = route.EndsWith('/') ? route : route += '/';
            var builder = new EndpointBuilder(route);
            BuildEndpoints(builder);
            return builder;
        }
    }
}