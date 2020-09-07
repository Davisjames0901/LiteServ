using System;
using Microsoft.Extensions.DependencyInjection;

namespace LiteServ.TestConsole
{
    public static class ServerBootstrapper
    {
        public static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            

            return services.BuildServiceProvider();
        }
    }
}