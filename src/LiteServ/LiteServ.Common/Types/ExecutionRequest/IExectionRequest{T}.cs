using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.ExecutionRequest
{
    public interface IExecutionRequest<T>:IExecutionRequestBase
    {
        T Request { get; set; }
    }
}