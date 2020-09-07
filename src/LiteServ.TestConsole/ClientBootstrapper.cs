using System;
using Microsoft.Extensions.DependencyInjection;

namespace LiteServ.TestConsole
{
    public static class ClientBootstrapper
    {
        public static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            

            return services.BuildServiceProvider();
        }
    }
}