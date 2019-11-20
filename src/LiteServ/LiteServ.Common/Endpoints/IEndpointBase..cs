using System;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.LiteActions;

namespace LiteServ.Common.Endpoints
{
    public interface IEndpointBase
    {
        ILiteActionBase Action { get; set; }
        
        IExecutionResultBase Execute(IExecutionRequestBase obj);
        Type GetReturnType();
    }
}