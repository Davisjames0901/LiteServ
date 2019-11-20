namespace LiteServ.Common.Types.ExecutionResult
{
    public class ExecutionResult : IExecutionResultBase
    {
        public Status Status { get; set; }
        public Error Error { get; set; }
    }
}