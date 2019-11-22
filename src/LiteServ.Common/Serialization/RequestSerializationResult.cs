using LiteServ.Common.Types.ExecutionRequest;

namespace LiteServ.Common.Serialization
{
    public class RequestSerializationResult
    {
        public SerializationStatus Status { get; set; }
        public IExecutionRequestBase Request { get; set; }
    }
}