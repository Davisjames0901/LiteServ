namespace LiteServ.Common.Types.ExecutionResult
{
    public interface IExecutionResult<T> : IExecutionResultBase
    {
        T Content { get; }
    }
}