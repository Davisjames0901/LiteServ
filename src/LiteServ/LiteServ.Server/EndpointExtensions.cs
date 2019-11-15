using System;
using LiteServ.Common.Types;
using LiteServ.Core;

namespace LiteServ
{
    public static class EndpointExtensions
    {
        public static Endpoint<T> Ok<T>(this Endpoint<T> endpoint, Action<T> action)
        {
            return endpoint;
        }
        public static Endpoint<T> Error<T>(this Endpoint<T> endpoint, Action<Error> action)
        {
            return endpoint;
        }
    }
}