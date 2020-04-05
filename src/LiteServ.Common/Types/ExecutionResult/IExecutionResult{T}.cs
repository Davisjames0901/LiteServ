namespace LiteServ.Common.Types.ExecutionResult
{
    public interface IExecutionResult<out T> : IExecutionResultBase
    {
        T Content { get; }
    }
}