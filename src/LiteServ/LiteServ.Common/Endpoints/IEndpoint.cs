namespace LiteServ.Common.Endpoints
{
    public interface IEndpoint : IEndpointBase
    {
        void Execute<T>(T obj);
    }
}