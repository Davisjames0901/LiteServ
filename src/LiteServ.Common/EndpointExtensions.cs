using System;
using LiteServ.Common.Endpoints;
using LiteServ.Common.Types.LiteActions;

namespace LiteServ.Common
{
    public static class EndpointExtensions
    {
        public static IEndpoint On<T>(this IEndpoint endpoint, Action<T> action)
        {
            endpoint.Action = new LiteAction<T>(action);
            return endpoint;
        }
        
        public static IEndpoint<TT> On<T, TT>(this IEndpoint<TT> endpoint, Func<T, TT> action)
        {
            endpoint.Action = new LiteFunc<T, TT>(action);
            return endpoint;
        }
    }
}