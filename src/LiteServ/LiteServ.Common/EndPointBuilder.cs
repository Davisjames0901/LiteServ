using System.Collections.Generic;
using LiteServ.Common.Endpoints;

namespace LiteServ.Common
{
    public class EndPointBuilder
    {
        public readonly Dictionary<string, IEndpointBase> Endpoints;

        public EndPointBuilder()
        {
            Endpoints = new Dictionary<string, IEndpointBase>();
        }

        public IEndpoint Add(string path)
        {
            var endpoint = new Endpoint();
            Endpoints.Add(path, endpoint);
            return endpoint;
        }
        
        public IEndpoint<T> Add<T>(string path)
        {
            var endpoint = new Endpoint<T>();
            Endpoints.Add(path, endpoint);
            return endpoint;
        }
    }
}