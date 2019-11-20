using System;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;

namespace LiteServ.Common.Types.LiteActions
{
    public interface ILiteActionBase
    {
        IExecutionResultBase Execute(IExecutionRequestBase requestBase);
    }
}