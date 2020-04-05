using System;

namespace LiteServ.Common.Attributes
{
    public class ActionAttribute : Attribute
    {
        public readonly string Route; 
        public ActionAttribute(string route = null)
        {
            Route = route;
        }
    }
}