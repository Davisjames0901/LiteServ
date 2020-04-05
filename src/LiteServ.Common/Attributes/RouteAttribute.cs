using System;

namespace LiteServ.Common.Attributes
{
    public class RouteAttribute : Attribute
    {
        
        public readonly string Route; 
        public RouteAttribute(string route)
        {
            Route = route;
        }
    }
}