using LiteServ.Core;

namespace LiteServ
{
    public abstract class LiteHub
    {
        public abstract void BuildEndpoints(EndPointBuilder endPointBuilder);
    }
}