namespace LiteServ.Common.Types.ExecutionResult
{
    public interface IExecutionResultBase
    {
        Status Status { get; set; }
        Error Error { get; set; }
    }
}