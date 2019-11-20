using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteServ.Server
{
    public class LiteServServer
    {
        //Dictionary<>
        public LiteServServer()
        {

        }

        public void Start()
        {
            var hubs = GetHubs();
            
        }

        private IEnumerable<Type> GetHubs()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(LiteHub).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        }
    }
}