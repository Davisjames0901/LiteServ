using LiteServ.Common.Types.ExecutionRequest;

namespace LiteServ.Common.Types.ExecutionRequestset
{
    public class ExecutionRequest<T>:IExecutionRequest<T>
    {
        public T Content { get; set; }
    }
}