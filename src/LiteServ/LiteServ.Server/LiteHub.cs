using LiteServ.Common;

namespace LiteServ.Server
{
    public abstract class LiteHub
    {
        private EndPointBuilder _builder;
        public abstract void BuildEndpoints(EndPointBuilder endPointBuilder);

        public void Build()
        {
            var builder = new EndPointBuilder();
            BuildEndpoints(builder);
            _builder = builder;
        }
    }
}