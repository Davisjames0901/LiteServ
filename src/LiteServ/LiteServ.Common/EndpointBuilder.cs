using System.Collections.Generic;
using LiteServ.Common.Endpoints;

namespace LiteServ.Common
{
    public class EndpointBuilder
    {
        public readonly Dictionary<string, IEndpointBase> Endpoints;
        private readonly string _routePrefix;

        public EndpointBuilder(string routePrefix)
        {
            Endpoints = new Dictionary<string, IEndpointBase>();
            _routePrefix = routePrefix;
        }

        public IEndpoint Add(string path)
        {
            var endpoint = new Endpoint();
            Endpoints.Add(_routePrefix+path.ToLower(), endpoint);
            return endpoint;
        }
        
        public IEndpoint<T> Add<T>(string path)
        {
            var endpoint = new Endpoint<T>();
            Endpoints.Add(_routePrefix+path.ToLower(), endpoint);
            return endpoint;
        }
    }
}