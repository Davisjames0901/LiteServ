using System;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Serialization
{
    public class SerializationContext
    {
        public Func<string, RequestSerializationResult> CreateRequest { get; set; }
        public Func<IExecutionResultBase, ResponseSerializationResult> CreateResponse { get; set; }
    }
}