using System;
using LiteServ.Common.Serialization;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.LiteActions
{
    public interface ILiteActionBase
    {
        IExecutionResultBase Execute(IExecutionRequestBase requestBase);
        SerializationContext GetSerializationContext(ISerializer serializer);
    }
}