namespace LiteServ.Common.Types.ExecutionResult
{
    public class ExecutionResult<T> : IExecutionResult<T>
    {
        public ExecutionResult(T content)
        {
            Content = content;
        }
        public Status Status { get; set; }
        public Error Error { get; set; }
        public T Content { get; }
    }
}