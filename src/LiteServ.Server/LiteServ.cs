using System;
using System.Collections.Generic;
using System.Linq;
using LiteServ.Common.Types;
using LiteServ.Common.Types.Hubs;
using Microsoft.Extensions.DependencyInjection;

namespace LiteServ.Server
{
    public static class LiteServ
    {
        public static void Start(Action<IServiceCollection> configure)
        {
            var services = new ServiceCollection();
            configure(services);

            services = RegisterHubs(services);
            services.AddSingleton<RoutingContext>();
            services.AddSingleton<LiteServServer>();

            var serviceProvider = services.BuildServiceProvider();
        }

        private static ServiceCollection RegisterHubs(ServiceCollection services)
        {
            foreach (var hub in GetHubs())
            {
                services.AddTransient(typeof(LiteHub), hub);
            }

            return services;
        }
        
        private static IEnumerable<Type> GetHubs() => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(LiteHub).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
    }
}