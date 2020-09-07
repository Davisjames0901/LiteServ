using System;

namespace LiteServ.Server
{
    public class ConnectionManager
    {
        private readonly IServiceProvider _provider;
        public ConnectionManager(IServiceProvider provider)
        {
            _provider = provider;
        }
        
        
    }
}