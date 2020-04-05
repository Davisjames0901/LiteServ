namespace LiteServ.Common.Types.ExecutionRequest
{
    public interface IExecutionRequest<T>:IExecutionRequestBase
    {
        T Content { get; set; }
    }
}