using System;
using LiteServ.Common.Types.ExecutionRequest;
using LiteServ.Common.Types.ExecutionResult;
using LiteServ.Common.Types.LiteActions;

namespace LiteServ.Common.Endpoints
{
    public class Endpoint<T> : IEndpoint<T>
    {
        public ILiteActionBase Action { get; set; }
        
        public IExecutionResultBase Execute(IExecutionRequestBase obj)
        {
            return Action.Execute(obj);
        }

        public Type GetReturnType()
        {
            return typeof(T);
        }
    }

}