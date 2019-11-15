using System.Collections.Generic;
using System.Net.Sockets;

namespace LiteServ.Core
{
    public class EndPointBuilder
    {
        private List<IEndpoint> endpoints;

        public EndPointBuilder(List<IEndpoint> endpoints)
        {
            this.endpoints = endpoints;
        }

        public Endpoint<T> Add<T>(string path)
        {
            var endpoint = new Endpoint<T> {Path = path};
            endpoints.Add(endpoint);
            return endpoint;
        }
    }
}